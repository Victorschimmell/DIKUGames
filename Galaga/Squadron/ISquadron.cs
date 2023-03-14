using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.MovementStrategy;
using System.Collections.Generic;

namespace Galaga.Squadron {
public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    IMovementStrategy Strategy {get;}
    void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride);

    void ChangeStrategy(IMovementStrategy newStrategy);
    void ChangeSpeed(Vec2F speed);
    }
}