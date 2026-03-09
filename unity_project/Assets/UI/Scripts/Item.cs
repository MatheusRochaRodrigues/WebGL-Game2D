using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;  

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public string nameItem;
    public string nameItemBr = "";
    
    [SerializeField] public TileBase tile; // Para Tiles normais
    [SerializeField] public RuleTile ruleTile = null; // Para Rule Tiles

    public ItemType type;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite img;
    public Sprite Spawn;

    [Header("Seed Status")]
    public List<Sprite> seedGrown;
    public int stateGrown;
    public Item nextItem; // Novo campo para referenciar outro Item

    public int food = 1;

    public enum ItemType
    {
        Building,
        Tool,
        Object,
        Seed,
        Food
    }

    public TileBase GetTile()
    {
        return type == ItemType.Building && ruleTile != null ? ruleTile : tile;
    }

    public Item CreateCopy()
    {
        // Cria uma nova instância do Item
        Item newItem = ScriptableObject.CreateInstance<Item>();

        // Copiar dados simples
        newItem.nameItem = this.nameItem;
        newItem.type = this.type;
        newItem.stackable = this.stackable;
        newItem.img = this.img;
        newItem.Spawn = this.Spawn;
        newItem.stateGrown = this.stateGrown;

        // Copiar a lista de sementes (copia profunda)
        newItem.seedGrown = new List<Sprite>(this.seedGrown);

        // Copiar o próximo Item (referência direta)
        newItem.nextItem = this.nextItem;

        // Copiar tiles
        newItem.tile = this.tile;
        newItem.ruleTile = this.ruleTile;

        newItem.food = this.food;

        return newItem;
    }
}
