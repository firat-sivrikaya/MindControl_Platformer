using UnityEngine;

namespace Model
{
    public class PlayerSpaceship : Spaceship
    {
        public PlayerSpaceship()
        {
            LoadPrefab("Done_Player");
            Instantiate();
            cannon = new Cannon(gameObject.transform, "Done_Bolt", 0.2f, 20);
            boundary = new Vector4(-6.9f, 6.9f, -4, 10);
        }
    }
}
 

