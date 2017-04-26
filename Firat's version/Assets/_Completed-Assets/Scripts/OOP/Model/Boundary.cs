using UnityEngine;

namespace Model
{
    class Boundary : GameObjectBase
    {
        public Boundary()
        {
            LoadPrefab("Boundary");
            position = gameObject.transform.position;
            Instantiate();
        }
    }
}
