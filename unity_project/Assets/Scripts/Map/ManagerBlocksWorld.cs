using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class ManagerBlocksWorld : MonoBehaviour
{ 
    // public Sprite groundPlow;
    public RuleTile groundPlow;
    public static RuleTile _groundPlow;

    // public List<PlowSeed> plowSeed;
    // public PlowSeed plowSeed;
    // public static PlowSeed _plowSeed;

    [Space(), Header("Prefabs")]
    public GameObject prefabItem;

    public static ManagerBlocksWorld  worldSeed;

    public static Dictionary<Vector3Int, Item> TileSeeds = new Dictionary<Vector3Int, Item>();

    void Start()
    {
        worldSeed = this;
        _groundPlow = groundPlow;
        // _plowSeed = plowSeed;
    } 

    // public bool constainSeed(string seed){  
    //     if(plowSeed.nameSeed == seed) return true;
    //     return false;
    // } 

    // public int constainSeed(string seed){ 
    //     int i = 0;
    //     int j = -1;
    //     foreach (var plw in plowSeed){
    //         if(plw.nameSeed == seed){ 
    //             j = i;
    //             break;}
    //         i++;
    //     } 
    //     return i;
    // } 
}
