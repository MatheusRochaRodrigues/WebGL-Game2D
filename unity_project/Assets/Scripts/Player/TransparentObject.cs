using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _transparenceValue = 0.7f;
    [SerializeField] private float _transparenceFadeTime = .4f;

    private SpriteRenderer _spriteRenderer;

    int orderInitial;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        orderInitial = _spriteRenderer.sortingOrder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteRenderer.sortingOrder = 17;

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(FadeObject(_spriteRenderer, _transparenceFadeTime,_spriteRenderer.color.a, _transparenceValue));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.sortingOrder = orderInitial;

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(FadeObject(_spriteRenderer, _transparenceFadeTime, _spriteRenderer.color.a, 1f));
        }
    }

    private IEnumerator FadeObject(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetValueTransparency)
    {
        float timeElapsed = 0;
        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetValueTransparency, timeElapsed/fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);


            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
