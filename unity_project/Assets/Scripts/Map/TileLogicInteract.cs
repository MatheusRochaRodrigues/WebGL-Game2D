using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class TileLogicInteract : DamageDrop
{ 
    Vector3Int cellPosition;   
 
    public void SetTileLogic(Vector3Int cellPosition, Item item)
    {
        this.cellPosition = cellPosition;
        life = 1;
        this.item = item; 
    }
  
    public override void isDamage(int damage, Transform colision, Item itemd = null){ 
        Tilemap tilemap = InteractMap._interactMap.tilemapInteractable;   
        //sound 
        life--;  
        if(life <= 0){  
            GetComponent<AudioSource>().PlayOneShot(SoundController._somDano); // Toca o som uma vez
            ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
            spawn._item = this.item;
            spawn.setItem(); 

            // Apaga o tile na posição especificada 
            tilemap.SetTile(cellPosition, null);  // Isso remove o tile da posição especificada 
            InteractMap._interactMap.tilemapInteractable.SetTile(cellPosition, null); 
            
            if(!InteractMap._interactMap.availableTiles.ContainsKey(cellPosition))
                InteractMap._interactMap.availableTiles.Add(cellPosition, InteractMap._interactMap.tilemapInteractable.GetTile(cellPosition));
             
            Destroy(this.gameObject);
        } 
    }
}
