using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour
{
    public string _tagTarget = "Player";
    public List<Collider2D> detecObjs = new List<Collider2D>(); 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _tagTarget)
            detecObjs.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detecObjs.Remove(collision);
    }
}
