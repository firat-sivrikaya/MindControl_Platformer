using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View
{
	public class Platform : GameObjectBase {

		public Platform(Transform modelTransform)
		{
			LoadPrefab("Platform");
			Instantiate(modelTransform);
		}

	}
}

