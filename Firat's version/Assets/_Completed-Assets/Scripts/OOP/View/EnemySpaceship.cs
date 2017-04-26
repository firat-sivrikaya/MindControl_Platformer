using System.Collections.Generic;
using UnityEngine;

namespace View
{
    class EnemySpaceship : Spaceship
    {

        public EnemySpaceship(int kind)
        {
            if (kind == 1)
            {
                LoadPrefab("Done_Enemy Ship");
            }
            else
            {
                LoadPrefab("Done_Enemy Ship_2");
            }

            explosion = new Explosion("done_explosion_enemy");
            bolt = LoadReturnPrefab("Done_Bolt-Enemy");
        }
    }
}

