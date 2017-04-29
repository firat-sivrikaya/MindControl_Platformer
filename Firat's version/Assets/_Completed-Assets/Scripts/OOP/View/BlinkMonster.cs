
using UnityEngine;

namespace View
{
    public class BlinkMonster : GameObjectBase
    {
        public BlinkMonster(Transform modelTransform)
        {
            LoadPrefab("BlinkMonster");
            Instantiate(modelTransform);
        }

    }
}
