using Galaga;
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

namespace GalagaTests {
    [TestFixture]
    public class TestPlayer {
    
        public Player testPlayer;

        [SetUp]
        public void InitiatePlayer() {
            testPlayer = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, testPlayer);
        }

        [Test]
        public void TestNoMove() {
            Assert.That(testPlayer.GetShape().Position.ToString() == (new Vec2F(0.45f, 0.1f)).ToString());
        }

        [Test]
        public void TestMoveRight() {
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveRight"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(testPlayer.GetShape().Position.X > 0.45f);
        }

        [Test]
        public void TestMoveLeft() {
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveLeft"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(testPlayer.GetShape().Position.X < 0.45f);
        }

        [Test]
        public void TestMoveUp() {
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveUp"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(testPlayer.GetShape().Position.Y > 0.1f);
        }

        [Test]
        public void TestMoveDown() {
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveDown"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            GalagaBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(testPlayer.GetShape().Position.Y < 0.1f);
        }
    }   
}