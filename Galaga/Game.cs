using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus;
    private Player player;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
    }
    public override void Render() {
    }
    public override void Update() {
        window.PollEvents();
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
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
    // TODO: switch on key string and disable the player's move direction
        switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveLeft(false);
                player.SetMoveRight(false);
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
        throw new System.NotImplementedException();
    }
}