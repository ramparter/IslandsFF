/*
 * Copyright (C) 2013 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager {
    private static GameManager sInstance = new GameManager();
    private int mLevel = 0;

    private GameProgress mProgress;

    // what is the highest score we have posted to the leaderboard?
    private long mLongestPostedDistance = 0;


    public static GameManager Instance {
        get {
            return sInstance;
        }
    }

    public int Level {
        get {
            return mLevel;
        }
    }

    private GameManager() {

    }


    public void RestartLevel() {
        SaveProgress();
    }

    public void CompleteLevel(long score) {
       // mProgress.SetLevelProgress(mLevel, score, stars);
      //  SaveProgress();
      //  ReportAllProgress();
    }

    public void SaveStats(long distance, int level, long score)
    {
        SaveProgress();
    }

    public void QuitToMenu() {
        SaveProgress();
        SceneManager.LoadScene("MainMenu");
    }

    public bool HasNextLevel() {
        return mLevel < GameConsts.MaxLevel;
    }

    public GameProgress Progress {
        get {
            return mProgress;
        }
    }

    public void SaveProgress() {
    }


}
