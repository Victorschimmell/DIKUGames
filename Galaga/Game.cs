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
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // Player
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        // EventBus
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent,  
            GameEventType.WindowEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        // Enemies
        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }
        // PlayerShot
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        // Explosions
        enemyExplosions = new AnimationContainer(numEnemies);
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
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveLeft(false);
                player.SetMoveRight(true);
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
        switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveLeft(false);
                player.SetMoveRight(false);
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
                        enemy.DeleteEntity();
                        AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                    }
                });
            }
        });
    }
    
    public void AddExplosion(Vec2F position, Vec2F extent) {
        StationaryShape explosion = new StationaryShape(position, extent);
        ImageStride strideExplosion = new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, strideExplosion);
    }
}