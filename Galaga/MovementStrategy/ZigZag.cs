using DIKUArcade.Entities;
using System;

namespace Galaga.MovementStrategy {
    public class ZigZag : IMovementStrategy
    {
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(enemy => {
                MoveEnemy(enemy);
            });
        }

        public void MoveEnemy(Enemy enemy)
        {
            var direction = enemy.Shape.AsDynamicShape().Direction;
            enemy.Shape.Position.Y += direction.Y;
            float sinCalc = (float) Math.Sin((2f*Math.PI*(enemy.StartPos.Y - enemy.Shape.Position.Y))
                /0.045f);
            enemy.Shape.Position.X = enemy.StartPos.X + 0.05f *  sinCalc;
        }
    }
}