using UnityEngine;

[System.Serializable]
public class buyItem{
    public int countItem;
    public Item item;
}

public class BuySystem : MonoBehaviour
{
    public GameObject invent;
    public InventoryManager inventoryManager;
    private bool isInsideTrigger = false;
    private bool active = false;

    public Item item1;
    public Item item2;
    public Item item3;

    void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            active = !active;
            invent.SetActive(active);
        }
        if (isInsideTrigger && active)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                // inventoryManager.getItem(item1);
                inventoryManager.AddItem(item1);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2)){
                inventoryManager.AddItem(item2); 
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)){
                inventoryManager.AddItem(item3); 
            }
        }
        
        if (active && !isInsideTrigger)
        {
            active = !active;
            invent.SetActive(active);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
            isInsideTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
            isInsideTrigger = false;
    }
}
