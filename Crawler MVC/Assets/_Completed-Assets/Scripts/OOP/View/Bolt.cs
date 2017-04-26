using UnityEngine;

namespace View
{
    public class Bolt : GameObjectBase
    {
        public Bolt(GameObject shot, Transform modelTransform)
        {
            SetPrefab(shot);
            Instantiate(modelTransform);
        }
    }
}

