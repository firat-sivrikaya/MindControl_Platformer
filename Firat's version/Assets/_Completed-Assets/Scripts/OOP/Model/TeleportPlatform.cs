//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//Teleport platform should modify the coordinate of the player When it touch it.
//This option should be into the controller probably.

namespace Model
{
	public class TeleportPlatform : Platform 
	{
		int x, z;
		public int destination_x;
		public int destination_z;
        public int camera_x;
        public int camera_z;
		public TeleportPlatform(int length, int width, int x, int z, int dest_x, int dest_z, int camera_x, int camera_z)
		{
			LoadPrefab("TeleportPlatform");
			//position = new Vector3(x, 0, z);
			this.x = x;
			this.z = z;
            this.camera_x = camera_x;
            this.camera_z = camera_z;
			SetPosition(x, z);
			destination_x = dest_x;
			destination_z = dest_z;
			_length = length;
			_width = width;
		}
		public void SetScale()
		{
			rigidbody.transform.localScale = new Vector3 (_length, 1, _width);
		}

		public void SetPosition(int x, int z)
		{
			position = new Vector3(x, 0, z);
		}
	}
}