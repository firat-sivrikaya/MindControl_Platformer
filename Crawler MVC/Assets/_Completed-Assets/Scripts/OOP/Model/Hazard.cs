using UnityEngine;

namespace Model
{
    public interface IHazard
    {
        bool CollisionDetection(Collider collider, System.Func<bool, bool> operation);
        Collider GetCollider();
        Cannon GetCannon();
        GameObject GetGameObject();
        int GetPoints();
        int GetKind();
        void Instantiate();
        void Init();
        void SetPosition();
        void Destroy();
    }
}




