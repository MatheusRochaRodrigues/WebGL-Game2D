using System;
using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
using System.Collections;

public enum STATE {
    DISABLED,
    WAITING,
    TYPING
}

[RequireComponent(typeof(AudioSource))]
public class DialogueSystem : MonoBehaviour {

    public DialogueData dialogueData; 
    public GameObject List;

    int currentText = 0;
    public bool finished = false;

    TypeTextAnimation typeText;
    DialogueUI dialogueUI;

    [HideInInspector]
    public STATE state;
 
    public BehaviourVideo bv;

    void Awake() {

        typeText = FindObjectOfType<TypeTextAnimation>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        bv = FindObjectOfType<BehaviourVideo>();

        typeText.TypeFinished = OnTypeFinishe;

    }

    void Start() {
        state = STATE.DISABLED;

        GetComponent<AudioSource>().volume = 0.6f; // Toca o som uma vez

    }

    public void ResetDD(DialogueData dialogueData){ 
        this.dialogueData = dialogueData;
        currentText = 0;
    }

    void Update() {

        if(state == STATE.DISABLED) return; 

        switch(state) {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
        }

    }

    public void Next() {

        if(currentText == 0) {
            dialogueUI.Enable();
        }

        //dialogueUI.SetName(dialogueData.talkScript[currentText].name);
        // Debug.Log("d + " + currentText);
        // Debug.Log("s " + dialogueData.talkScript.Count);

        typeText.fullText = dialogueData.talkScript[currentText++].text;

        if(currentText == dialogueData.talkScript.Count) finished = true;
        else    //video setup
        {  
            bv.active(true); 
            bv.switchVideo(dialogueData.talkScript[currentText-1].videoURL);
        }

        typeText.StartTyping();
        state = STATE.TYPING;

    }

    void OnTypeFinishe() {
        state = STATE.WAITING;
    }
    public AudioClip guideSong; // Arraste o som aqui pelo Inspector

    void Waiting() {

        if(Input.GetKeyDown(KeyCode.Space)) {  
            if(!finished) {
                GetComponent<AudioSource>().PlayOneShot(guideSong); // Toca o som uma vez
                Next();
            } else {
                dialogueUI.Disable();
                // state = STATE.DISABLED;
                currentText = 0;
                // finished = false;
                List.SetActive(true);
                bv.active(false);

                StartCoroutine(finish());

                IEnumerator finish(){ 
                    yield return new WaitForSeconds(1.0f);  
                    state = STATE.DISABLED;
                    finished = false;
                }
            } 
        }

    }

    void Typing() {

        if(Input.GetKeyDown(KeyCode.Space)) {
            typeText.Skip();
            state = STATE.WAITING;
        }

    }

}






















































































































































































































































































































































































