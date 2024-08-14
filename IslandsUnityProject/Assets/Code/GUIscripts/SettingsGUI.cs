using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SettingsLayoutProperties
{
    public Color textColor = new Color(1, 0.75f, 0);
    public float WindowY = 200;
    public float WindowWidth = 1000;
    public float WindowHeight = 600;

    public float SettingsTitleX = 0;
    public float SettingsTitleY = 200;
    public float SettingsTitleFontSize = 60;

    public float MedButtonFontSize = 36;
    public float MedButtonWidth = 240;
    public float MedButtonHeight = 80;

    public float SmButtonFontSize = 36;
    public float SmButtonWidth = 240;
    public float SmButtonHeight = 80;

    public float settingsY = 100;
    public float settingsRowHeight = 10;
    public float settingsFontSize = 36;

    public float slidersX = 240;
    public float slidersWidth = 36;


}

public class SettingsGUI : MonoBehaviour {

    public GUISkin GuiSkin;
    public Texture2D windowTex;
    public Countdown mTransition = new Countdown(false, GameConsts.Menu.TransitionDuration);
    public AudioClip UiBeepFx;
    public SettingsLayoutProperties settingsLayoutProperties;
    public SettingsLayoutProperties slp { get { return settingsLayoutProperties; } }

    float axisSensX = 5, axisSensY = 5, deadzone = 0.2f;
    Transform axisTester;

    void OnEnable()
    {
        mTransition.Start();
    }

    void Beep()
    {
        //AudioSource.PlayClipAtPoint(UiBeepFx, Vector3.zero);
    }

    void Start()
    {
        axisTester = GameObject.Find("axisTester").transform;
        ResetSettings();
        AccelReader.SetDefault();
    }

    void OnGUI()
    {

        GUI.skin = GuiSkin;

        DrawWindow();
        DrawSliders();

        DrawSettingsTitle();


        if (DrawSaveButton())
        {
            Beep();
            SaveSettings();
            CloseSettings();
        }
        else if (DrawBackButton())
        {
            CloseSettings();
            Beep();
        }
        else if (DrawSetDefaultButton())
        {
            Beep();
        }


    }

    void CloseSettings()
    {
        gameObject.GetComponent<MainMenuGUI>().enabled = true;

        this.enabled = false;
    }

    void ResetSettings()
    {
        axisSensX = PlayerPrefs.GetFloat(Strings.axisSensX, 8f);
        axisSensY = PlayerPrefs.GetFloat(Strings.axisSensY, 8f);
        deadzone = PlayerPrefs.GetFloat(Strings.axisDeadzone, 0.2f);
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat(Strings.axisSensX, axisSensX);
        PlayerPrefs.SetFloat(Strings.axisSensY, axisSensY);
        PlayerPrefs.SetFloat(Strings.axisDeadzone, deadzone);
        PlayerPrefs.Save();
    }


    void Update()
    {
        mTransition.Update(Time.deltaTime);
    }

    void DrawSettingsTitle()
    {
        Gu.SetColor(slp.textColor);

        Gu.Label(Gu.Center(0),
            (int)Util.Interpolate(0.0f, 0.0f, 1.0f, Gu.Top(GameConsts.Menu.TitleY),
                mTransition.NormalizedElapsed),
            Gu.Dim(GameConsts.Menu.TitleFontSize), "Settings");
    }

    bool DrawSaveButton()
    {
        float w = GameConsts.Menu.MedButtonWidth;
        float h = GameConsts.Menu.MedButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Left(600 - w/2),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f, Gu.Middle(200),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            "Save");
    }

    bool DrawBackButton()
    {
        float w = GameConsts.Menu.MedButtonWidth;
        float h = GameConsts.Menu.MedButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Left(200 - w/2),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f, Gu.Middle(200),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            "Back");
    }

    bool DrawSetDefaultButton()
    {
        float w = GameConsts.Menu.MedButtonWidth;
        float h = GameConsts.Menu.MedButtonHeight;
        Gu.SetColor(Color.white);
        return Gu.Button(
            Gu.Left(600 - w / 2),
            (int)Util.Interpolate(0.0f, Screen.height, 1.0f, Gu.Middle(0),
                mTransition.NormalizedElapsed),
            Gu.Dim(w), Gu.Dim(h),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            "Set Axis Center");
    }

    void DrawSliders()
    {
       
        // draw X
        Gu.SetColor(Color.white);
        Gu.Label((int)Util.Interpolate(0.0f, - Gu.Left(100), 1.0f, Gu.Left(40), mTransition.NormalizedElapsed),
            Gu.Middle(-200),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            "Axis X", false);

        Rect xRect = new Rect((int)Util.Interpolate(0.0f, - Gu.Left(100), 1.0f, Gu.Left(220), mTransition.NormalizedElapsed),
            Gu.Middle(-190), Gu.Dim(400), Gu.Dim(GameConsts.Menu.MedButtonFontSize));
        axisSensX = GUI.HorizontalSlider(xRect, axisSensX, 2.5f, 20.0f);

        Gu.SetColor(Color.white);
        Gu.Label((int)Util.Interpolate(0.0f, Gu.Left(-100), 1.0f, Gu.Left(660), mTransition.NormalizedElapsed),
            Gu.Middle(-200),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            axisSensX.ToString("0.00"), false);

        //
        Gu.Label((int)Util.Interpolate(0.0f, -Gu.Left(100), 1.0f, Gu.Left(40), mTransition.NormalizedElapsed),
            Gu.Middle(-150),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            "Axis Y", false);

        Rect yRect = new Rect((int)Util.Interpolate(0.0f, -Gu.Left(100), 1.0f, Gu.Left(220), mTransition.NormalizedElapsed),
            Gu.Middle(-140), Gu.Dim(400), Gu.Dim(GameConsts.Menu.MedButtonFontSize));
        axisSensY = GUI.HorizontalSlider(yRect, axisSensY, 2.5f, 20.0f);

        Gu.Label((int)Util.Interpolate(0.0f, -Gu.Left(100), 1.0f, Gu.Left(660), mTransition.NormalizedElapsed),
            Gu.Middle(-150),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            axisSensY.ToString("0.00"), false);

        //
        Gu.Label((int)Util.Interpolate(0.0f, -Gu.Left(100), 1.0f, Gu.Left(40), mTransition.NormalizedElapsed),
            Gu.Middle(-100),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            "Deadzone", false);

        Rect dRect = new Rect((int)Util.Interpolate(0.0f, -Gu.Left(100), 1.0f, Gu.Left(220), mTransition.NormalizedElapsed),
            Gu.Middle(-90), Gu.Dim(400), Gu.Dim(GameConsts.Menu.MedButtonFontSize));
        deadzone = GUI.HorizontalSlider(dRect, deadzone, 0.0f, 1f);

        Gu.Label((int)Util.Interpolate(0.0f, -Gu.Left(100), 1.0f, Gu.Left(660), mTransition.NormalizedElapsed),
            Gu.Middle(-100),
            Gu.Dim(GameConsts.Menu.MedButtonFontSize),
            deadzone.ToString("0.00"), false);
    }

    void DrawWindow()
    {

        Rect r = new Rect(Gu.Left(0), Gu.Top(slp.WindowY), 
            (int)Util.Interpolate(0.0f, 0.0f, 1.0f, Gu.Dim(slp.WindowWidth), mTransition.NormalizedElapsed), 
            Gu.Dim(slp.WindowHeight));
        GUI.DrawTexture(r, windowTex);
    }
}
