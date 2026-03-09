using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour, ILife
{
    public float speed = 3.5f;
    private Vector2 direction;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;

    public AreaDetection areaDetection;
    private Animator anim;

    public int life = 4;
    public bool isDam = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    public void isDamage(int Damage, Transform colision, Item item = null){
        life -= Damage;
        if(life <= 0){
            anim.SetInteger("Move", 3);
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(this.gameObject, 4.0f);
            rigidbody.isKinematic = true;
            this.enabled = false;
            GetComponent<Damage>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.tag = "Untagged";
 
            FindObjectOfType<CheckList>()?.updateCount();  

        }else{
            isDam = true;
        }
    }

    private void FixedUpdate()
    {
        if (areaDetection.detecObjs.Count > 0)
        {
            anim.SetInteger("Move", 2);

            direction = (areaDetection.detecObjs[0].transform.position - transform.position).normalized;
            if(direction.x > 0)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0) 
            {
                sprite.flipX = true;
            }

            if(!isDam)
                rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);
            else{

                // Knockback
                Vector2 knockbackDir = -(areaDetection.detecObjs[0].transform.position - transform.position).normalized; 
                rigidbody.velocity = knockbackDir * 2.0f; // Ajuste a força conforme necessário
                StartCoroutine(ResetKnockback()); // Espera um tempo antes de voltar ao normal

            }
        }
        else
        {
            anim.SetInteger("Move", 1);
        }
    }

    // Aguarda um tempo para restaurar o movimento normal
    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.3f); // Ajuste conforme necessário
        isDam = false;
        rigidbody.velocity = Vector2.zero; // Para evitar que o inimigo continue se movendo indefinidamente
    }

}



//void Update()
//{
//direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
//}