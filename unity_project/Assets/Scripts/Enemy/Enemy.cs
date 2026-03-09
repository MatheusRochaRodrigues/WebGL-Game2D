using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Enemy : MonoBehaviour, ILife, IAnimationManager
{
    public float speed = 1.5f;
    private Vector2 direction;
    private Rigidbody2D rigidbody;
    public string currentAnimation = "";

    public AreaDetection areaDetectionWalk;
    public AreaDetection areaDetectionAttack;
    private Animator anim;
    public GameObject Arrow;

    public int life = 10;
    public bool isDam = false;

    [Header("pl")]
    public Transform ShooterLocation;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(onFire());
    }
    private bool once=true;
    private void FixedUpdate()
    {
        if(isDam && once){
            once = false;
            // Knockback
            Vector2 knockbackDir = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized; 
            // Vector2 knockbackDir = -(areaDetection.detecObjs[0].transform.position - transform.position).normalized; 
            // rigidbody.velocity = knockbackDir * 5.0f; // Ajuste a força conforme necessário
            rigidbody.AddForce(knockbackDir * 5.0f, ForceMode2D.Impulse);
            // rigidbody.velocity = knockbackDir * 5.0f; // Ajuste a força conforme necessário
            StartCoroutine(ResetKnockback()); // Espera um tempo antes de voltar ao normal
        }
        if(isDam) return;
        
        if (areaDetectionAttack.detecObjs.Count == 0 && areaDetectionWalk.detecObjs.Count > 0)
        {
            SetDirectionPlayer();
            ChangeAnimation("Run");
            rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);
        } 
        if(areaDetectionAttack.detecObjs.Count == 0 && areaDetectionWalk.detecObjs.Count == 0)
        {
            ChangeAnimation("");
        }
    }
    // Aguarda um tempo para restaurar o movimento normal
    private IEnumerator onFire()
    { 
        // InvokeRepeating("SpawnObject", 2f, 5f); // Começa em 2s e repete a cada 5s
        // Invoke("SpawnObject", 3f); // Chama SpawnObject function após 3 segundos
        while(true){
            if(areaDetectionAttack.detecObjs.Count > 0){
                SetDirectionPlayer();
                ChangeAnimation("Attack");
                // yield return new WaitForSeconds(0.5f); // Ajuste conforme necessário 
                GameObject obj = Instantiate(Arrow, ShooterLocation);
                obj.transform.SetParent(this.transform);
                obj.GetComponent<Arrow>().Run(GameObject.FindGameObjectWithTag("Player").transform);  
                
                ChangeAnimation("");
            }
            yield return new WaitForSeconds(1.5f); // Ajuste conforme necessário 
        }
    }

    // Aguarda um tempo para restaurar o movimento normal
    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.3f); // Ajuste conforme necessário
        isDam = false;
        once = true;
        rigidbody.velocity = Vector2.zero; // Para evitar que o inimigo continue se movendo indefinidamente
    }

    
    public void ChangeAnimation(string animation, float crossfade = 0.2f, float time = 0){
        if(time > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait(){
            yield return new WaitForSeconds(time - crossfade);
            Validate();
            
        }

        void Validate(){
            if(currentAnimation != animation){
                currentAnimation = animation;

                if(currentAnimation == "")
                    anim.CrossFade("Idle", crossfade); //CheckAnimation("");            //checkAnimation()
                else
                    anim.CrossFade(animation, crossfade);
            }
            
        }
    }


    private void SetDirectionPlayer(){
        direction = (areaDetectionWalk.detecObjs[0].transform.position - transform.position).normalized;
        direction.x = (direction.x > 0.20)? 1 : direction.x; 
        anim.SetFloat("AxisX", direction.x);
        anim.SetFloat("AxisY", direction.y); 
 
        // direction = (Player.transform.position - transform.position).normalized;
    }
    
    
    public void isDamage(int Damage, Transform colision,  Item item = null){
        life -= Damage;
        if(life <= 0){
            ChangeAnimation("SkeletonBowman_Death");
            Destroy(this.gameObject, 4.0f);
            rigidbody.isKinematic = true;
            this.enabled = false;
            GetComponent<Damage>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            StopAllCoroutines();
            this.enabled = true;
            this.tag = "Untagged";
               
            FindObjectOfType<CheckList>()?.updateCount();  
        }else{
            ChangeAnimation("Damage");
            isDam = true;
        }
    }

}



//void Update()
//{
//direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
//}