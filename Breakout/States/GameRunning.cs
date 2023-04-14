using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.GUI;
using System.Collections.Generic;
using System.IO;

namespace Breakout.States{
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        void IGameState.HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            /*switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }*/
        }

        public void RenderState(){

        }

        public void ResetState(){

        }

        public void UpdateState(){

        }

        private void InitializeGameState() {

        }
    }
}