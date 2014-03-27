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
    private float xp;

    private LevelState levelState = LevelState.running;

    GameObject player;

    public float Distance { get { return distanceTravelled; } set { distanceTravelled = value; } }
    public long Score { get { return score; } set { score = value; } }
    public int Level { get { return level; } set { level = value; } }
    public float XP { get { return xp; } set { xp = value; } }

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
