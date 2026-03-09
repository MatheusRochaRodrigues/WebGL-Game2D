using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public List<Item> foods; 

    public void TryEat (string item){
        Item fd = null;
        foreach (var t in foods){
            if(t.nameItem == item) fd = t;
        }
        if(fd == null) return;

        GetComponent<Life>().plusLife(fd.food);

    }
}
