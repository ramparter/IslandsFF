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

public class PilotStatsGUI : MonoBehaviour {
    public GUISkin GuiSkin;
    public bool SmallFormat = false;
    public bool ShowPilotLevel = true;

    void OnGUI() {
        GUI.skin = GuiSkin;
        DrawPilotInfo();
    }

    public void DrawPilotInfo() {
        Gu.SetColor(Color.black);
        float fontSize = SmallFormat ? GameConsts.Menu.PilotInfoFontSizeSmall :
            GameConsts.Menu.PilotInfoFontSize;
        float y = SmallFormat ? GameConsts.Menu.PilotInfoYSmallFont :
            GameConsts.Menu.PilotInfoY;

        // show summary info
        string summary;

            summary = string.Format(Strings.ExpString,
                0, "stats.Title",
                0, 100);

        if (ShowPilotLevel) {
            Gu.Label(Gu.Left(GameConsts.Menu.PilotInfoX),
                Gu.Top(y), Gu.Dim(fontSize),
                summary, false);
        }

        if (!SmallFormat) {
            long total = 0;
            int stars = 0;
            Gu.Label(Gu.Right(GameConsts.Menu.TotalScoreLabelX),
                Gu.Top(GameConsts.Menu.TotalScoreLabelY),
                Gu.Dim(GameConsts.Menu.TotalScoreLabelFontSize),
                Strings.TotalScore);
            Gu.Label(Gu.Right(GameConsts.Menu.TotalScoreX),
                Gu.Top(GameConsts.Menu.TotalScoreY),
                Gu.Dim(GameConsts.Menu.TotalScoreFontSize),
                total.ToString("D7"));
            Gu.Label(Gu.Right(GameConsts.Menu.StarsX),
                Gu.Top(GameConsts.Menu.StarsY),
                Gu.Dim(GameConsts.Menu.StarsFontSize),
                string.Format(Strings.TotalStarsFmt, stars));
        }
    }
}
