
using UnityEngine;


namespace View
{
	public class Platform : GameObjectBase {

		protected Platform(){}

		public Platform(Transform modelTransform)
		{
			LoadPrefab("Platform");
			Instantiate(modelTransform);
		}

	}
}

