using UnityEngine;
using System.Collections;

public enum MenuState
{
    teamLogo = 0,
    intro,
    mainmenu,
    settings,
    credits,
    selectLevel
}

public class MainMenuController : MonoBehaviour {

    public GameObject teamLogo;
    public GameObject mainmenu;
    public GameObject credits;
    public MenuState menuState;

    public GUISkin skin;
    public float buttonsStart = 0.5f, buttonsOffset = 100, buttonWidth = 240, buttonHeight = 80;
    public Vector2 settingsWindowSize = new Vector2(600, 600);
    float axisSensX = 5, axisSensY = 5;
    string playerName = "Player";
    bool playButton, settingsButton, exitButton;

	// Use this for initialization
	void Start () {
        ResetSettings();	    
	}
	
	// Update is called once per frame
	void Update () {
        if (menuState == MenuState.teamLogo && GameObject.FindObjectOfType<TeamLogoScript>() == null)
        {
            Instantiate(mainmenu);
            menuState = MenuState.mainmenu;
        }
	}




    void OnGUI()
    {
        GUI.skin = skin;

//#if UNITY_ANDROID
//        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector2.one * 2);
//#endif

        if (menuState == MenuState.mainmenu) GUIMainMenu();
        else if (menuState == MenuState.settings) GUISettings();
    }

    private void GUIMainMenu()
    {
        Rect buttonRect = new Rect(Screen.width / 2 - (buttonWidth / 2), Screen.height * buttonsStart, buttonWidth, buttonHeight);

        playButton = GUI.Button(buttonRect, "Play");
        buttonRect.y += buttonsOffset;
        settingsButton = GUI.Button(buttonRect, "Settings");
        buttonRect.y += buttonsOffset;
        exitButton = GUI.Button(buttonRect, "Exit");

        // Draw a button to start the game
        if (playButton)
        {
            // On Click, load the first level.
            // "Stage1" is the name of the first scene we created.
            Application.LoadLevel("Level1");
        }
        if (settingsButton)
        {
            menuState = MenuState.settings;
        }
        if (exitButton)
        {
            Application.Quit();
        }
    }

    private void GUISettings()
    {
        Rect windowRect = new Rect(Screen.width / 2 - (settingsWindowSize.x / 2), Screen.height / 2 - settingsWindowSize.y / 2, settingsWindowSize.x, settingsWindowSize.y);
        windowRect = GUI.Window(0, windowRect, GUISettingsWindow, "Settings");

    }

    void GUISettingsWindow(int windowID)
    {
        playerName = GUI.TextField(new Rect(200, 30, 260, 30), playerName, 15);
        axisSensX = GUI.HorizontalSlider(new Rect(200, 80, 260, 30), axisSensX, 2.5f, 20.0f);
        axisSensY = GUI.HorizontalSlider(new Rect(200, 140, 260, 30), axisSensY, 2.5f, 20.0f);
        GUI.Label(new Rect(20, 30, 160, 40), "Player Name");
        GUI.Label(new Rect(20, 80, 160, 40), "Axis Sens X"); GUI.Label(new Rect(480, 80, 160, 40), axisSensX.ToString("0.00"));
        GUI.Label(new Rect(20, 140, 160, 40), "Axis Sens Y"); GUI.Label(new Rect(480, 140, 160, 40), axisSensY.ToString("0.00"));


        bool saveButton = GUI.Button(new Rect(settingsWindowSize.x - 140, settingsWindowSize.y - 60, 120, 40), "Save");
        bool cancelButton = GUI.Button(new Rect(20, settingsWindowSize.y - 60, 120, 40), "Back");
       // buttonRect.y += buttonsOffset;
       // buttonRect.y += buttonsOffset;
       // exitButton = GUI.Button(buttonRect, "Exit");

        // Draw a button to start the game
        if (saveButton)
        {
            SaveSettings();
            menuState = MenuState.mainmenu;
        }
        if (cancelButton)
        {
            menuState = MenuState.mainmenu;
            ResetSettings();
        }
    }

    void ResetSettings()
    {
        playerName = PlayerPrefs.GetString(Strings.playerName);
        axisSensX = PlayerPrefs.GetFloat(Strings.axisSensX);
        axisSensY = PlayerPrefs.GetFloat(Strings.axisSensY);
    }

    void SaveSettings()
    {
        PlayerPrefs.SetString(Strings.playerName, playerName);
        PlayerPrefs.SetFloat(Strings.axisSensX, axisSensX);
        PlayerPrefs.SetFloat(Strings.axisSensY, axisSensY);
        PlayerPrefs.Save();
    }

}
