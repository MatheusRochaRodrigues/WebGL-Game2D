using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Função chamada para mudar a cena
    public void MudarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
 
        #if UNITY_WEBGL && !UNITY_EDITOR
        // Application.ExternalCall("updateToolbox", nomeCena); // Envia para o JavaScript
        Application.ExternalEval("parent.postMessage({ type: 'updateToolbox', data: '"+nomeCena+"', id: '1' }, '*');");
        #endif
    } 
}
