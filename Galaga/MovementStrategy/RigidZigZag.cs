using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Galaga.MovementStrategy {
    public class RigidZigZag : IMovementStrategy {
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => {
                MoveEnemy(enemy);
            });
        }
        public void MoveEnemy(Enemy enemy) {
            float sinCalc = (float) Math.Sin(0.5+(2f * Math.PI * (enemy.StartPos.Y - enemy.Shape.Position.Y))
                / 0.09f);
            if (sinCalc <= 0) {
                sinCalc = -1f;
            }
            else {
                sinCalc = 1f;
            }
            enemy.Shape.AsDynamicShape().Direction.X = 0.004f * sinCalc;
            enemy.Shape.Move();
        }
    }
}