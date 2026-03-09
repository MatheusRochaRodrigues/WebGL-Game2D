using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour {

    public Image background;
    //TextMeshProUGUI nameText;
    TextMeshProUGUI talkText;

    public float speed = 10f;
    bool open = false;

    public Image backgroundData;

    void Awake() {
        //background = transform.GetChild(0).GetComponent<Image>();
        //nameText   = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        talkText   = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Start() {

    }

    void Update() {
        //if(open) {
        //    background.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);
        //} else {
        //    background.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
        //}

        if (open)
        {
            float newValue = Mathf.Lerp(background.color.a, 1, speed * Time.deltaTime);
            background.color = new Color(background.color.r, background.color.g, background.color.b, newValue);

            backgroundData.color = new Color(backgroundData.color.r, backgroundData.color.g, backgroundData.color.b, newValue);

        }
        else
        {
            float newValue = Mathf.Lerp(background.color.a, 0, speed * Time.deltaTime); 
            background.color = new Color(background.color.r, background.color.g, background.color.b, newValue);

            
            backgroundData.color = new Color(backgroundData.color.r, backgroundData.color.g, backgroundData.color.b, newValue);
        }
    }

    //public void SetName(string name) {
    //    nameText.text = name;
    //}

    public void Enable() {
        //background.fillAmount = 0;
        open = true;
    }

    public void Disable() {
        open = false;
        //nameText.text = "";
        talkText.text = "";
    }

}












































































































































































































































