using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemBase))]

public class ItemBaseEditor : Editor
{
    private ItemBase _itemBase;

    private void Awake()
    {
        _itemBase = (ItemBase)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Item"))
        {
            _itemBase.CreateItem();
        }
        if (GUILayout.Button("Remove Item"))
        {
            _itemBase.RemoveItem();
        }
        if (GUILayout.Button("<="))
        {
            _itemBase.PrevItem();
        }
        if (GUILayout.Button("=>"))
        {
            _itemBase.NextItem();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }

}
