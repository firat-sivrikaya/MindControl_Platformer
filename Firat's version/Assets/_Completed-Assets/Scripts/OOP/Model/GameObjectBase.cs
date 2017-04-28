using UnityEngine;


namespace Model
{
    public abstract class GameObjectBase
    {
        public GameObject gameObject;
        public Quaternion orientation;
        public Vector3 position, velocity;
        public Collider collider;
        public Rigidbody rigidbody;
        public AudioSource audioSource;


        public void SetPrefab(GameObject prefab)
        {
            gameObject = prefab;
        }

        public void LoadPrefab(string path)
        {
            gameObject = (GameObject)Resources.Load("Model/" + path, typeof(GameObject));
        }

        public bool CollisionDetection(Collider collider, System.Func<bool, bool> @operator)
        {
            if (collider != null && this.collider != null && @operator(Intersects(collider)))
            {
               // Destroy();

                return true;
            }

            return false;
        }

        public virtual void Instantiate()
        {
            gameObject = MonoBehaviour.Instantiate(gameObject, position, orientation);
            gameObject.name = gameObject.name + " Model";
            collider = gameObject.GetComponent<Collider>();
            rigidbody = gameObject.GetComponent<Rigidbody>();
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public virtual void Destroy()
        {
            MonoBehaviour.Destroy(gameObject);
        }

        public virtual void Destroy(float lifeTime)
        {
            MonoBehaviour.Destroy(gameObject, lifeTime);
        }

        public bool Intersects(Collider collider)
        {
            return this.collider.bounds.Intersects(collider.bounds);
        }

        public Collider GetCollider()
        {
            return collider;
        }

        public static GameObject LoadReturnPrefab(string path)
        {
            return (GameObject)Resources.Load("Model/" + path, typeof(GameObject));
        }
    }
}
