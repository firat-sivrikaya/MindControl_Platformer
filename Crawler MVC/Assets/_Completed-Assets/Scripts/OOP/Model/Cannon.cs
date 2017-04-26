using UnityEngine;
using System.Collections.Generic;

namespace Model
{
    public class Cannon : GameObjectBase
    {
        public float fireRate;
        public float nextFire;
        GameObject bolt;
        public List<Bolt> bolts;
        Transform otherTransform;
        float power;


        public Cannon(Transform otherTransform, string prefab, float fireRate, float power)
        {
            bolt = LoadReturnPrefab(prefab);
            this.fireRate = fireRate;
            this.power = power;
            this.otherTransform = otherTransform;
            bolts = new List<Bolt>();
        }

        public void Fire()
        {
            bolts.Add(new Bolt(bolt, power, otherTransform));
        }

        public void DestroyBolt(int index)
        {
            bolts.RemoveAt(index);
        }
    }
}

