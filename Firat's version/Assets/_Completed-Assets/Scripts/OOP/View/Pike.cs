
using UnityEngine;


namespace View
{
    public class Pike : Platform
    {

        public Pike(Transform modelPlatform)
        {
            LoadPrefab("Pike");
            Instantiate(modelPlatform);
        }
    }
}
