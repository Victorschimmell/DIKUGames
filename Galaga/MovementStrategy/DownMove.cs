using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class DownMove : IMovementStrategy
    {
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(enemy => {
                MoveEnemy(enemy);
            });
        }

        public void MoveEnemy(Enemy enemy)
        {
            enemy.Shape.Move();
        }
    }
}