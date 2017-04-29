using UnityEngine;


namespace View
{
    public class PlayerCharacter : GameObjectBase
    {
        public PlayerCharacter(Transform modelTransform)
        {
            LoadPrefab("Done_Player");
            Instantiate(modelTransform);
            //explosion = new Explosion("done_explosion_player");
           // bolt = LoadReturnPrefab("Done_Bolt");
        }
    }
}
 

