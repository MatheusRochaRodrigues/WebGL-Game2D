using System.Collections;
using UnityEngine;

public class SleepAnimal : MonoBehaviour, ILife
{
    public float moveSpeed = 2f;
    public float fleeSpeed = 4f;
    public float changeDirectionTime = 4f;
    public float actionInterval = 3f;
    public float fleeDuration = 2f;
    public float sleepTime = 5f;
    public int maxHealth = 3;

    private int currentHealth;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 movement;
    private bool isMoving = false;
    private bool isFleeing = false;
    private bool isSleeping = false;
    private GameObject player;

    //drop
    public Item item;
    public int countDrop = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;

        StartCoroutine(BehaviorLoop());
    }

    void Update()
    {
        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        if (!isSleeping) // Só se move se não estiver dormindo
        {
            if (isMoving)
            {
                rb.velocity = movement * (isFleeing ? fleeSpeed : moveSpeed);
                spriteRenderer.flipX = movement.x > 0;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Mantém parado enquanto dorme
        }
    }

    IEnumerator BehaviorLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);

            if (!isFleeing && !isSleeping) // Só age normalmente se não estiver fugindo ou dormindo
            {
                if (Random.value > 0.2f)
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

                    yield return new WaitForSeconds(actionInterval);

                    float actionChoice = Random.value;
                    if (actionChoice > 0.6f)
                    {
                        if (actionChoice > 0.8f)
                        {
                            StartCoroutine(Sleep());
                        }
                        else
                        {
                            animator.SetTrigger("Action1");
                        }
                    }

                    
                }
            }
        }
    }
    public AudioClip sound; // Arraste o AudioSource no Inspector

    IEnumerator Sleep()
    {
        isSleeping = true;
        isMoving = false;
        animator.SetBool("isSleeping", true); // Ativa animação de sono
        rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(sleepTime);
        
        animator.SetBool("isSleeping", false); // Sai do modo de sono
        yield return new WaitForSeconds(0.5f);
        isSleeping = false;
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
         
            Die();
            return;
        }

        if (isSleeping)
        {
            isSleeping = false;
            animator.SetBool("isSleeping", false); // Sai da animação de sono
            animator.SetTrigger("WakeUp");
        }

        StartCoroutine(FleeAfterDamage());
    }

    IEnumerator FleeAfterDamage()
    {
        isFleeing = true;
        isSleeping = false;
        FleeFromPlayer();
        yield return new WaitForSeconds(fleeDuration);
        isFleeing = false;
    }

    void FleeFromPlayer()
    {
        if (player == null) return;
        isFleeing = true;
        isSleeping = false;
        movement = (transform.position - player.transform.position).normalized;
        isMoving = true;
    }

    void Die()
    {
        //Drop
        for(int i = 0; i < countDrop; i++){
            ItemSpawn spawn = Instantiate(InventoryManager._prefabItemSpawn, this.transform.position, this.transform.rotation).GetComponent<ItemSpawn>();
            spawn._item = item;
            spawn.setItem();
        }

        animator.SetTrigger("die");
        animator.Play("Death"); 
        isMoving = false;
        rb.velocity = Vector2.zero;
        // this.enabled = false;
        Destroy(gameObject, 1f);
        
        this.tag = "Untagged"; 
        FindObjectOfType<CheckList>()?.updateCount();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; 
        movement = (this.transform.position - collision.transform.position).normalized; 
        // Debug.Log(movement);
    }

}
