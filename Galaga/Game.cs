using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using Galaga.Squadron;
using Galaga.RandomSquadronCreater;
using Galaga.GameText;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private ISquadron squadron;
    private GameEventBus eventBus;
    private Player player;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    private List<Image> enemyStridesGreen;
    private List<Image> enemyStridesBlue;
    private Vec2F enemySpeed;
    private WindowArgs windowArgs;
    private IRandomSquadronCreater squadCreator;
    private RoundCounter roundCounter;
    private Health health;
    private GameTextContainer gameTexts;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        this.windowArgs = windowArgs;
        // Player
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        // EventBus
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent,
            GameEventType.WindowEvent, GameEventType.MovementEvent, GameEventType.GameStateEvent});
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
        eventBus.Subscribe(GameEventType.MovementEvent, player);
        // Enemies
        enemyStridesBlue = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets",
            "Images", "GreenMonster.png"));
        enemySpeed = new Vec2F(0f, -0.001f);
        // PlayerShot
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        // Explosions
        explosionStrides = ImageStride.CreateStrides(8, 
            Path.Combine("Assets", "Images", "Explosion.png"));
        enemyExplosions = new AnimationContainer(8);
        //GameTexts
        health = new Health(new Vec2F(0.02f, -0.4f), new Vec2F(0.5f, 0.5f));
        gameTexts = new GameTextContainer();
        gameTexts.AddText(health);
        roundCounter = new RoundCounter(new Vec2F(0.02f, 0.5f), new Vec2F(0.5f, 0.5f));
        gameTexts.AddText(roundCounter);
        //Squads
        squadCreator = new RSC1();
    }
    public override void Render() {
        player.Render();
        squadron.Enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
        gameTexts.RenderTexts();
    }
    public override void Update() {
        UpdateSquadron();
        eventBus.RegisterEvent(GameEventCreator.CreateMovementEvent("MoveAll"));
        IterateShots();
        UpdateHealth();
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
    }
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                eventBus.RegisterEvent(GameEventCreator.CreateMovementEvent("MoveLeft"));
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(GameEventCreator.CreateMovementEvent("MoveRight"));
                break;
            case KeyboardKey.Space:
                Vec2F pShot = player.GetPosition();
                float sumExtent = player.GetExtent().X - PlayerShot.GetExtent().X;
                PlayerShot newPS = new PlayerShot(new Vec2F(pShot.X + sumExtent / 2, pShot.Y), playerShotImage);
                eventBus.Subscribe(GameEventType.MovementEvent, newPS);
                playerShots.AddEntity(newPS);
                break;
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(GameEventCreator.CreateWindowEvent("CloseWindow"));
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        GameEvent MoveStop = GameEventCreator.CreateMovementEvent("MoveStop");
        switch (key) {
            case KeyboardKey.Left:
                eventBus.RegisterEvent(MoveStop);
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(MoveStop);
                break;
            case KeyboardKey.Space:
                break;
            case KeyboardKey.Escape:
                break;
        }
    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }
    void IGameEventProcessor.ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
            case "CloseWindow":
                window.CloseWindow();
                break;
            default:
                break;
        }}
        else if (gameEvent.EventType == GameEventType.GameStateEvent) {
            switch (gameEvent.Message) {
            case "Game Over":
                eventBus.Unsubscribe(GameEventType.MovementEvent, player);
                player.DeleteEntity();
                squadron.Enemies.Iterate(enemy => {
                    enemy.DeleteEntity();
                });
                gameTexts.RemoveText(health);
                gameTexts.AddText(new GameStateText("Game Over", new Vec2F(0.2f, 0f), new Vec2F(0.8f, 0.8f)));
                break;
            default:
                break;
        }}
    }
    private void IterateShots() {
        playerShots.Iterate(shot => {
            if (shot.Shape.Position.Y > 1) {
                eventBus.Unsubscribe(GameEventType.MovementEvent, shot);
                shot.DeleteEntity();
            } else {
            squadron.Enemies.Iterate(enemy => {
            if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                eventBus.Unsubscribe(GameEventType.MovementEvent, shot);
                shot.DeleteEntity();
                enemy.TakeDamage();
                if (enemy.IsDeleted()) {
                    AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                }
            }
            });
            }
        });
    }
    private void AddExplosion(Vec2F position, Vec2F extent) {
        StationaryShape explosion = new StationaryShape(position, extent);
        ImageStride strideExplosion = new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, strideExplosion);
    }

    private void UpdateHealth() {
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.Y < 0) {
                if (health.LoseHealth()) {
                    eventBus.RegisterEvent(GameEventCreator.CreateGameStateEvent("Game Over"));
                }
                enemy.DeleteEntity();
            }
        });
    }

    private void UpdateSquadron() {
        bool doUpdate = false;
        if (squadron == null) {
            doUpdate = true;
        }
        else if (squadron.Enemies.CountEntities() == 0 && !player.IsDeleted()) {
            doUpdate = true;
        }
        if (doUpdate) {
            if (squadron != null) {
                eventBus.Unsubscribe(GameEventType.MovementEvent, squadron);
            }
            squadron = squadCreator.CreateSquad(enemyStridesBlue, enemyStridesGreen);   
            enemySpeed += new Vec2F(0f, -0.00025f);
            squadron.ChangeSpeed(enemySpeed);
            eventBus.Subscribe(GameEventType.MovementEvent, squadron);
            roundCounter.IncrementRound();
        }
    }
}