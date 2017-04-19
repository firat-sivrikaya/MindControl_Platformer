using UnityEngine;

using UnityEditor;
using System.Collections;

public class AddChild : ScriptableObject
{
    [MenuItem ("Tools/Add Child")]
    static void MenuAddChild()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

        foreach(Transform transform in transforms)
        {
            GameObject newChild = new GameObject("_Child");
            newChild.transform.parent = transform;
        }
    }
}