using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target; 
    public float force = 15f;
    public float curveForce = 5f; // Força para puxar a flecha para um lado

    private Rigidbody2D rb;

    public int damage = 2;

    public void Run(Transform tg)
    {
        rb = GetComponent<Rigidbody2D>(); 
        target = tg;
        // Calcula a direção inicial para o player
        Vector2 direction = (target.position - transform.position).normalized; 

        // Aplica força para frente e uma curvatura extra
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        // rb.AddForce(Vector2.right * curveForce, ForceMode2D.Impulse); // Curva para o lado direito
        Destroy(gameObject, 5f); // Destroi a flecha após 5 segundos
    } 
    public void Run(Vector2 tg)
    {
        rb = GetComponent<Rigidbody2D>();  
        // Aplica força para frente e uma curvatura extra
        rb.AddForce(tg * force, ForceMode2D.Impulse);
        // rb.AddForce(Vector2.right * curveForce, ForceMode2D.Impulse); // Curva para o lado direito
        Destroy(gameObject, 5f); // Destroi a flecha após 5 segundos
    } 

    void Update()
    {
        // Ajusta a rotação da flecha para apontar na direção do movimento
        if (rb.velocity.sqrMagnitude > 0.1f) // Evita rotação brusca quando parar
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    } 
     
    private void OnTriggerEnter2D(Collider2D other)
    {      
        if(this.transform.parent == other.transform) return; 

        ILife life = other.gameObject.GetComponent<ILife>();
        if(life != null) {
            life.isDamage(damage, this.transform); 
            this.transform.SetParent(other.transform); 
            this.GetComponent<Rigidbody2D>().simulated = false;
            // this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(this, 6);
            this.enabled = false;
        }

    }
 
}
