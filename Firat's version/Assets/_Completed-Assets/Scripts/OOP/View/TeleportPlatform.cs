using System.Collections;
using System.Collections.Generic;
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

