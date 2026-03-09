using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject obj; 
    private bool active = false;
    
    public void Active()
    {
        active = !active;
        obj.SetActive(active);
    }
}
