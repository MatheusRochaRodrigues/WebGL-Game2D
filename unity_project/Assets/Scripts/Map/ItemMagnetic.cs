using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnetic : MonoBehaviour
{
    public float attractionSpeed = 5f;    // Velocidade de atração
    public Coroutine myCoroutine = null; 

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // O Tilemap da água deve ter a tag "Water"
        {
            transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, attractionSpeed * Time.deltaTime);

            if((transform.position).sqrMagnitude - (collision.transform.position).sqrMagnitude < 1)
            {
                if (myCoroutine != null)
                {
                    StopCoroutine(myCoroutine);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public static IEnumerator JumpCollectible(Sprite drop, GameObject collectible)
    {
        Destroy(collectible, 30f);

        collectible.GetComponent<SpriteRenderer>().sprite = drop;


        float jumpHeight = 1.0f;
        float jumpDuration = 0.05f;

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        Vector3 startPosition = collectible.transform.position; // Posição inicial
        Vector3 targetPosition = startPosition + randomDirection * jumpHeight; // Posição alvo para o salto
        float elapsedTime = 0f;

        // Enquanto o tempo de salto não for alcançado
        while (elapsedTime < jumpDuration)
        {
            // Interpola a posição do objeto para criar um efeito de salto
            collectible.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / jumpDuration);

            // Atualiza o tempo de transição
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Garante que o objeto chegue exatamente à posição alvo
        collectible.transform.position = targetPosition;

        // Agora vamos fazer ele voltar para a posição original (opcional)
        yield return new WaitForSeconds(0.1f); // Atraso antes de retornar

        elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            collectible.transform.position = Vector3.Lerp(targetPosition, startPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Garante que o objeto retorne à posição original
        collectible.transform.position = startPosition;
    }

}
