using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{
    public class BlinkMonster : GameObjectBase
    {
        public int  x, z;

        public BlinkMonster(int x, int z)
        {
            this.x = x;
            this.z = z;
            LoadPrefab("BlinkMonster");
            SetPosition(x, z);
        }
        public void SetPosition(int x, int z)
        {
            position = new Vector3(x, 1, z);
        }
    }
}
