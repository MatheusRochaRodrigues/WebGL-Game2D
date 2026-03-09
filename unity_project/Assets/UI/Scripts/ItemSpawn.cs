using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawn : MonoBehaviour
{ 
    public SpriteRenderer _img;
    public Item _item; 
    public Rigidbody2D rigidbody;
    // public float velocityChase = 20f; // Velocidade do movimento
    public float velocityChase = 6; // Velocidade do movimento

    GameObject player;
 
    private float startY;
    public float amplitude = 0.5f; // Altura do movimento
    public float frequency = 2f; // Velocidade do movimento

    private float initRandom;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        rigidbody = GetComponent<Rigidbody2D>();


        startY = transform.position.y;
        setItem();

        initRandom = Random.Range(0.0f, 100.0f);
        // ChasePlayer();
    }
    public void setItem()
    { 
        _img.sprite = _item.img; 
    }
 
    public bool fsT = false;
    void FixedUpdate()
    {
        if(!fsT){
            fsT  =true;
            // Garante que o objeto tenha um deslocamento inicial pequeno, mas perceptível
            Vector2 randomForce = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            // rigidbody.velocity = randomForce; // Usa velocity para aplicar um deslocamento imediato
            rigidbody.MovePosition(rigidbody. position + randomForce);
        }
 
        if(player == null) return;
        if((this.transform.position - player.transform.position).sqrMagnitude < 20.0f ){ 

            Vector3 playerPos = player.transform.position;
            // playerPos.y += 0.7f; // Ajuste na altura, se necessário
            Vector2 direction = (playerPos - transform.position).normalized;

            float distance = Vector2.Distance(transform.position, playerPos);
            
            if (distance <= 0.2f)
            {
                player.GetComponent<PlayerController>().collectItem(_item); 
                Destroy(gameObject);
                return;
            }

            // Usa Lerp para suavizar a velocidade
            float speedFactor = Mathf.Clamp01(distance / 5f);
            float currentVelocity = Mathf.Lerp(2, velocityChase, speedFactor);
            
            rigidbody.velocity = direction * currentVelocity;

        } else{ 
            float newY = startY + Mathf.Sin((Time.time+initRandom) * frequency) * amplitude;
            rigidbody.MovePosition(new Vector2(transform.position.x, newY));
        }
    }
    
   
    
}







    // public void ChasePlayer(){
    //     start = true;
    //     StartCoroutine(Chase());
    // }

//  private IEnumerator Chase()
//     {
//         if (player == null) yield break; // Sai se não encontrar o player ou rigidbody

//         Vector2 direction;
//         float initialVelocity = velocityChase; // Velocidade inicial
//         float minDistance = 0.2f; // Distância mínima que a flecha deve atingir
//         float maxDistance = 5f; // Distância até a qual a velocidade será desacelerada completamente

//         while (player != null && player.activeInHierarchy)
//         {
//             Vector3 playerPos = player.transform.position;
//             playerPos.y += 0.7f; // Ajuste na posição para nivelar a altura (se necessário)
//             direction = playerPos - this.transform.position; // Direção até o jogador

//             float distance = direction.magnitude; // Calcula a distância até o jogador
//             if (distance <= minDistance)
//             {
//                 // Toca som e coleta o item
//                 player.GetComponent<PlayerController>().collectItem(_item); 
//                 break; // Sai do loop se o player estiver muito perto
//             }

//             // Calculando o fator de desaceleração com base na distância
//             float speedFactor = Mathf.Clamp01(distance / maxDistance); // Normaliza entre 0 e 1
//             float currentVelocity = Mathf.Lerp(2, initialVelocity, speedFactor); // Desacelera com base na distância

//             // Move a flecha com a nova velocidade ajustada
//             rigidbody.velocity = direction.normalized * currentVelocity;

//             yield return new WaitForSeconds(0.2f); // Ajuste conforme necessário
//         }

//         Destroy(this.gameObject); // Destroi o objeto após o fim do Chase
//     }