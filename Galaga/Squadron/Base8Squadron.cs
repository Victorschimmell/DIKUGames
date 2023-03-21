using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using Galaga.MovementStrategy;
using DIKUArcade.Events;

namespace Galaga.Squadron {

/// <summary>
/// This is the base class for squads with 8 enemies. The CreateEnemies method simply needs to be 
/// overriden with a new implementation. This base class makes sure code does not need to be 
/// rewritten in all squads if there is an error in common code.
/// </summary>
public class Base8Squadron : ISquadron
{
    private EntityContainer<Enemy> enemies;
    private int maxEnemies;
    private IMovementStrategy strategy;
    private List<Image> alternativeEnemyStride;
    public EntityContainer<Enemy> Enemies => enemies;
    public IMovementStrategy Strategy => strategy;
    public int MaxEnemies => maxEnemies;

    public Base8Squadron(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        maxEnemies = 8;
        this.alternativeEnemyStride = alternativeEnemyStride;
        enemies = new EntityContainer<Enemy>(maxEnemies);
        CreateEnemies(enemyStride, alternativeEnemyStride);
        strategy = new NoMove();
    }
    public virtual void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {}
    public void ChangeStrategy(IMovementStrategy newStrategy) {
        strategy = newStrategy;
    }
    public void ChangeSpeed(Vec2F speed) {
        Enemies.Iterate(enemy => {
            enemy.Shape.AsDynamicShape().Direction += speed;
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