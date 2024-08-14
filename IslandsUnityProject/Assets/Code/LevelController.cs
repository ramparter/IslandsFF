using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System;


public enum LevelState
{
    running = 0,
    paused,
    intro,
    lost,
    won,
    failed
}

public class LevelController : MonoBehaviour {

    private float distanceTravelled;
    private long score;
    private int level;
    private int xp;
    private Dictionary<CollectableType, int> resources;

    private LevelState levelState = LevelState.running;

    GameObject player;

    public float Distance { get { return distanceTravelled; } }
    public long Score { get { return score; } }
    public int Level { get { return level; } }
    public int XP { get { return xp; } }
    public Dictionary<CollectableType, int> Resources { get { return resources; } }

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        player = GameObject.FindGameObjectWithTag("Player");
        resources = new Dictionary<CollectableType, int>();
        foreach (CollectableType type in Enum.GetValues(typeof(CollectableType)))
        {
            resources[type] = 0;
        }
        Console.WriteLine(resources);
    }
	
	// Update is called once per frame
	void Update () {
	    if(!player && levelState == LevelState.running)
        {
            OnPlayerLost();
        }
        distanceTravelled = Camera.main.transform.position.x;
        level = (int) distanceTravelled / 1000;
	}

    void OnPlayerLost()
    {
        SaveScore();
        levelState = LevelState.lost;
    }

    public void UpdateScore(long value)
    {
        score += value;
    }

    private void SaveScore()
    {
        GameManager.Instance.SaveStats((long)distanceTravelled, level, score);
    }


    public void UpdateValuables(Collectable collectable)
    {
        resources[collectable.collectableType] += collectable.amount;
    }

}
