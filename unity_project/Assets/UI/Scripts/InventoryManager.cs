using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class lvl{
    public bool LevelReached = false;
    // public bool AlreadyLevelHunterAll = false;
    public List<lvlItem> itens;     //itens para entrar
    public List<lvlItem> itensPut;  //itens para sair
    public DialogueData lvl_DD;
}

[System.Serializable]
public class lvlItem{
    public int countItem;
    public Item item;
}

[RequireComponent(typeof(AudioSource))]
public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] _inventory;

    // public List<InventorySlot> _inventoryHandle = new List<InventorySlot>();
    private int _itemSelec = 0;

    public GameObject _inventoryItemPrefab;
    public GameObject _inventoryMenu;


    public Color select;
    // private Color deselect;

    // public int _maxStackLimitItens = 4;
    public int _maxStackLimitItens = 60;


    //new List for Hold Itens in Inventory
    // public List<InventorySlot> _inventoryHandle = new List<InventorySlot>(); 
    public Dictionary<string, DraggableItem> _inventoryItens = new Dictionary<string, DraggableItem>(); 

    public static InventoryManager invent;

    [Header("ItemSpawn")]
    //drop game obj
    public GameObject prefabItemSpawn;
    public static GameObject _prefabItemSpawn;

    //EVENTS level ==========================================================================================
    
    //Events
    public delegate void PopHandler(string text);
    public event PopHandler nextLevel;
    [Space()] [Header("SetupLevels")]
    public List< lvl > CheckLevels;
    public int currentLevel;

    public AudioClip somItem; // Arraste o som aqui pelo Inspector

    void Start()
    {
        _prefabItemSpawn = prefabItemSpawn;

        invent = this;
        
        _inventoryMenu.SetActive(true);
        _inventory = transform.parent.GetComponentsInChildren<InventorySlot>();
        // System.Array.Reverse(_inventory); // Inverte a ordem dos elementos 
        if (_inventory.Length >= 4)
        {
            var lastFour = _inventory[^4..]; // Pega os últimos 4 elementos
            var remaining = _inventory[..^4]; // Pega o restante dos elementos 
            _inventory = lastFour.Concat(remaining).ToArray(); // Junta os arrays
        }
        _inventoryMenu.SetActive(false);


        //handle hand
        // for(int i = _inventory.Length - 4; i < _inventory.Length; i++)
        // {
        //     _inventoryHandle.Add(_inventory[i]);
        // }

        // //update selection visual inventory
        // select = _inventory[0].img.color;
        // select.a = 1.0f;
        // deselect.a = 0.0f;
        
        // SelectItem(0);
    }

    // public Item SelectItem(int pos)
    // {  
    //     _inventoryHandle[_itemSelec].img.color = deselect;
    //     _itemSelec = pos;
    //     _inventoryHandle[_itemSelec].img.color = select;

    //     return getItemInventory();
    // }

    // public Item getItemInventory()
    // {
    //     return (_inventoryHandle[_itemSelec].transform.childCount == 0)
    //             ?  null
    //             : _inventoryHandle[_itemSelec].itemDrag._item;

    // }

    // public void AddItemListInventory(string name, DraggableItem drag){
    // if(!_inventoryItens.ContainsKey(name)) 
    //     _inventoryItens.Add(name, new List<DraggableItem>()); 
    // _inventoryItens[name].Add(drag);
    // } 

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) up();
    }

    public void up(){
        lvl level = CheckLevels[currentLevel]; 

        level.LevelReached = true;
        nextLevel?.Invoke("Parabens, voce desbloqueou novos blocos");
        CallJS();
        if(currentLevel+1 < CheckLevels.Count ){ 
            currentLevel++;
            FindObjectOfType<DialogueSystem>().ResetDD(CheckLevels[currentLevel].lvl_DD); } 

        FindObjectOfType<CheckList>().updateCount(); 
        if(currentLevel+1 == CheckLevels.Count) FindObjectOfType<CheckList>()?.gameObject.SetActive(false);
    }


    public void ManagerLevels(Item item, bool put = false){
        //new
        if(FindObjectOfType<EventSystemManager>().faseAllHunter == true) return;
        
        
        lvl level = CheckLevels[currentLevel];

        List<lvlItem> lista = (!put) ? level.itens : level.itensPut;  

        for(int i = 0; i < lista.Count; i++){  
            // para verificar o recolhimento de Itens se put for false; e se put for true é para a injeçao dos intes 
            lvlItem t = lista[i];                                                       
            if(item == t.item){ 
                if(t.countItem == 1)
                    lista.Remove(t);
                else
                    t.countItem--;

                break;
            }
        }


        if(level.LevelReached == false && level.itens.Count == 0 && level.itensPut.Count == 0){ 
            level.LevelReached = true;
            nextLevel?.Invoke("Parabens, voce desbloqueou novos blocos");
            CallJS();
            if(currentLevel+1 < CheckLevels.Count ){ 
                currentLevel++;
                FindObjectOfType<DialogueSystem>().ResetDD(CheckLevels[currentLevel].lvl_DD); }
        }
        
        FindObjectOfType<CheckList>()?.updateCount(); 
        
        if(currentLevel+1 == CheckLevels.Count) FindObjectOfType<CheckList>()?.gameObject.SetActive(false);
    }

    public void CallJS(){
        // Chamando um script JavaScript no navegador
        #if UNITY_WEBGL && !UNITY_EDITOR 
            Application.ExternalEval("parent.postMessage({ type: 'Level', data: 'L', id: 'L' }, '*');");
        #endif
    }
    

    public bool AddItem(Item item)
    {
        //check Level
        if(CheckLevels.Count != 0)
            ManagerLevels(item);

        
        GetComponent<AudioSource>().volume = 0.6f; // Toca o som uma vez
        GetComponent<AudioSource>().PlayOneShot(somItem); // Toca o som uma vez

        // Inventory
        for (int i = 0; i < _inventory.Length; i++)
        {
            InventorySlot slot = _inventory[i];
            DraggableItem slotItem = slot.GetComponentInChildren<DraggableItem>();
            if (
                item.stackable &&
                slotItem != null &&
                slotItem._item == item &&
                slotItem._count <= _maxStackLimitItens
                )
            {
                slotItem._count++;
                slotItem.refreshCountText();

                
                callJS("add", item.nameItem, 1);        //slotItem._count

                return true;
            }
        }

        for (int i = 0; i < _inventory.Length; i++)
        {
            InventorySlot slot = _inventory[i];
            DraggableItem slotItem = slot.GetComponentInChildren<DraggableItem>();
            if(slotItem == null) {
                //addition 
                _inventoryItens.Add(item.nameItem, SpawnItem(slot, item)); 

                //webgl 
                callJS("add", item.nameItem, 1);        //slotItem._count

                return true;
            }
        }   
        return false;
    }

    public bool GetItemBool(string item){
        if(_inventoryItens.ContainsKey(item)){
            DraggableItem slot = _inventoryItens[item];
            slot._count--;
            callJS("remove", slot._item.nameItem, 1);        //slotItem._count
            slot.refreshCountText();
            //item acabou
            if(slot._count <= 0){
                _inventoryItens.Remove(item);
                Destroy(slot.gameObject);
            }
            return true;
        }
        return false;
    }


    public Item getItem(string item){ 
        if(_inventoryItens.ContainsKey(item)){
            DraggableItem slot = _inventoryItens[item];
            Item it = slot._item;
            slot._count--;
            callJS("remove", slot._item.nameItem, 1);    
            slot.refreshCountText();
            //item acabou
            if(slot._count <= 0){
                _inventoryItens.Remove(item);
                Destroy(slot.gameObject);
            }

            // ManagerLevels(it, true);   // para verificar o recolhimento de Itens

            return it;
        }
        return null;
    }

    // void SpawnItem(InventorySlot slot, Item item)
    DraggableItem SpawnItem(InventorySlot slot, Item item)
    {
        GameObject obj = Instantiate(_inventoryItemPrefab, slot.transform);
        DraggableItem drag = obj.GetComponent<DraggableItem>();
        drag.setScripItem(item);

        return drag;
    }

    //WebGl 
    private void callJS(string op, string name, int itemCount){
        // Chamando um script JavaScript no navegador
        #if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval("parent.postMessage({ type: 'inventory', data: '"+op+"', id: '"+name+"', count: "+itemCount+" }, '*');");
        #endif
    }
}
