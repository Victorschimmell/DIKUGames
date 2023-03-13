using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class NoMove : IMovementStrategy
    {
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            //Do nothing
        }

        public void MoveEnemy(Enemy enemy)
        {
            //Do nothing
        }
    }
}