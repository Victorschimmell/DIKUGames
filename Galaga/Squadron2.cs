using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron {
public class Squadron2 : ISquadron
{
    private EntityContainer<Enemy> enemies;
    private int maxEnemies;

    private List<Image> alternativeEnemyStride;
    public EntityContainer<Enemy> Enemies => enemies;

    public int MaxEnemies => maxEnemies;

    public Squadron2(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        maxEnemies = 8;
        this.alternativeEnemyStride = alternativeEnemyStride;
        enemies = new EntityContainer<Enemy>(maxEnemies);
        CreateEnemies(enemyStride, alternativeEnemyStride);
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride)
    {
        for (int i = 0; i < maxEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f - (i % 2) * 0.1f ), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride)));
        }
    }
}
}