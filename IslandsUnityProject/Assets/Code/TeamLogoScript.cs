using UnityEngine;
using System.Collections;

public class TeamLogoScript : MonoBehaviour {

    public float floatFrequency;
    public float floatHeight;
    public float fadeTime;
    public float startTime;
    public float duration;

    private Color defaultColor;
    private Color color;
    private float aliveTime;
    private Vector2 pos;
    private float heightOffset;
    private float prevHeightOffset = 0;

	// Use this for initialization
	void Start () {
        defaultColor = GetComponent<SpriteRenderer>().color;
        //GetComponent<SpriteRenderer>().enabled = false;
        defaultColor.a = 0;
        pos = transform.position;
        Destroy(gameObject, startTime + duration + 1);
	}
	
	// Update is called once per frame
	void Update () {
        aliveTime += Time.deltaTime;
        if (aliveTime > startTime && aliveTime - startTime < fadeTime)
            defaultColor.a = (aliveTime - startTime) / fadeTime;
        else if (aliveTime >= startTime && aliveTime - startTime < duration)
            defaultColor.a = 1;
        else if (aliveTime - startTime >= duration && aliveTime - startTime - duration < fadeTime)
            defaultColor.a = 1 - (aliveTime - startTime - duration) / fadeTime;
        else
            defaultColor.a = 0;

        GetComponent<SpriteRenderer>().color = defaultColor;

        heightOffset = floatHeight * Mathf.Sin(Time.time * Mathf.PI * 2 * floatFrequency);
        pos = (Vector2)transform.position + Vector2.up * (heightOffset - prevHeightOffset);
        transform.position = pos;
	}
}
