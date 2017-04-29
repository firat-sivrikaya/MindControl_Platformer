
using UnityEngine;

namespace View
{
	public class MovingPlatform : Platform {
		protected MovingPlatform() {}
		public MovingPlatform(Transform modelTransform)
		{
			LoadPrefab("MovingPlatform");
			Instantiate(modelTransform);
		}

	}
}

