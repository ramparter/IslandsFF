using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public int score = 50;
    public float speedTowardsCollector = 2;
    public float collectionDuration = 2;

    private bool nearCollector;
    private bool isCollected;
    private float collectionTime = 0;
    private GameObject collector;
    private Vector2 collectorDirection;
    private Color color;
    private float scale;

    private LevelController levelController;


	// Use this for initialization
	void Start () {
        levelController = GameObject.FindObjectOfType<LevelController>().GetComponent<LevelController>();        
        color = GetComponent<SpriteRenderer>().color;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (isCollected)
        {
            Destroy(gameObject);
            return;
        }

        if (nearCollector)
        {
            collectorDirection = (collector.transform.position - transform.position).normalized;
            transform.position += (Vector3)collectorDirection * speedTowardsCollector * Time.deltaTime;
            collectionTime += Time.deltaTime;

            scale = Mathf.Max(0, 1 - collectionTime / collectionDuration);

           // GetComponent<SpriteRenderer>().color = color * scale ;
        }
        else
        {
            scale = 0.95f + 0.05f * Mathf.Cos(Time.time * Mathf.PI * 1.5f);
        }

        if (collectionTime >= collectionDuration)
            OnCollect();

        transform.localScale = Vector2.one * scale;
	}

    void OnCollect()
    {
        isCollected = true;
        collector.GetComponent<Collector>().OnCollect(GetComponent<Collectable>());
        if (score > 0) levelController.UpdateScore(score);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (nearCollector || collider.gameObject.tag != "Collector") return;
        collector = collider.gameObject;
        nearCollector = true;
        if (gameObject.GetComponent<Moving>())
            Destroy(gameObject.GetComponent<Moving>());
        //Debug.Log("Collected " + transform.position);
        Destroy(gameObject, collectionDuration * 2);
    }
}
