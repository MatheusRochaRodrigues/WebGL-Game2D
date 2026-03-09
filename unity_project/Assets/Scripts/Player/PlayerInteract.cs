using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;

public class PlayerInteract : MonoBehaviour
{
    public string nameList = "near";

    public static List<Collider2D> detecObjsNear = new List<Collider2D>();
    public static List<Collider2D> detecObjsFar = new List<Collider2D>();
    // private HashSet<GameObject> detecObjs = new HashSet<GameObject>();
    public List<Collider2D> currentList;
    public void Start()
    {
        if(nameList == "near")
            currentList = detecObjsNear;
        else
            currentList = detecObjsFar;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!currentList.Contains(collision)){
            currentList.Add(collision);
            // Debug.Log("Quantidade de objetos perto na área do jogador: " + currentList.Count);
            callJS("add", collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentList.Remove(collision);
        callJS("remove", collision.gameObject.name);
    }

    private void callJS(string op, string name){
        // Chamando um script JavaScript no navegador
        #if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalEval("parent.postMessage({ type: '"+nameList+"', data: '"+op+"', id: '"+name+"' }, '*');");
        #endif
    }


   


}













        // #if UNITY_WEBGL && !UNITY_EDITOR
        //     Application.ExternalEval("parent.postMessage('far', '*');");
        // #endif


        

        // if (detecObjs.Add(collision.gameObject)) // Só adiciona se ainda não estiver na lista
        // {
        //     Debug.Log("Quantidade de objetos na área do jogador: " + detecObjs.Count);
        // }