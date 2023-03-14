using Galaga.Squadron;
using DIKUArcade.Graphics;
using System.Collections.Generic;

using Galaga.MovementStrategy;

namespace Galaga.RandomSquadronCreater {
public class RSC1 : IRandomSquadronCreater {
    public ISquadron CreateSquad(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        System.Random random = new System.Random();
            int rand = random.Next(4);
            ISquadron squadron = new Squadron1(enemyStride, alternativeEnemyStride);
            switch (rand) {
                case 0:
                squadron = new Squadron1(enemyStride, alternativeEnemyStride);
                break;
                case 1:
                squadron = new Squadron2(enemyStride, alternativeEnemyStride);
                break;
                case 2:
                squadron = new Squadron3(enemyStride, alternativeEnemyStride);
                break;
                case 3:
                squadron = new Squadron4(enemyStride, alternativeEnemyStride);
                break;
            }
            rand = random.Next(2);
            switch (rand) {
                case 0:
                squadron.ChangeStrategy(new DownMove());
                break;
                case 1:
                squadron.ChangeStrategy(new ZigZag());
                break;
            }
            return squadron;
    } 
}
}