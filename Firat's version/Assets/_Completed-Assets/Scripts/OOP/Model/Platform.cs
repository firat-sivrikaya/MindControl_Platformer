using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{
	public class Platform : GameObjectBase {
		int length, width, x, z;
		// position is pre-defined
		// Use this for initialization


		public Platform( int length, int width, int x, int z)
		{
			this.length = length;
			this.width = width;
			this.x = x;
			this.z = z;
			LoadPrefab("Platform");
			SetPosition(x, z);
			Instantiate();
			position = new Vector3(x, 0, z);
		}

		public void SetPosition(int x, int z)
		{
			position = new Vector3(x, 0, z);
		}
	}
}

