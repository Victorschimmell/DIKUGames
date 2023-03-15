using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron;
public class Squadron2 : Base8Squadron {
    public Squadron2(List<Image> enemyStride, List<Image> alternativeEnemyStride): 
        base(enemyStride, alternativeEnemyStride) {}
    public override void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int i = 0; i < MaxEnemies; i++) {
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + 
                    (float)i * 0.1f, 1.1f - (i % 2) * 0.1f ), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride)));
        }
    }
}