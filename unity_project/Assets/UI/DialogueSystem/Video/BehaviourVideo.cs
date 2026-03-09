using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BehaviourVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    private string currentVideoURL = "-1";  //-1
    public GameObject video;  

    public void Start() { videoPlayer.source = VideoSource.Url; }

    public void switchVideo(string url){ 
        if(url == null || url == "") return;
        if (currentVideoURL != url)
        { 
            // videoPlayer.url = Application.absoluteURL.Replace("index.html", "") + "Videos/meu_video.mp4";
            // Debug.Log(Application.absoluteURL.Replace("index.html", "") + "Videos/meu_video.mp4");

            // videoPlayer.url = Application.absoluteURL.Replace("index.html", "") + "Videos/" + url +".mp4";
            // Debug.Log(Application.absoluteURL.Replace("index.html", "") + "Videos/" + url +".mp4");
            // videoPlayer.Play();
            int lvl = InventoryManager.invent.currentLevel + 1;

            videoPlayer.url = Application.absoluteURL.Replace("index.html", "") + "Videos/lvl"+ lvl + "/" + url +".mp4";
            Debug.Log(videoPlayer.url);
            videoPlayer.Play();
        }
    }
    
    public void active(bool a){
        if(a)
            video.SetActive(a);
        else
            video.GetComponent<FadeVideo>().DisableWithFade();

        if(videoPlayer == null) return;
    }


}





            // currentVideoURL = url;
            // videoPlayer.url = url;           // Define o link do vídeo    videoPlayer.clip = v; 
            // videoPlayer.url = Application.absoluteURL + "Videos/meu_video.mp4"; 
            // videoPlayer.Play();
 
            // string parentURL = Application.absoluteURL.Substring(0, Application.absoluteURL.LastIndexOf('/'));
            // videoPlayer.source = VideoSource.Url;
            // videoPlayer.url = parentURL + "/Videos/meu_video.mp4";