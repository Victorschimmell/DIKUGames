using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.GUI;
using System.Collections.Generic;
using Galaga.RandomSquadronCreater;
using Galaga.GameText;
using Galaga.Squadron;
using System.IO;



namespace Galaga.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private ISquadron squadron;
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
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            GameRunning.instance.ResetState();
            return GameRunning.instance;
        }

        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateMovementEvent("MoveLeft"));
                    break;
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateMovementEvent("MoveRight"));
                    break;
                case KeyboardKey.Up:
                    GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateMovementEvent("MoveUp"));
                    break;
                case KeyboardKey.Down:
                    GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateMovementEvent("MoveDown"));
                    break;
                case KeyboardKey.Space:
                    Vec2F pShot = player.GetPosition();
                    float sumExtent = player.GetExtent().X - PlayerShot.GetExtent().X;
                    PlayerShot newPS = new PlayerShot(new Vec2F(pShot.X + sumExtent / 2, pShot.Y), 
                        playerShotImage);
                    GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, newPS);
                    playerShots.AddEntity(newPS);
                    break;
                case KeyboardKey.Escape:
                    GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateWindowEvent("CloseWindow"));
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key) {
            GameEvent MoveStopLeftRight = GameEventCreator.CreateMovementEvent("MoveStopLeftRight");
            GameEvent MoveStopUpDown = GameEventCreator.CreateMovementEvent("MoveStopUpDown");
            switch (key) {
                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(MoveStopLeftRight);
                    break;
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(MoveStopLeftRight);
                    break;
                case KeyboardKey.Up:
                    GalagaBus.GetBus().RegisterEvent(MoveStopUpDown);
                    break;
                case KeyboardKey.Down:
                    GalagaBus.GetBus().RegisterEvent(MoveStopUpDown);
                    break;
                case KeyboardKey.Space:
                    break;
                case KeyboardKey.Escape:
                    break;
            }
        }
        void IGameState.HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        public void RenderState() {
            player.Render();
            squadron.Enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
            health.RenderGameText();
            roundCounter.RenderGameText();
        }

        public void ResetState() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            // EventBus
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, player);
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, player);
            // Enemies
            enemyStridesBlue = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets",
                "Images", "GreenMonster.png"));
            enemySpeed = new Vec2F(0f, 0f);
            // PlayerShot
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            // Explosions
            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets", "Images", "Explosion.png"));
            enemyExplosions = new AnimationContainer(8);
            //GameTexts
            health = new Health(new Vec2F(0.02f, -0.4f), new Vec2F(0.5f, 0.5f));
            roundCounter = new RoundCounter(new Vec2F(0.02f, 0.5f), new Vec2F(0.5f, 0.5f));
            //SquadCreator
            squadCreator = new RSC1();
        }

        public void UpdateState() {
            UpdateSquadron();
            GalagaColltionDetection();
            GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateMovementEvent("MoveAll"));
            UpdateHealth();
        }

        private void InitializeGameState() {
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                if (shot.Shape.Position.Y > 1) {
                    GalagaBus.GetBus().Unsubscribe(GameEventType.MovementEvent, shot);
                    shot.DeleteEntity();
                } else {
                squadron.Enemies.Iterate(enemy => {
                if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                    GalagaBus.GetBus().Unsubscribe(GameEventType.MovementEvent, shot);
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
        private void PlayerCollideWithEnemy() {
            squadron.Enemies.Iterate(enemy => {
                if (CollisionDetection.Aabb(player.GetShape(), enemy.Shape).Collision) {
                    IsGameOver(3);
                    AddExplosion(player.Shape.Position, player.Shape.Extent);
                }
                });
        }
        private void EnemyCollideWithPlayer() {
            squadron.Enemies.Iterate(enemy => {
                if (CollisionDetection.Aabb(enemy.Shape.AsDynamicShape(), player.Shape).Collision) {
                    IsGameOver(3);
                    AddExplosion(player.Shape.Position, player.Shape.Extent);
                }
                });
        }
        private void GalagaColltionDetection() {
            IterateShots();
            PlayerCollideWithEnemy();
            EnemyCollideWithPlayer();
        }
        private void IsGameOver(int amount) {
            if (health.LoseHealth(amount) <= 0) {
                GalagaBus.GetBus().RegisterEvent(GameEventCreator.CreateGameStateEvent("GameOver"));
            }
        }
        private void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape explosion = new StationaryShape(position, extent);
            ImageStride strideExplosion = new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides);
            enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, strideExplosion);
        }

        private void UpdateHealth() {
            squadron.Enemies.Iterate(enemy => {
                if (enemy.Shape.Position.Y < 0) {
                    IsGameOver(1);
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
                    GalagaBus.GetBus().Unsubscribe(GameEventType.GameStateEvent, squadron);
                    GalagaBus.GetBus().Unsubscribe(GameEventType.MovementEvent, squadron);
                }
                squadron = squadCreator.CreateSquad(enemyStridesBlue, enemyStridesGreen);   
                enemySpeed += new Vec2F(0f, -0.00025f);
                squadron.ChangeSpeed(enemySpeed);
                GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, squadron);
                GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, squadron);
                roundCounter.IncrementRound();
            }
        }
    }
}