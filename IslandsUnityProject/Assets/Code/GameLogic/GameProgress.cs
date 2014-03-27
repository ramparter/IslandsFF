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

using System;
using UnityEngine;

public class GameProgress {
    private const string PlayerPrefsKey = "islands-game-progress";

    private int mIslanderExp = 0; // islander experience points
    private int mLevelsCompleted = 0; // levels
    private long mHighestScore = 0; // 
    private long mLongestDistance = 0; // 
    private long mTotalScore = 0; // 
    private long mTotalGemstones = 0; // 
    private long mTotalDistance = 0; // 

    // do we have modifications to write to disk/cloud?
    private bool mDirty = false;

    public GameProgress () {

    }

    public long GetHighScore() {
        return mHighestScore;
    }

    public void SetScore(long distance, int level, long score) {
        if (mHighestScore < score) {
                mHighestScore= score;
                mDirty = true;
            }
        if (mLongestDistance < distance)
        {
            mLongestDistance = distance;
            mDirty = true;
        }
        if (mLevelsCompleted < level)
        {
            mLevelsCompleted = level;
            mDirty = true;
        }
        mTotalScore += score;
    }

    public static GameProgress LoadFromDisk() {
        string s = PlayerPrefs.GetString(PlayerPrefsKey, "");
        if (s == null || s.Trim().Length == 0) {
            return new GameProgress();
        }
        return GameProgress.FromString(s);
    }

    public static GameProgress FromBytes(byte[] b) {
        return GameProgress.FromString(System.Text.ASCIIEncoding.Default.GetString(b));
    }

    public void SaveToDisk() {
        PlayerPrefs.SetString(PlayerPrefsKey, ToString());
        mDirty = false;
    }

    public void MergeWith(GameProgress other) {
        if (other.mHighestScore > mHighestScore)
        {
            mHighestScore = other.mHighestScore;
            mDirty = true;
        }
        if (other.mLongestDistance > mLongestDistance)
        {
            mLongestDistance = other.mLongestDistance;
            mDirty = true;
        }
        if (other.mTotalScore > mTotalScore)
        {
            mTotalScore = other.mTotalScore;
            mDirty = true;
        }
        if (other.mIslanderExp > mIslanderExp) {
            mIslanderExp = other.mIslanderExp;
            mDirty = true;
        }
    }

    public override string ToString () {
        string s = "GPv2:" + mIslanderExp.ToString();
        s += ":" + mLongestDistance;
        s += ":" + mHighestScore;
        s += ":" + mTotalScore;
        return s;
    }

    public byte[] ToBytes() {
        return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
    }

    public static GameProgress FromString(string s) {
        GameProgress gp = new GameProgress();
        string[] p = s.Split(new char[] { ':' });
        if (!p[0].Equals("GPv2")) {
            Debug.LogError("Failed to parse game progress from: " + s);
            return gp;
        }
        gp.mIslanderExp = System.Convert.ToInt32(p[1]);
        gp.mLongestDistance = System.Convert.ToInt64(p[2]);
        gp.mHighestScore = System.Convert.ToInt64(p[3]);
        gp.mTotalScore = System.Convert.ToInt64(p[4]);
        return gp;
    }



    public bool AreAllLevelsCleared() {
        return mLevelsCompleted >= 10;
    }

    public int IslanderExperience {
        get {
            return mIslanderExp;
        }
    }

    public bool Dirty {
        get {
            return mDirty;
        }
        set {
            mDirty = value;
        }
    }

    public int IslanderLevel {
        get {
            return GetIslanderLevel(mIslanderExp);
        }
    }

    public bool AddIslanderExperience(int points) {
        if (points > 0) {
            int levelBefore = IslanderLevel;
            mIslanderExp += points;
            mDirty = true;
            return IslanderLevel > levelBefore;
        } else {
            return false;
        }
    }

    public static int GetIslanderLevel(int expPoints) {
        int i;
        for (i = GameConsts.Progression.ExpForLevel.Length - 1; i >= 0; --i) {
            if (GameConsts.Progression.ExpForLevel[i] <= expPoints) {
                break;
            }
        }
        return Util.Clamp(i, 1, GameConsts.Progression.MaxLevel);
    }

    public bool IsMaxLevel() {
        return IslanderLevel >= GameConsts.Progression.MaxLevel;
    }

    public int GetExpForNextLevel() {
        return IsMaxLevel() ? -1 : GameConsts.Progression.ExpForLevel[IslanderLevel + 1];
    }

    public PilotStats CurPilotStats {
        get {
            return new PilotStats(0);
        }
    }

    public long TotalScore {
        get { return mTotalScore; }
    }

    public int LevelsCompleted { get { return mLevelsCompleted; } }
    public long LongestDistance { get { return mLongestDistance; } }

    // Mostly for debug purposes
    public void ForceLevelUp() {
        mIslanderExp = GetExpForNextLevel();
    }

    // Mostly for debug purposes
    public void ForceLevelDown() {
        int level = IslanderLevel;
        if (level > 1) {
            mIslanderExp = GameConsts.Progression.ExpForLevel[level - 1];
        }
    }
}

