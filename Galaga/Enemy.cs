using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private int hitPoints;
    private List<Image> enemyStridesGreen;
    public Enemy(DynamicShape shape, IBaseImage image)
        : base(shape, image) {
            hitPoints = 10;
            enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets",
            "Images", "GreenMonster.png"));
        }

    public void TakeDamage() {
        hitPoints--;
        if (hitPoints <= 5) {
            Enrage();
        }
        if (hitPoints <= 0) {
            DeleteEntity();
        }
    }
    private void Enrage() {
        Image = new ImageStride(80, enemyStridesGreen);
        Shape.AsDynamicShape().Direction = new Vec2F(0f,0.1f);
    }
}