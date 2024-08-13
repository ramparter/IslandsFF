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
using System;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SceneManagement;

[Serializable]
public class LayoutProperties
{
    public float TitleY = 200;
    public float TitleFontSize = 60;

    // "Please wait" message settings:
    public float PleaseWaitFontSize = 36;

    // Play button
    public float MedButtonFontSize = 36;
    public float MedButtonWidth = 240;
    public float MedButtonHeight = 80;

    public  float SmButtonFontSize = 36;
    public  float SmButtonWidth = 240;
    public  float SmButtonHeight = 80;

    // Achievements and leaderboards buttons
    public float ButtonsRowHeigth = 10;
    public float ButtonsY = 10;
    public int PlayButtonOrder = 0;
    public float AchButtonY = 0;
    public float AchButtonX = 0;
    public int AchButtonOrder = 1;
    public float LbButtonX = 0;
    public float LbButtonY = 0;
    public int LbButtonOrder = 2;

    // Sign-in bar
    public float SignInBarY = 150;

    // Sign-in button
    public float SignInButtonX = -580;

    // Placement of Google+ logo
    public  float GooglePlusLogoX = - 10;
    public  float GooglePlusLogoY =  - 10;
    public  float GooglePlusLogoSize = 60;

    // Sign-in encouragement text
    public float SignInBlurbX = 0;
    public float SignInBlurbY = 10;
    public float SignInBlurbFontSize = 32;
    public Color SignInBlurbColor = new Color(1.0f, 0.25f, 0.0f, 1.0f);
    public Color SignedInBlurbColor = new Color(1.0f, 0.25f, 0.0f, 1.0f);

    // sign-out button
    public  float SignOutButtonX =  100;

    // Placement of the version string
    public  float BuildStringX = 20;
    public  float BuildStringY = 50;
    public  float BuildStringFontSize = 26;

    // pilot info
    public const float PilotInfoY = 30;
    public const float PilotInfoYSmallFont = 30;
    public const float PilotInfoX = 20;
    public const float PilotInfoFontSize = 35;
    public const float PilotInfoFontSizeSmall = 35;

    // total score and stars display
    public const float TotalScoreLabelX = 400;
    public const float TotalScoreLabelFontSize = 40;
    public const float TotalScoreLabelY = 50;
    public const float TotalScoreX = 150;
    public const float TotalScoreY = 50;
    public const float TotalScoreFontSize = 60;
    public const float StarsX = 90;
    public const float StarsY = 120;
    public const float StarsFontSize = 50;
}

public class MainMenuGUI : MonoBehaviour {
    public GUISkin GuiSkin;
    public GUISkin SignInButtonGuiSkin;
    public Countdown mTransition = new Countdown(false, GameConsts.Menu.TransitionDuration);
    public Texture2D GooglePlusTex;
    public Texture2D SignInBarTex;
    public AudioClip UiBeepFx;
    public LayoutProperties layoutProperties;
    private static bool sAutoAuthenticate = true;

    void OnEnable() {
        mTransition.Start();
    }

    void Beep() {
      //  AudioSource.PlayClipAtPoint(UiBeepFx, Vector3.zero);
    }

    void Start() {
        // if this is the first time we're running, bring up the sign in flow
        if (sAutoAuthenticate) {
            //GameManager.Instance.Authenticate();
            sAutoAuthenticate = false;
        }
    }

    void OnGUI() {
        bool standBy = false;
        bool authenticated = false;
        //authenticated = true;

        GUI.skin = GuiSkin;

        DrawTitle();

        DrawSignInBar();


        if (standBy) {
            DrawPleaseWait();
            return;
        }

        if (DrawPlayButton()) {
            Beep();
            this.enabled = false;
            SceneManager.LoadScene("Level1");
        }

        if (DrawSettingsButton())
        {
            Beep();
            gameObject.GetComponent<SettingsGUI>().enabled = true;
            this.enabled = false;
        }

        if (DrawCreditsButton())
        {
            Beep();
            gameObject.GetComponent<SettingsGUI>().enabled = true;
            this.enabled = false;
        }

        if (DrawExitButton())
        {
            Beep();
            Application.Quit();
        }

  

        if (authenticated)
        {
            DrawSignedInBlurb();
        }


        DrawBuildString();
    }

    void DrawPleaseWait() {
        Gu.SetColor(Color.black);
        Gu.Label(Gu.Center(0), Gu.Middle(0), Gu.Dim(layoutProperties.PleaseWaitFontSize),
            "100%");
    }

    void DrawBuildString() {
        Gu.SetColor(Color.black);
        Gu.Label(Gu.Left(layoutProperties.BuildStringX), Gu.Bottom(layoutProperties.BuildStringY),
            Gu.Dim(layoutProperties.BuildStringFontSize), Strings.BuildString, false);
    }

    void Update() {
        mTransition.Update(Time.deltaTime);
    }

