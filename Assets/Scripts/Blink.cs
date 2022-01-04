using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Color defaultColor;

    private float time = 1f;

    private void Awake()
    {
        defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        if(time <= 0)
        {
            StartCoroutine(BlinkCoroutine());
            time = 1f;
        }

        time -= Time.deltaTime;
    }

    private IEnumerator BlinkCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = defaultColor;
        yield return new WaitForSeconds(0.5f);
    }
}
