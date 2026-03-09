using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckList : MonoBehaviour
{
    TextMeshProUGUI text; 
    InventoryManager inventory; 
    int currentlevel = -1;
    public float typeDelay = 0.05f * 2;

    public AudioClip som; // Arraste o som aqui pelo Inspector
    public AudioClip somUpLevel; // Arraste o som aqui pelo Inspector

    void Awake() 
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 
        inventory = FindObjectOfType<InventoryManager>();

        // if(inventory.CheckLevels.Count == 0) this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlevel != inventory.currentLevel){
            finish = false;
            currentlevel = inventory.currentLevel;
            GetComponent<AudioSource>().PlayOneShot(somUpLevel); // Toca o som uma vez
            
            string txt = uptText();
            
            StartCoroutine(TypeText(txt)); 
        }

    }

    private string uptText(){ 
        string txt = "Lista de Tarefas \n";
        if(inventory.CheckLevels[currentlevel].itens.Count != 0 || inventory.CheckLevels[currentlevel].itensPut.Count != 0){
            foreach(var t in inventory.CheckLevels[currentlevel].itens){
                txt = txt + " Pegue ";
                txt = txt + t.countItem + " " + t.item.nameItemBr + "\n"; 
            } 
            txt = txt + "\n";
            foreach(var t in inventory.CheckLevels[currentlevel].itensPut){
                txt = txt + " Ponha ";
                txt = txt + t.countItem + " " + t.item.nameItemBr + "\n";
            } 
        }else{
            txt = txt + " Caçe todos \n";

            GameObject[] pig = GameObject.FindGameObjectsWithTag("Pig");
            GameObject[] chicken = GameObject.FindGameObjectsWithTag("Chicken");
            GameObject[] slime = GameObject.FindGameObjectsWithTag("Slime");
            GameObject[] skeleton = GameObject.FindGameObjectsWithTag("Skeleton");

            if (pig.Count() > 0) txt = txt + " porcos " + pig.Count() + "\n";
            if (chicken.Count() > 0) txt = txt + " galinhas " + chicken.Count() + "\n";
            if (slime.Count() > 0) txt = txt + " slimes " + slime.Count() + "\n";
            if (skeleton.Count() > 0) txt = txt + " skeleto " + skeleton.Count() + "\n";

            if (pig.Count() + chicken.Count() + slime.Count() + skeleton.Count() == 0)  txt = "Vá ao guia de instruções";
            //     inventory.CheckLevels[currentlevel].AlreadyLevelHunterAll = false; 
        }

        return txt;
    }

    public void updateCount(){ 
        if(!finish) return;
  
        GetComponent<AudioSource>().PlayOneShot(som); // Toca o som uma vez
        string txt = uptText();
        text.text = txt;
        text.maxVisibleCharacters = txt.Count();

    }

    private IEnumerator TypeText(string fullText) {
        text.text = fullText; 
        text.maxVisibleCharacters = ("Lista de Tarefas ").Count();      // text.maxVisibleCharacters = 0;
        for(int i = 0; i <= text.text.Length; i++) {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeDelay);
        }  
        finish = true;
    }

    public bool finish = false;

    public void OnDisable()
    {
        text.maxVisibleCharacters = text.text.Length;
        
    }
}
