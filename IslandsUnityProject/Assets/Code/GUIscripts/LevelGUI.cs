using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class LevelGUILayoutProperties
{
    public float distanceX = 200;
    public float distanceY = 40;
    public float distanceFontSize = 36;

    public float scoreX = 400;
    public float scoreY = 40;
    public float scoreFontSize = 36;

    public float levelX = 0;
    public float levelY = 40;
    public float levelFontSize = 36;

    public float xpX = 200;
    public float xpY = 40;
    public float xpFontSize = 18;

}

public class LevelGUI : MonoBehaviour {

    public LevelGUILayoutProperties layoutProperties;
    private LevelGUILayoutProperties lp { get { return layoutProperties; } }

    private LevelController levelController;

	// Use this for initialization
	void Start () {
        levelController = GetComponent<LevelController>();
        if (!levelController)
            this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        Gu.Label(Gu.Right(lp.distanceX), Gu.Bottom(lp.distanceY), Gu.Dim(lp.distanceFontSize), string.Format("{0}yd",levelController.Distance.ToString("0.0")));
        Gu.Label(Gu.Center(lp.scoreX), Gu.Top(lp.scoreY), Gu.Dim(lp.scoreFontSize), string.Format("Score: {0}", levelController.Score.ToString("00000")));
        Gu.Label(Gu.Center(lp.levelX), Gu.Top(lp.levelY), Gu.Dim(lp.levelFontSize), string.Format("Level {0}", levelController.Level.ToString()));
        //Gu.Label(Gu.Center(lp.xpX), Gu.Top(lp.xpY), Gu.Dim(lp.xpFontSize), string.Format("{0} xp", levelController.XP.ToString()));
        Gu.Label(Gu.Center(lp.xpX), Gu.Top(lp.xpY), Gu.Dim(lp.xpFontSize), string.Format("{0}", dictionaryToString(levelController.Resources)));
    }

    String dictionaryToString<TEnum>(Dictionary<TEnum, int> dict)
    {
        return string.Join("\n", dict.Select(kv => $"{kv.Key}: {kv.Value}"));

    }
}
