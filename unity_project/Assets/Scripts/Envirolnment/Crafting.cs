using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{ 
    public List<CraftingItem> crafts; 
    [HideInInspector]
    public InventoryManager inventory;

    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.M))
    //         TryCraft("Fence");
    // }

    public void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        
    }

    public void TryCraft (string item){
        CraftingItem cft = null;
        foreach (var t in crafts){
            if(t.nameItem == item) cft = t;
        }
        if(cft == null) return;

 
        foreach( var cardCraft in cft.craftItems )
            if (inventory._inventoryItens.ContainsKey(cardCraft.item.name)){
                if(inventory._inventoryItens[cardCraft.item.name]._count < cardCraft.count){ return; }  
            }
            else  return; 
         
        foreach( var cardCraft in cft.craftItems ){
            for (int i = 0; i < cardCraft.count; i++)
                inventory.getItem(cardCraft.item.name); 
        } 

        
        for (int i = 0; i < cft.almostItem; i++)
            inventory.AddItem(cft.nextItem); 

    }
}
