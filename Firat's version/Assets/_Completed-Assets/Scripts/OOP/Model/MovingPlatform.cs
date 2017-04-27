//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//Teleport platform should modify the coordinate of the player When it touch it.
//This option should be into the controller probably.

namespace Model
{
	public class MovingPlatform : Platform 
	{
        public bool right = true;
		public int origin_x, origin_z;
		public int _direction = 0; //0 = horizontal, 1 = vertical, define the direction of the movment
		public int _amplitude; //define the amplitude of the movment
		protected MovingPlatform() {}
		public MovingPlatform(int length, int width, int x, int z, int direction, int amplitude)
		{
			LoadPrefab("MovingPlatform");
			origin_x = x;
			this.z = z;
			//position = new Vector3(x, 0, z);
			SetPosition(x, z);
			_direction = direction;
			_amplitude = amplitude;
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