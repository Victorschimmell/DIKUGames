using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron;
public class Squadron3 : Base8Squadron {
    public Squadron3(List<Image> enemyStride, List<Image> alternativeEnemyStride): 
        base(enemyStride, alternativeEnemyStride) {}
    public override void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int i = 0; i < MaxEnemies; i++) {
            if (i <= MaxEnemies / 2 - 1) {
                Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 1.15f - (i / 20f) ), 
                    new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride)));
            }
            else {
                Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 1.15f - ((MaxEnemies-i-1) / 20f)), 
                    new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride)));
            }
        }
    }
}