using DIKUArcade.Entities;
using System;

namespace Galaga.MovementStrategy {
    public class ZigZagV2 : IMovementStrategy {
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => {
                MoveEnemy(enemy);
            });
        }

        public void MoveEnemy(Enemy enemy) {
            float sinCalc = (float) Math.Sin(1.5+(2f * Math.PI * (enemy.StartPos.Y - enemy.Shape.Position.Y))
                / 0.045f);
            enemy.Shape.AsDynamicShape().Direction.X = 0.01f *  sinCalc;
            enemy.Shape.Move();
        }
    }
}