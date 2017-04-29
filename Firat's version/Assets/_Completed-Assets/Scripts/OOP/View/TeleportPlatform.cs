
using UnityEngine;


namespace View
{
	public class TeleportPlatform : Platform {

		public TeleportPlatform(Transform modelTransform)
		{
			LoadPrefab("TeleportPlatform");
			Instantiate(modelTransform);
		}

	}
}

