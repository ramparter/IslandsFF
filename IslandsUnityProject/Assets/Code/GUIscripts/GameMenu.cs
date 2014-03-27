using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

    public GUISkin skin;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;
        if(GameObject.FindGameObjectWithTag("Player") == null)
            DrawGameMenu();
    }

    private static void DrawGameMenu()
    {
        const int buttonWidth = 150;
        const int buttonHeight = 50;

        bool retryButton = GUI.Button(
            new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retry!");
        bool toMainMenuButton = GUI.Button(
            new Rect(Screen.width / 2 - (buttonWidth / 2), (1.5f * Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight), "Exit to Menu");

        if (retryButton)
        {
            // Reload the level
            Application.LoadLevel(Application.loadedLevelName);
        }

        if (toMainMenuButton)
        {
            // Reload the level
            Application.LoadLevel("MainMenu");
        }
    }
}
