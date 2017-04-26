using UnityEngine;


namespace Model
{
    public class Asteroid : GameObjectBase, IHazard
    {
        public Vector3 eulerAngles;
        public Vector3 rotationSpeed;
        public Vector2 range;
        int points;


        public Asteroid(int kind)
        {
            range = new Vector2(20, 80);

            switch (kind)
            {
                case 0:
                    LoadPrefab("Done_Asteroid 01");
                    break;

                case 1:
                    LoadPrefab("Done_Asteroid 02");
                    break;

                case 2:
                    LoadPrefab("Done_Asteroid 03");
                    break;
            }

            rotationSpeed = new Vector3(Random.Range(range.x, range.y), Random.Range(range.x, range.y), Random.Range(range.x, range.y));

            points = 10;
        }

        public void SetPosition()
        {
            position = new Vector3(Random.Range(-6, 6), 0, 16);
        }

        public void Init()
        {
            rigidbody.AddForce(
                gameObject.transform.TransformDirection(Vector3.forward) * -Random.Range(100, 150)
            );
        }

        public Cannon GetCannon()
        {
            return null;
        }

        public int GetPoints()
        {
            return points;
        }

        public int GetKind()
        {
            return 0;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
