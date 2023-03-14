using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.MovementStrategy;
using System.Collections.Generic;

namespace Galaga.Squadron {
public class Squadron4 : ISquadron
{
    private EntityContainer<Enemy> enemies;
    private int maxEnemies;

    private List<Image> alternativeEnemyStride;
    public EntityContainer<Enemy> Enemies => enemies;

    public int MaxEnemies => maxEnemies;

    private IMovementStrategy strategy;
    public IMovementStrategy Strategy => strategy;

    public Squadron4(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        maxEnemies = 16;
        this.alternativeEnemyStride = alternativeEnemyStride;
        enemies = new EntityContainer<Enemy>(maxEnemies);
        CreateEnemies(enemyStride, alternativeEnemyStride);
        strategy = new NoMove();
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int i = 0; i < maxEnemies/2; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride)));
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 1.1f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride)));
        }
    }

    public void ChangeStrategy(IMovementStrategy newStrategy) {
            strategy = newStrategy;
        }

    public void ChangeSpeed(Vec2F speed) {
        Enemies.Iterate(enemy => {
            enemy.Shape.AsDynamicShape().Direction = speed;
        });
    }
    }
}