using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron;
public class Squadron4 : Base8Squadron {
    public Squadron4(List<Image> enemyStride, List<Image> alternativeEnemyStride): 
        base(enemyStride, alternativeEnemyStride) {}
    public override void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int j = 0; j < 2; j++) {
            for (int i = 0; i < MaxEnemies/2; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.2f, 1f + 0.1f * j), 
                        new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride)));
            }
        }
    }
}