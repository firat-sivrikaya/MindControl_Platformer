using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
	public class MagicPlatform : MovingPlatform {

		public MagicPlatform(Transform modelTransform)
		{
			LoadPrefab("MagicPlatform");
			Instantiate(modelTransform);
		}

	}
}
