using UnityEngine;

namespace Model
{
    class EnemySpaceship : Spaceship, IHazard
    {
        public float targetManeuver;
        public float moveRateMax, moveRateMin;
        public Vector2 moveRate;
        public Vector2 nextMove;
        int points;
        public int kind;


        public EnemySpaceship()
        {
            kind = Random.Range(0, 2);

            if (kind == 1)
            {
                LoadPrefab("Done_Enemy Ship");
                points = 20;
                moveRateMax = 4f;
                moveRateMin = 2f;
            }
            else
            {
                LoadPrefab("Done_Enemy Ship_2");
                points = 30;
                moveRateMax = 2f;
                moveRateMin = 0f;
            }

            boundary = new Vector4(-6.9f, 6.9f, -15, 16);
        }

        public void SetPosition()
        {
            position = new Vector3(Random.Range(-6, 6), 0, 16);
        }

        public Cannon GetCannon()
        {
            return cannon;
        }

        public int GetPoints()
        {
            return points;
        }

        public int GetKind()
        {
            return kind;
        }

        public void Init()
        {
            cannon = new Cannon(gameObject.transform, "Done_Bolt-Enemy", 1.5f * kind + 1, -(kind == 0 ? 15 : 10));
            rigidbody.velocity = gameObject.transform.forward * -(kind == 0 ? 6 : 3);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}

