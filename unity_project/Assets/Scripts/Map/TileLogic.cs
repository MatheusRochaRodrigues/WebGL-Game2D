using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLogic : DamageDrop
{ 
    Vector3Int cellPosition;   

    // Start is called before the first frame update
    public void SetTileLogic(Vector3Int cellPosition, Item item)
    {
        this.cellPosition = cellPosition;
        life = 1;
        this.item = item;
        // ItemInWorld = item;
    }

    // private Vector2 GetFacingDirection()
    // {
    //     // Exemplo: baseado nas últimas teclas pressionadas ou no estado do personagem
    //     return FindObjectOfType<PlayerController>().ArrowDirection();
    // }
    // public float d = -0.4f;
    public override void isDamage(int damage, Transform colision, Item itemd = null){
        // Vector3 PLayer = colision.transform.position + new Vector3(0,d,0);
        // Vector2 directionToTile = (transform.position - PLayer).normalized;
        // // Vector2 directionToTile = (colision.transform.position - transform.position).normalized;
        // Vector2 facingDirection = GetFacingDirection(); // Sua função para obter direção do player (ex: (1,0), (0,1), etc)

        // float dot = Vector2.Dot(facingDirection, directionToTile);
        // Debug.Log(dot);

        // Debug.DrawLine(new Vector2(PLayer.x, PLayer.y), transform.position, Color.red, 1f);
        // Debug.DrawLine(new Vector2(PLayer.x, PLayer.y), new Vector2(PLayer.x, PLayer.y) + facingDirection, Color.blue, 1f);
        // Debug.Log("DirToTile: " + directionToTile + " | Facing: " + facingDirection + " | Dot: " + dot);

        // if (dot <= 0.75f) // 1 = exatamente na frente, 0.7 ~ 45 graus de abertura
        // {
        //     // Está dentro do cone na frente do player
        //     return;
        // }



        Tilemap tilemap = InteractMap._interactMap.tilemapPlow;  
        //sound 
        life--;  
        if(life <= 0){ 
            GetComponent<AudioSource>().PlayOneShot(SoundController._somDano); // Toca o som uma vez
            Sprite tileSprite = (tilemap.GetTile(cellPosition) as Tile)?.sprite; // Pega o TileBase na posição especificada 

            if(this.item.stateGrown == this.item.seedGrown.Count-1){  
                ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
                spawn._item = this.item.nextItem;
                spawn.setItem();
            } 
            // Apaga o tile na posição especificada 
            tilemap.SetTile(cellPosition, null);  // Isso remove o tile da posição especificada 
            InteractMap._interactMap.tilemapInteractable.SetTile(cellPosition, null);
            ManagerBlocksWorld.TileSeeds.Remove(cellPosition);
            
            if(!InteractMap._interactMap.availableTiles.ContainsKey(cellPosition))
                InteractMap._interactMap.availableTiles.Add(cellPosition, InteractMap._interactMap.tilemapInteractable.GetTile(cellPosition));
            // tilemap.SetColliderType(cellPosition, Tile.ColliderType.None);       // Se necessário, também pode remover o collider do tile
            
            Destroy(this.gameObject);
        } 
    }
}
