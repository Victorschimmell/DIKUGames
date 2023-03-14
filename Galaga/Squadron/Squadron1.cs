using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using Galaga.MovementStrategy;
using DIKUArcade.Events;

namespace Galaga.Squadron {
public class Squadron1 : ISquadron
{
    private EntityContainer<Enemy> enemies;
    private int maxEnemies;
    private IMovementStrategy strategy;
    private List<Image> alternativeEnemyStride;
    public EntityContainer<Enemy> Enemies => enemies;
    public IMovementStrategy Strategy => strategy;
    public int MaxEnemies => maxEnemies;

    public Squadron1(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        maxEnemies = 8;
        this.alternativeEnemyStride = alternativeEnemyStride;
        enemies = new EntityContainer<Enemy>(maxEnemies);
        CreateEnemies(enemyStride, alternativeEnemyStride);
        strategy = new NoMove();
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int i = 0; i < maxEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 1f), new Vec2F(0.1f, 0.1f)),
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
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.MovementEvent) {
            switch (gameEvent.Message) {
                case "MoveAll":
                Strategy.MoveEnemies(Enemies);
                break;
            }
        }
    }
    }
}