    void DrawTitle() {
        Gu.SetColor(GameConsts.ThemeColor);
        Gu.Label(Gu.Center(0),
            (int) Util.Interpolate(0.0f, 0.0f, 1.0f, Gu.Top(layoutProperties.TitleY),
                mTransition.NormalizedElapsed),
            Gu.Dim(layoutProperties.TitleFontSize), Strings.GameTitle);
    }

    bool DrawPlayButton() {
        float w = layoutProperties.MedButtonWidth;
        float h = layoutProperties.MedButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Center(-w/2),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f,
            Gu.Middle(layoutProperties.ButtonsY + (h + layoutProperties.ButtonsRowHeigth) * layoutProperties.PlayButtonOrder),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(layoutProperties.MedButtonFontSize),
            Strings.Play);
    }

    bool DrawAchButton()
    {
        float w = layoutProperties.MedButtonWidth;
        float h = layoutProperties.MedButtonHeight;
        return Gu.Button(Gu.Center(-w/2),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f,
            Gu.Middle(layoutProperties.ButtonsY + (layoutProperties.ButtonsRowHeigth + h) * layoutProperties.AchButtonOrder),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(layoutProperties.MedButtonFontSize),
            Strings.Achievements);
    }

    bool DrawLbButton()
    {
        float w = layoutProperties.MedButtonWidth;
        float h = layoutProperties.MedButtonHeight;
        return Gu.Button(Gu.Center(-w/2),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f,
            Gu.Middle(layoutProperties.ButtonsY + (h + layoutProperties.ButtonsRowHeigth) * layoutProperties.LbButtonOrder),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(layoutProperties.MedButtonFontSize),
            Strings.Leaderboards);
    }

    bool DrawSettingsButton()
    {
        float w = layoutProperties.SmButtonWidth;
        float h = layoutProperties.SmButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Center(0),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f, Gu.Bottom(h + 10),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(layoutProperties.SmButtonFontSize),
            Strings.Settings);
    }

    bool DrawCreditsButton()
    {
        float w = layoutProperties.SmButtonWidth;
        float h = layoutProperties.SmButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Center(240),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f, Gu.Bottom(h + 10),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(layoutProperties.SmButtonFontSize),
            Strings.Credits);
    }

    bool DrawExitButton()
    {
        float w = layoutProperties.SmButtonWidth;
        float h = layoutProperties.SmButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Center(480),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f, Gu.Bottom(h + 10),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(layoutProperties.SmButtonFontSize),
            Strings.Exit);
    }



    void DrawSignInBar() {
        // draw the sign-in bar (the white bar behind the sign in button)
        Rect r = new Rect(Gu.Left(0), Gu.Bottom(layoutProperties.SignInBarY),
            (int)Util.Interpolate(0.0f, 0.0f, 1.0f, Screen.width,
                mTransition.NormalizedElapsed),
            Gu.Dim(layoutProperties.MedButtonHeight));
        GUI.DrawTexture(r, SignInBarTex);
    }

    void DrawSignInBlurb(string text) {
        bool authenticated = false;
        float x = authenticated ? 0.0f : layoutProperties.SignInBlurbX;

        // draw sign in explanation text
        Gu.SetColor(authenticated ? layoutProperties.SignedInBlurbColor :
            layoutProperties.SignInBlurbColor);
        Gu.Label(Gu.Center(x),
            Gu.Bottom(layoutProperties.SignInBarY + layoutProperties.SignInBlurbY),
            Gu.Dim(layoutProperties.SignInBlurbFontSize),
            text);
    }

    bool DrawSignInButton() {

        // draw the sign in button
        GUI.skin = SignInButtonGuiSkin;
        bool result = Gu.Button(Gu.Center(layoutProperties.SignInButtonX),
            Gu.Bottom(layoutProperties.SignInBarY),
            Gu.Dim(layoutProperties.MedButtonWidth),
            Gu.Dim(layoutProperties.MedButtonHeight),
            Gu.Dim(layoutProperties.MedButtonFontSize),
            "     " + Strings.SignIn);
        GUI.skin = GuiSkin;

        // draw the Google+ logo
        GUI.DrawTexture(new Rect(Gu.Center(layoutProperties.SignInButtonX + layoutProperties.GooglePlusLogoX),
            Gu.Bottom(layoutProperties.SignInBarY + layoutProperties.GooglePlusLogoY),
            Gu.Dim(layoutProperties.GooglePlusLogoSize),
            Gu.Dim(layoutProperties.GooglePlusLogoSize)), GooglePlusTex);

        // draw sign in encouragement text
        DrawSignInBlurb(Strings.SignInBlurb);

        return result;
    }

    void DrawSignedInBlurb() {

        DrawSignInBlurb(Strings.SignedInBlurb);
    }

    bool DrawSignOutButton() {
        return Gu.Button(Gu.Center(layoutProperties.SignOutButtonX),
            Gu.Bottom(layoutProperties.SignInBarY),
            Gu.Dim(layoutProperties.MedButtonWidth),
            Gu.Dim(layoutProperties.MedButtonHeight),
            Gu.Dim(layoutProperties.MedButtonFontSize),
            Strings.SignOut);
    }
}
