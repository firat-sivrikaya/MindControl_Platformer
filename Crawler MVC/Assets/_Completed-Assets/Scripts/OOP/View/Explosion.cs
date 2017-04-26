using UnityEngine;

namespace View
{
    public class Explosion : GameObjectBase
    {
        public Explosion(string explosion)
        {
            LoadPrefab(explosion);
        }

        public void Explode(Vector3 position, Quaternion rotation)
        {
            Instantiate(position, rotation);
            Destroy(2);
        }
    }
}


