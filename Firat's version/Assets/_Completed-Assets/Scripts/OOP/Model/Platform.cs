using UnityEngine;


namespace Model
{
	public class Platform : GameObjectBase {
		public int _length, _width, x, z;
		// position is pre-defined
		// Use this for initialization
		protected Platform(){}

		public Platform( int length, int width, int x, int z)
		{
			this._length = length;
			this._width = width;
			this.x = x;
			this.z = z;
			LoadPrefab("Platform");
			SetPosition(x, z);
			//Instantiate();
			//position = new Vector3(x, 0, z);
			//SetScale (length, width);
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

