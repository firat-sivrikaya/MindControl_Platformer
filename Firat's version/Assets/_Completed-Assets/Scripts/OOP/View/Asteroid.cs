using UnityEngine;

namespace View
{
    public class Asteroid : GameObjectBase
    {
        public Asteroid(int kind)
        {
            
            switch (kind)
            {
                case 0:
                    LoadPrefab("Done_Asteroid 01");
                    break;

                case 1:
                    LoadPrefab("Done_Asteroid 02");
                    break;

                case 2:
                    LoadPrefab("Done_Asteroid 03");
                    break;
            }

            explosion = new Explosion("done_explosion_asteroid");
        }
    }
}
