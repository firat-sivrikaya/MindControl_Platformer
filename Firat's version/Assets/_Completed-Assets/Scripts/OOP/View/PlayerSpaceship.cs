using UnityEngine;


namespace View
{
    public class PlayerSpaceship : Spaceship
    {
        public PlayerSpaceship(Transform modelTransform)
        {
            LoadPrefab("Done_Player");
            Instantiate(modelTransform);
            explosion = new Explosion("done_explosion_player");
           // bolt = LoadReturnPrefab("Done_Bolt");
        }
    }
}
 

