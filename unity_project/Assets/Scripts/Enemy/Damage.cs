 using UnityEngine; 
 
public class Damage : MonoBehaviour
{  
    public int damage = 1; 
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        ILife life = collision.gameObject.GetComponent<ILife>();
        if(life != null) {
            life.isDamage(damage, this.transform);  
            // this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // Destroy(this, 6);
            // this.enabled = false;
        }

    }
}
