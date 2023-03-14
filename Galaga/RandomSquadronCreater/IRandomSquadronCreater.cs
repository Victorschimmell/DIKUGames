using Galaga.Squadron;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga.RandomSquadronCreater {
public interface IRandomSquadronCreater {
    ISquadron CreateSquad(List<Image> enemyStride, List<Image> alternativeEnemyStride);
}
}