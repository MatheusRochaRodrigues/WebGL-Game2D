using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

// [RequireComponent(typeof(AudioSource))]
public class AttackArea : MonoBehaviour
{
    public int damage = 1; //3
    public Collider2D col;
    public AudioClip hitSound; // Arraste o AudioSource no Inspector

    
    [Header("Shake")]
    public ShakeData shakeLight; 

    public void Start() {
        col = GetComponent<Collider2D>();
        this.gameObject.SetActive(false);
    }

    // private void OnTriggerEnter2D(Collider2D collider) { 
    //     if(collider.GetComponent<ILife>() != null)
    //     {  
    //         ILife health = collider.GetComponent<ILife>();
    //         health.isDamage(damage, this.transform, null);
    //         AudioSource.PlayClipAtPoint(hitSound, transform.position);
    //     } 

    //     this.gameObject.SetActive(false);  
    // }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<ILife>() != null)
        {
            if(collider.isTrigger && collider.GetComponent<TileLogic>() == null && collider.GetComponent<TileLogicInteract>() == null) return;

            
            CameraShakerHandler.Shake(shakeLight);

            ILife health = collider.GetComponent<ILife>();
            health.isDamage(damage, this.transform, null);

            // GetComponent<AudioSource>().PlayOneShot(hitSound); // Toca o som uma vez
            // AudioSource.PlayClipAtPoint(hitSound, transform.position); 
            // AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

            AudioSource tempAudio = new GameObject("TempAudio").AddComponent<AudioSource>();
            tempAudio.clip = hitSound;
            tempAudio.volume = 1.0f; // força volume máximo
            tempAudio.spatialBlend = 0f; // som 2D
            tempAudio.Play();
            Destroy(tempAudio.gameObject, hitSound.length);

            

            gameObject.SetActive(false);
        }
    }

 
}