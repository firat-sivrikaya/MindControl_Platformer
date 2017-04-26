using UnityEngine;

namespace Model
{
    public class Bolt : GameObjectBase
    {
        public Bolt(GameObject shot, float power, Transform otherTransform)
        {
            SetPrefab(shot);
            position = otherTransform.position;
            Instantiate();
            rigidbody.velocity = gameObject.transform.forward * power;
        }
    }
}

