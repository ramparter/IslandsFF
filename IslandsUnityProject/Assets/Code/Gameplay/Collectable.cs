using UnityEngine;
using System.Collections;

public enum CollectableType
{
    steam = 0,
    gemstone,
    crystal,
    scrapMetal,
    gold,
    steel,
    stone,
    wood
}

public class Collectable : MonoBehaviour {


    public CollectableType collectableType = 0;
    public int amount = 5;
    public float shrinkScale = 1f;
    public float speedTowardsCollector = 20;
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
        levelController = FindAnyObjectByType<LevelController>().GetComponent<LevelController>();        
        color = GetComponent<SpriteRenderer>().color;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (isCollected)
        {
            Destroy(gameObject);
            return;
        }

        if (collectionTime >= collectionDuration)
            OnCollect();
    }

    void OnCollect()
    {
        isCollected = true;
        collector.GetComponent<Collector>().OnCollect(GetComponent<Collectable>());
        //if (score > 0) levelController.UpdateScore(score);

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

    private void FixedUpdate()
    {

        if (nearCollector)
        {
            collectorDirection = (collector.transform.position - transform.position).normalized;
            transform.position += (Vector3)collectorDirection * speedTowardsCollector * Time.fixedDeltaTime;
            collectionTime += Time.fixedDeltaTime;
            scale = Mathf.Lerp(1, shrinkScale, collectionTime / collectionDuration);

            // GetComponent<SpriteRenderer>().color = color * scale ;
        }
        else
        {
            scale = 0.95f + 0.05f * Mathf.Cos(Time.time * Mathf.PI * 1.5f);
        }


        transform.localScale = Vector2.one * scale;
    }
}
