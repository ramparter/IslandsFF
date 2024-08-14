using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float duration = 1f;
    public float startingScale = 0.25f;
    public float endScale = 1.5f;
    public float startingAlpha = 1f;
    public float endAlpha = 0f;

    private float t;
    private Color color;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
        color = GetComponent<SpriteRenderer>().color; 
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        float alpha = Mathf.Lerp(startingAlpha, endAlpha, t / duration);
        float scale = Mathf.Lerp(startingScale, endScale, t / duration);
        color.a = alpha;
        GetComponent<SpriteRenderer>().color = color;
        transform.localScale = initialScale * scale;
    }
}
