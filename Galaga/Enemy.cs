using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private int hitPoints;
    public int HitPoints {get {return hitPoints;}}
    private Vec2F startPos;
    public Vec2F StartPos => startPos;
    private ImageStride alternativeImage;
    private bool isEnraged;
    public bool IsEnraged {get {return isEnraged;}}
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage alternativeImage)
        : base(shape, image) {
            hitPoints = 3;
            this.alternativeImage = alternativeImage as ImageStride;
            Shape.AsDynamicShape().Direction = new Vec2F(0f, -0.001f);
            startPos = new Vec2F(Shape.Position.X, Shape.Position.Y);
            isEnraged = false;
        }

    public void TakeDamage() {
        hitPoints--;
        if (!isEnraged && hitPoints <= 2) {
            Enrage();
        }
        if (hitPoints <= 0) {
            DeleteEntity();
        }
    }
    public void Die() {
        hitPoints = 0;
        DeleteEntity();
    }
    private void Enrage() {
        Image = alternativeImage;
        Shape.AsDynamicShape().Direction *= new Vec2F(1f, 2f);
        isEnraged = true;
    }
}