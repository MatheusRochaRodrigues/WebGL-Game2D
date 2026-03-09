using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[Serializable]
public struct craft {
    public Item item;
    public int count;
} 

[CreateAssetMenu(menuName = "Item/CraftingItem")]
public class CraftingItem : ScriptableObject
{
    [Header("Crafting")]
    public string nameItem;
    public List<craft> craftItems; 
    public Item nextItem; // Novo campo para referenciar outro Item
    public uint almostItem = 1;
 

    public CraftingItem CreateCopy()
    {
        // Cria uma nova instância do Item
        CraftingItem newItem = ScriptableObject.CreateInstance<CraftingItem>();

        // Copiar dados simples
        newItem.nameItem = this.nameItem;  

        // Copiar a lista de sementes (copia profunda)
        newItem.craftItems = new List<craft>(this.craftItems);

        // Copiar o próximo Item (referência direta)
        newItem.nextItem = this.nextItem; 

        return newItem;
    }
}
