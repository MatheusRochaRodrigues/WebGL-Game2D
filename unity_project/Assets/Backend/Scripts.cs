using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    [SerializeField] private SpriteRenderer circle;
    public void Start()
    {
        //ReceiveMessage("colorBlue");
    }
    public void ReceiveMessage(string message)
    {
        Debug.Log("Mensagem recebida: " + message);
        if (message == "left")
        {
            this.transform.position += new Vector3(-1, 0, 0);
        }
        if (message == "right")
        {
            this.transform.position += new Vector3(1, 0, 0);
        }
        if (message == "up")
        {
            this.transform.position += new Vector3(0, 1, 0);
        }
        if (message == "dawn")
        {
            this.transform.position += new Vector3(0, -1, 0);
        }
        if (message == "colorBlue")
        {
            circle.color = Color.blue;
        }

    }
}
