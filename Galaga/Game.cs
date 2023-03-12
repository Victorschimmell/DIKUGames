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

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private EntityContainer<Enemy> enemies;
    private GameEventBus eventBus;
    private Player player;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    private List<Image> enemyStridesGreen;
    private List<Image> enemyStridesBlue;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // Player
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        // EventBus
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent,
            GameEventType.WindowEvent, GameEventType.MovementEvent});
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.MovementEvent, player);
        // Enemies
        enemyStridesBlue = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets",
            "Images", "GreenMonster.png"));
        ISquadron squad =  new Squadron3(enemyStridesBlue, enemyStridesGreen);
        enemies = squad.Enemies;
        // PlayerShot
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        // Explosions
        enemyExplosions = new AnimationContainer(squad.MaxEnemies);
        explosionStrides = ImageStride.CreateStrides(8, 
            Path.Combine("Assets", "Images", "Explosion.png"));
    }
    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
    }
    public override void Update() {
        player.Move();
        IterateShots();
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
    }
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                GameEvent MoveLeft = new GameEvent();
                MoveLeft.EventType = GameEventType.MovementEvent;
                MoveLeft.Message = "MoveLeft";
                eventBus.RegisterEvent(MoveLeft);
                break;
            case KeyboardKey.Right:
                GameEvent MoveRight = new GameEvent();
                MoveRight.EventType = GameEventType.MovementEvent;
                MoveRight.Message = "MoveRight";
                eventBus.RegisterEvent(MoveRight);
                break;
            case KeyboardKey.Space:
                Vec2F pShot = player.GetPosition();
                float sumExtent = player.GetExtent().X - PlayerShot.GetExtent().X;
                playerShots.AddEntity(new PlayerShot(new Vec2F(pShot.X + sumExtent / 2, pShot.Y), playerShotImage));
                break;
            case KeyboardKey.Escape:
                GameEvent windowClose = new GameEvent();
                windowClose.EventType = GameEventType.WindowEvent;
                windowClose.Message = "Close the window";
                eventBus.RegisterEvent(windowClose);
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        GameEvent MoveStop = new GameEvent();
        MoveStop.EventType = GameEventType.MovementEvent;
        MoveStop.Message = "MoveStop";
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
        switch (gameEvent.Message) {
            case "Close the window":
                window.CloseWindow();
                break;
            default:
                break;
        }
    }

    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1) {
                shot.DeleteEntity();
            } else {
            enemies.Iterate(enemy => {
                if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
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
}