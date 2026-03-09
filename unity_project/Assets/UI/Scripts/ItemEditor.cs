#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item)target;

        item.nameItem = EditorGUILayout.TextField("Item Name", item.nameItem);
        item.nameItemBr = EditorGUILayout.TextField("Item Name Br", item.nameItemBr);
        item.type = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", item.type);

        // Se o tipo for "Building", mostra RuleTile e esconde os outros
        if (item.type == Item.ItemType.Building)
        {
            item.ruleTile = (RuleTile)EditorGUILayout.ObjectField("Rule Tile", item.ruleTile, typeof(RuleTile), false);
        }
        // Se for do tipo Object, mostra "seedGrown" e "stateGrown"
        else if (item.type == Item.ItemType.Seed)
        {
            // SerializedProperty seedGrownProp = serializedObject.FindProperty("seedGrown");
            // EditorGUILayout.PropertyField(seedGrownProp, new GUIContent("Seed Grown"), true);

            SerializedProperty seedGrownProp = serializedObject.FindProperty("seedGrown");
            EditorGUILayout.PropertyField(seedGrownProp, true);

            

            item.stateGrown = EditorGUILayout.IntField("State Grown", item.stateGrown);
             // Novo campo para referenciar outro item
            item.nextItem = (Item)EditorGUILayout.ObjectField("Reference Item", item.nextItem, typeof(Item), false);
        }
        //food
        else if (item.type == Item.ItemType.Food)
        {
            item.food = EditorGUILayout.IntField("Regeneration Food", item.food);
        }
        // Se for um tipo diferente de "Object", mostra TileBase
        else
        {
            item.tile = (TileBase)EditorGUILayout.ObjectField("Tile", item.tile, typeof(TileBase), false);
        }
 
        item.img = (Sprite)EditorGUILayout.ObjectField("Image", item.img, typeof(Sprite), false);
        item.Spawn = (Sprite)EditorGUILayout.ObjectField("Spawn", item.Spawn, typeof(Sprite), false);
        item.stackable = EditorGUILayout.Toggle("Stackable", item.stackable);

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }
}

#endif