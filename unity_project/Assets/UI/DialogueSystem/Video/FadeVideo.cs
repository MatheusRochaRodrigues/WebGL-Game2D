using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class FadeVideo : MonoBehaviour
{ 
    public VideoPlayer videoPlayer = null; 
    public float fadeDuration = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
        RestartVideo();
    } 
    
    private void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }

    private IEnumerator FadeIn()
    {
        RawImage v = GetComponent<RawImage>();
        Color color = v.color;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time / fadeDuration);
            v.color = color;
            yield return null;
        }

        color.a = 1;
        v.color = color; 
    }

    public void DisableWithFade()
    {
        StartCoroutine(Fade()); 
    } 

    private IEnumerator Fade()
    {
        RawImage v = GetComponent<RawImage>();
        Color color = v.color;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, time / fadeDuration);
            v.color = color;
            yield return null;
        }

        color.a = 1;
        v.color = color; 
    gameObject.SetActive(false); // Só desativa depois do fade
    }
}
