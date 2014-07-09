using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    // ----- Constants -----
    // Menu variables
    private const int MENU_STATUS_MAIN = 0;
    private const int MENU_STATUS_CONTROLS = 1;

    // ----- Variables ------
    // Complex 
    public GameSetup gameSetup;
    public PlayerControl playerControlScript;

    // Simple
    private int menuStatus = MENU_STATUS_MAIN;
    
    // Menusizes
    // Background box
    private float boxLeft;
    private float boxTop;
    private float boxWidth = 150f;
    private float boxHeight;
    // Button
    private float button1Width = 130f;
    private float button1Height = 50f;

    // ----- Methods -----
    // Use this for initialization
    void Start()
    {
        boxHeight = CalculateBackgroundBoxHeight(2);
        CalculateMenuPositions();
    }

    void OnGUI()
    {
        // When it is menu Time ....
        if (gameSetup.getGameStatus() == GameSetup.GAME_STATUS_MENU)
        {
            switch (menuStatus)
            {

                case MENU_STATUS_MAIN:
                    // Background box
                    GUI.Box(new Rect(boxLeft, boxTop, boxWidth, boxHeight), "Menu");
                    
                    // First button -> Start game
                    if (GUI.Button(new Rect(boxLeft + 10, boxTop + 30, button1Width, button1Height), "Start"))
                    {
                        gameSetup.setGameStatus(GameSetup.GAME_STATUS_GAME);
                    }
                    
                    // Second button -> Control menu
                    if (GUI.Button(new Rect(boxLeft + 10, boxTop + button1Height + 30, button1Width, button1Height), "Steuerung"))
                    {
                        setMenuStatus(MENU_STATUS_CONTROLS);
                    }
                    break;
                
                case MENU_STATUS_CONTROLS:
                    // Background box
                    GUI.Box(new Rect(boxLeft, boxTop, boxWidth, boxHeight), "Steuerung");
                    
                    // First button -> Control 1 + main menu
                    if (GUI.Button(new Rect(boxLeft + 10, boxTop + 30, button1Width, button1Height), "One-Touch"))
                    {
                        playerControlScript.setCurrentControl(1);
                        setMenuStatus(this.menuStatus = MENU_STATUS_MAIN);
                    }

                    // Second button -> Control 2 + main menu
                    if (GUI.Button(new Rect(boxLeft + 10, boxTop + button1Height + 30, button1Width, button1Height), "Multi-Touch"))
                    {
                        playerControlScript.setCurrentControl(2);
                        setMenuStatus(this.menuStatus = MENU_STATUS_MAIN);
                    }

                    // Third button -> Control 3 + main menu
                    if (GUI.Button(new Rect(boxLeft + 10, boxTop + 2 * button1Height + 30, button1Width, button1Height), "Tilt"))
                    {
                        playerControlScript.setCurrentControl(3);
                        setMenuStatus(this.menuStatus = MENU_STATUS_MAIN);
                    }

                    // Forth button -> back to main menu
                    if (GUI.Button(new Rect(boxLeft + 10, boxTop + 3 * button1Height + 30, button1Width, button1Height), "Zurück"))
                    {
                        setMenuStatus(this.menuStatus = MENU_STATUS_MAIN);
                    }
                    break;

                default:
                    goto case MENU_STATUS_MAIN;
            }
        }
    }

    private float CalculateBackgroundBoxHeight(int numberOfButtons) {
        return  40f + numberOfButtons * button1Height;
    }

    private void CalculateMenuPositions()
    {
        boxLeft = Screen.width / 2 - boxWidth / 2;
        boxTop = Screen.height / 2 - boxHeight / 2;
    }

    // ----- Getter & Setter -----
    public void setMenuStatus(int menuStatus) {
        this.menuStatus = menuStatus;
        switch (menuStatus)
        {
            case MENU_STATUS_MAIN:
                boxHeight = CalculateBackgroundBoxHeight(2);
                break;

            case MENU_STATUS_CONTROLS:
                boxHeight = CalculateBackgroundBoxHeight(4);
                break;
        }
        CalculateMenuPositions();
    }
}
