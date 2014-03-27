using UnityEngine;
using System.Collections;


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
    private int gemstones;

    private LevelState levelState = LevelState.running;

    GameObject player;

    public float Distance { get { return distanceTravelled; } }
    public long Score { get { return score; } }
    public int Level { get { return level; } }
    public int XP { get { return xp; } }
    public int Gemstones { get { return gemstones; } }

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        player = GameObject.FindGameObjectWithTag("Player");
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


}
