using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public int life;
    public int lifeMax;
    public bool dead = false;

    public Image[] health;
    public Sprite cheio;
    public Sprite vazio;

    void Update()
    {
        heathLog();
        isDead();
    }

    public void heathLog()
    {
        if(life > lifeMax)
        {
            life = lifeMax;
        }

        for(int i = 0; i < health.Length; i++)
        {
            if (i < life)
            {
                health[i].sprite = cheio;
            }
            else
            {
                health[i].sprite = vazio;
            }

            if (i < lifeMax)
            {
                health[i].enabled = true;
            }
            else
            {
                health[i].enabled = false;
            }
        }
    }

    public void plusLife(int t){
        life += t;
    }

    public void isDead()
    {
        if(life <= 0 && !dead)
        {
            dead = true;
            GetComponent<PlayerController>()._playerRigidbody.isKinematic = true;
            GetComponent<PlayerController>()._playerRigidbody.velocity = Vector2.zero;
            GetComponent<PlayerController>().ChangeAnimation("Death");
            GetComponent<PlayerController>().enabled = false;
            Destroy(this.gameObject, 2.0f);
            this.enabled = false; 
        }
    }

    public void Damage(){
        life--;
    }
}
