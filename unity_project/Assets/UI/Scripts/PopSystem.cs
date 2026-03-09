using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PopSystem : MonoBehaviour
{
    public RectTransform popupTransform;
    public float moveSpeed = 500f;
    public float displayTime = 2f;
    public float moveDistance = 300f; // Distância fixa do movimento
    private Vector2 startPos;
    private Vector2 endPos;

    void Start()
    {
        popupTransform = GetComponent<RectTransform>();
        startPos = popupTransform.anchoredPosition;
        endPos = startPos + new Vector2(-moveDistance, 0);
        popupTransform.anchoredPosition = startPos;
    }

    public void PopUpNotification(string text)
    {
        popupTransform.GetComponentInChildren<Text>().text = text;
        StartCoroutine(ShowPopup(text));
    }

    IEnumerator ShowPopup(string text)
    {
        float elapsedTime = 0;
        float duration = moveDistance / moveSpeed;

        while (elapsedTime < duration)
        {
            // popupTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);

            float t = Mathf.SmoothStep(0, 1, elapsedTime / duration);
            popupTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        popupTransform.anchoredPosition = endPos;

        yield return new WaitForSeconds(displayTime);

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            // popupTransform.anchoredPosition = Vector2.Lerp(endPos, startPos, elapsedTime / duration);
 
            float t = Mathf.SmoothStep(0, 1, elapsedTime / duration);
            popupTransform.anchoredPosition = Vector2.Lerp(endPos, startPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        popupTransform.anchoredPosition = startPos;
    }
}