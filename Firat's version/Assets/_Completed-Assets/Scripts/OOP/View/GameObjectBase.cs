using System;
using UnityEngine;

namespace View
{
    public abstract class GameObjectBase
    {
        public GameObject gameObject;
        public Explosion explosion;


        public void SetPrefab(GameObject prefab)
        {
            gameObject = prefab;
        }

        public void LoadPrefab(string path)
        {
            gameObject = (GameObject)Resources.Load("View/" + path, typeof(GameObject));
        }

        public virtual void Instantiate(Vector3 position, Quaternion rotation)
        {
            gameObject = MonoBehaviour.Instantiate(gameObject, position, rotation);
        }

        public virtual void Instantiate(Transform modelTransform)
        {
            gameObject = MonoBehaviour.Instantiate(gameObject, modelTransform);
            gameObject.transform.position = modelTransform.position;
            gameObject.name = gameObject.name + " View";
        }

        public void Instantiate()
        {
            gameObject = MonoBehaviour.Instantiate(gameObject);
            gameObject.name = gameObject.name + " View";
        }
            
        public virtual void Destroy(float lifeTime)
        {
            MonoBehaviour.Destroy(gameObject, lifeTime);
        }

        public static GameObject LoadReturnPrefab(string path)
        {
            return (GameObject)Resources.Load("View/" + path, typeof(GameObject));
        }
    }
}
