using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class TestPlayer {
        private Player player;

        [SetUp]
        public void InitiatePlayer() {
            player = new Player(
                new DynamicShape(new Vec2F(0.425f, 0.1f), new Vec2F(0.15f, 0.02f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, player);
        }

        [Test]
        public void TestCenterPosition() {
            // Center X-pos is 0.5f, player is 0.15f wide, and center should therefore be 0.425f
            Assert.That(player.GetShape().Position.ToString() == (
                new Vec2F(0.425f, 0.1f)).ToString());
        }

        [Test]
        public void TestMoveRight() {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveRight"
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.That(player.GetShape().Position.X > 0.425f);
        }

        [Test]
        public void TestWindowBoarderLimitRight() {
            for (int i = 0; i < 10000; i++) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveRight"
                });
                BreakoutBus.GetBus().ProcessEventsSequentially();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveAll"
                });
            }
            Assert.That(player.GetShape().Position.X < 1f);
        }

        [Test]
        public void TestMoveLeft() {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveLeft"
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent,
                Message = "MoveAll"
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.That(player.GetShape().Position.X < 0.425f);
        }

        [Test]
        public void TestWindowBoarderLimitLeft() {
            for (int i = 0; i < 10000; i++) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveLeft"
                });
                BreakoutBus.GetBus().ProcessEventsSequentially();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent,
                    Message = "MoveAll"
                });
            }
            Assert.That(player.GetShape().Position.X > 0f);
        }
    }
}