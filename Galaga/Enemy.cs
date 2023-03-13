using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private int hitPoints;
    private ImageStride alternativeImage;
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage alternativeImage)
        : base(shape, image) {
            hitPoints = 10;
            this.alternativeImage = alternativeImage as ImageStride;
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
        Image = alternativeImage;
        Shape.AsDynamicShape().Direction = new Vec2F(0f,0.1f);
    }
}