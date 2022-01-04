using UnityEngine;

public class BonusBlink : MonoBehaviour
{
    public float timeToStartBlink;
    public float startBlinkSpeed;
    public float finalBlinkSpeed;
    public float timeToDestroy;


    private float _startTime;
    private Material material;

    private void Start()
    {
        _startTime = Time.time;
        material = new Material(GetComponent<Renderer>().sharedMaterial);
        GetComponent<Renderer>().sharedMaterial = material;
    }

    private void Update()
    {
        if (Time.time > _startTime + timeToStartBlink)
        {
            material.color = new Color(material.color.r,
                                        material.color.g,
                                        material.color.b,
                                        Mathf.Sin(Time.fixedTime * Mathf.PI *
                                        Mathf.Lerp(startBlinkSpeed, finalBlinkSpeed, (Time.time - (_startTime + timeToStartBlink)) / (timeToDestroy - timeToStartBlink))
                                        ) * 0.5f + 0.5f);
        }
        if (Time.time > _startTime + timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

}
