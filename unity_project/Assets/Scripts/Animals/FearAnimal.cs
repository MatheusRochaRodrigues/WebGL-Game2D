using System.Collections;
using UnityEngine;

public class FearAnimal : MonoBehaviour, ILife
{
    public float moveSpeed = 2f; // Velocidade normal
    public float fleeSpeed = 4f; // Velocidade de fuga
    public float changeDirectionTime = 4f; // Tempo para mudar de direção
    public float actionInterval = 3f; // Tempo para ações aleatórias
    public float playerDetectionRange = 3f; // Distância para detectar o player
    public float fleeDuration = 2f; // Tempo que o animal foge após levar dano
    public int maxHealth = 3; // Vida máxima

    private int currentHealth;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 movement;
    private bool isMoving = false;
    private GameObject player;
    private bool isFleeing = false; // Flag para saber se está fugindo

    public Item egg;

    //drop
    public Item item;
    public int countDrop = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(BehaviorLoop());
    }

    void Update()
    {
        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        if (isFleeing || PlayerIsNear()) 
        {
            FleeFromPlayer();
        }

        if (isMoving)
        {
            rb.velocity = movement * (isFleeing ? fleeSpeed : moveSpeed);

            // Faz FlipX apenas se a direção horizontal mudar
            if (movement.x > 0)
                spriteRenderer.flipX = true;  // Olha para a direita
            else if (movement.x < 0)
                spriteRenderer.flipX = false; // Olha para a esquerda
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator BehaviorLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);

            if (!isFleeing) // Só age normalmente se não estiver fugindo
            {
                if (Random.value > 0.2f) // 50% de chance de andar normalmente
                {
                    movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                    isMoving = true;
                    if (Random.value > 0.95f)
                    { 
                        AudioSource tempAudio = new GameObject("TempAudio").AddComponent<AudioSource>();
                        tempAudio.clip = sound;
                        tempAudio.volume = 0.6f; // força volume máximo
                        tempAudio.spatialBlend = 0f; // som 2D
                        tempAudio.Play();
                        Destroy(tempAudio.gameObject, sound.length);
                    }
                }
                else
                {
                    isMoving = false;
                    rb.velocity = Vector2.zero;

                    yield return new WaitForSeconds(actionInterval); // Espera um tempo para a ação

                    if (Random.value > 0.6f) // 40% de chance de fazer uma ação
                    {
                        animator.SetTrigger("Action");
                    }

                    if(Random.value > 0.80f){
                        ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
                        spawn._item = egg;
                        spawn.setItem();
                    }
                }

            }
        }
    }
    public AudioClip sound; // Arraste o AudioSource no Inspector

    bool PlayerIsNear()
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) < playerDetectionRange;
    }

    void FleeFromPlayer()
    {
        if (player == null) return;
        isFleeing = true; // Ativa modo de fuga
        Vector2 fleeDirection = (transform.position - player.transform.position).normalized;
        movement = fleeDirection;
        isMoving = true;
    }

    bool once = false;
    public void isDamage(int damage, Transform colision, Item itemd = null)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
        animator.Play("Damage"); 

        if (currentHealth <= 0)
        {
            if(once) return;
            once = true;
            
            //Drop
            for(int i = 0; i < countDrop; i++){
                ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
                spawn._item = item;
                spawn.setItem();
            }

            Die();
            return;
        }

        StartCoroutine(FleeAfterDamage());
    }

    IEnumerator FleeAfterDamage()
    {
        isFleeing = true;
        FleeFromPlayer();
        yield return new WaitForSeconds(fleeDuration);
        isFleeing = false; // Volta ao comportamento normal depois do tempo de fuga
    }

    void Die()
    {
        animator.SetTrigger("die");
        animator.Play("Death"); 
        isMoving = false;
        rb.velocity = Vector2.zero;
        Destroy(gameObject, 1f);
        this.tag = "Untagged"; 
        FindObjectOfType<CheckList>()?.updateCount();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ChangeDirection();
        movement = (this.transform.position - collision.transform.position).normalized; 
    }

    void ChangeDirection()
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
