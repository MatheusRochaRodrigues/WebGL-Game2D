using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[RequireComponent(typeof(AudioSource))]
public class Tree : DamageDrop
{  
    public Sprite branch;

    public override void Start()
    {
        base.Start();
        GetComponent<AudioSource>().volume = 0.3f;
    }
 

    public override void isDamage(int damage, Transform colision, Item itemhand = null){
        if(hand != "" && PlayerController._itemInHandle != hand) return;

        life--; 
        StartCoroutine(Damage()); 
        if(life <= 0){
            GetComponent<AudioSource>().PlayOneShot(SoundController._somDano); // Toca o som uma vez

            StopAllCoroutines();
            //Drop
            for(int i = 0; i < countDrop; i++){
                ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
                spawn._item = item;
                spawn.setItem();
            }
            
            spRender.sprite = branch;
            Destroy(this.GetComponent<TransparentObject>());
            Destroy(this.GetComponent<CircleCollider2D>());
            spRender.color = color;
            Destroy(this.gameObject, 30);
            Destroy(this);
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
