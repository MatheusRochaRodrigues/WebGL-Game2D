using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[RequireComponent(typeof(AudioSource))]
public class DamageDrop : MonoBehaviour, ILife
{
    public int life = 5;
    public SpriteRenderer spRender;
    // public GameObject prefabItem;
    public Item item; 
    
    public float shakeDuration = 0.2f;  // Duração do tremor
    public float shakeMagnitude = 0.1f; // Intensidade do tremor

    protected Vector3 originalPosition;
    protected Color color;

    public string hand = "";
    public int countDrop = 2;

    // Start is called before the first frame update
    public virtual void Start() {
        originalPosition = transform.position;
        spRender = GetComponent<SpriteRenderer>();
        if(spRender == null) gameObject.AddComponent<SpriteRenderer>(); 
        color = spRender.color;

        GetComponent<AudioSource>().volume = 0.3f;

    } 

    public virtual void isDamage(int damage, Transform colision, Item itemd = null){
        if(hand != "" && PlayerController._itemInHandle != hand) return;
        
        life--; 
        StartCoroutine(Damage()); 
        if(life <= 0){
            GetComponent<AudioSource>().PlayOneShot(SoundController._somDano); // Toca o som uma vez
            StopAllCoroutines();//Drop
            for(int i = 0; i < countDrop; i++){
                ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
                spawn._item = item;
                spawn.setItem();
            }
            Destroy(this.gameObject);
        }

        IEnumerator Damage(){   
            spRender.color = Color.red;  
            yield return new WaitForSeconds(0.1f);  
            spRender.color = color;

            
            // IEnumerator ShakeTree()
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
                float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

                transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = originalPosition; // Volta à posição original
        }  
    }
}
