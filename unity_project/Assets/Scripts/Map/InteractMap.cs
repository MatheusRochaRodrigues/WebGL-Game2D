using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class InteractMap : MonoBehaviour
{
    public Tilemap tilemapInteractable;

    public Tilemap trigger;

    public Dictionary<Vector3Int, TileBase> availableTiles = new Dictionary<Vector3Int, TileBase>();
    //public Dictionary<Vector3Int, TileBase> availableTilesSet;

    private List<Tilemap> tileMaps;
    private List<Tilemap> auxTiles;

    public TileBase tileb;

    //for plow
    public Tilemap tilemapPlow;

    public static InteractMap _interactMap;

    void Start()
    {
        _interactMap = this;
        //Tile tile = ScriptableObject.CreateInstance<Tile>();          AnimatedTile        CustomTile  RuleTile
        //tile.sprite = mySprite; // Sprite do tile

        tileMaps =   new List<Tilemap>( this.GetComponentsInChildren<Tilemap>() );
        auxTiles =   new List<Tilemap>(tileMaps);

        //primeiro filho
        auxTiles.Remove(foreachTile("Background", true));

        //interagivelTilemap aux;
        auxTiles.Remove(foreachTile("Interactable", true));

        foreach (Tilemap tilemap in auxTiles)
            foreachTile(tilemap.name, false);




        //percorrendo os tiles interativos e armazenando eles
        foreach (Vector3Int position in tilemapInteractable.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemapInteractable.GetTile(position);
            if (tile != null)  // Apenas armazena posições com tiles
            {
                availableTiles.Add(position, tile);
                //availableTiles.Add(position);

                tilemapInteractable.SetTile(position, null);
            }
        }

    }




    private Tilemap foreachTile(string name, bool intectable)
    {

        foreach (Tilemap tilemap in auxTiles)
        {
            if (tilemap.name == name)
            {
                foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
                {
                    TileBase tile = tilemap.GetTile(position);
                    if( tile != null )
                        if (intectable)
                        {
                            tilemapInteractable.SetTile(position, tileb);
                            // dados que camada interactive pode interagir DEBUG VISUAL 
                        }
                        else
                        {
                            tilemapInteractable.SetTile(position, null);
                        }
                }
                return tilemap;
            }
        }
        Debug.Log("Falha, tileMap nao foi encontrado");
        return null;

    }


    //Tile myTile = ScriptableObject.CreateInstance<Tile>();
    //myTile.sprite = mySprite; // Define o sprite do tile
    //myTile.color = Color.green; // Define a cor do tile
    //tilemap.SetTile(tilePosition, myTile);


}
