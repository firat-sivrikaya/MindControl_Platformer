using UnityEngine;

namespace View
{
    class ViewText : GameObjectBase
    {
        public GUIText gUIText;

        public ViewText(string prefab)
        {
            LoadPrefab(prefab);
            Instantiate();
            gUIText = gameObject.GetComponent<GUIText>();
        }
    }
}
