using UnityEngine;
using System.Collections;

public class EntitiesSpawner : MonoBehaviour {

    public float minCloudX = 5;
    public float maxCloudX = 10;
    public float minRockX = 3;
    public float maxRockX = 6;

    public Transform[] clouds;
    public Transform[] rocks;
    public Transform[] patterns;

    private float lastCollectX = 7;
    private float lastRockX = 7;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //SpawnPattern();
        SpawnRocks();
        SpawnCollectables();
	}

    void SpawnCollectables()
    {
        Vector2 cameraTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);
        //Debug.Log(cameraTopRight);
        if (cameraTopRight.x > lastCollectX)
        {
            //PowerType newPowerType = PowerType.steam;
           // if (Random.value * 100 <= 5) newPowerType = PowerType.powerAttack;
          //  else if (Random.value * 100 <= 5) newPowerType = PowerType.invincibility;

            float newCollectX = lastCollectX + minCloudX + Mathf.Pow(Random.value, 2) * (maxCloudX-minCloudX);

            float newCollectY = Random.Range(-cameraTopRight.y, cameraTopRight.y);
            int i = Random.Range(0, clouds.Length - 1);
            var cloudTransform = Instantiate(clouds[i]) as Transform;
            cloudTransform.position = new Vector2(newCollectX, newCollectY);
               // level.UpdateStatisticValue(GameStrings.StatCloudsSpawned + newCloudType, 1);
              //  level.UpdateStatisticValue(GameStrings.StatCloudsSpawned, 1);
                lastCollectX = newCollectX;
        
        }
    }

    void SpawnPattern()
    {
        Vector2 cameraTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);
        //Debug.Log(cameraTopRight);
        if (cameraTopRight.x > lastRockX)
        {
            float newRockX = lastRockX + minRockX + Mathf.Pow(Random.value, 1) * (maxRockX - minRockX);
            float newRockY = Random.Range(-cameraTopRight.y, cameraTopRight.y) * 0.1f;
            int i = Random.Range(0, patterns.Length - 1);
            var patternTransform = Instantiate(patterns[i]) as Transform;
            patternTransform.position = new Vector2(newRockX, newRockY);
            // level.UpdateStatisticValue(GameStrings.StatCloudsSpawned + newCloudType, 1);
            //  level.UpdateStatisticValue(GameStrings.StatCloudsSpawned, 1);
            lastRockX = newRockX;

        }
    }

    void SpawnRocks()
    {
        Vector2 cameraTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);
        //Debug.Log(cameraTopRight);
        if (cameraTopRight.x > lastRockX)
        {
            float newRockX = lastRockX + minRockX + Mathf.Pow(Random.value, 1) * (maxRockX - minRockX);
            float newRockY = Random.Range(-cameraTopRight.y, cameraTopRight.y) * 0.9f;
            int i = Random.Range(0, rocks.Length - 1);
            var rock = Instantiate(rocks[i]) as Transform;
            rock.position = new Vector2(newRockX, newRockY);
            // level.UpdateStatisticValue(GameStrings.StatCloudsSpawned + newCloudType, 1);
            //  level.UpdateStatisticValue(GameStrings.StatCloudsSpawned, 1);
            lastRockX = newRockX;

        }
    }
}
