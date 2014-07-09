using UnityEngine;
using System.Collections;

public class CameraSetup : MonoBehaviour
{
    // ----- Variables -----
    // Complex
    public GameObject playerGameObject;
    public Player playerScript;
    public PlayerControl playerControlScript;
    public GameSetup gameSetup;

    // Simple
 

    // ----- Methods ------
    // Use this for initialization
    void Start()
    {
      
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 newCameraPosition = Camera.main.transform.position;

        if (playerControlScript.getCurrentControl() == 3)
        {
            newCameraPosition.y = playerGameObject.transform.position.y +3;
        } else if (playerGameObject.transform.position.y > Camera.main.transform.position.y)
        {
            newCameraPosition.y = playerGameObject.transform.position.y;
        }
        Camera.main.transform.position = newCameraPosition;
    }

    void OnGUI()
    {
        // Show Score
        GUI.Label(new Rect(Screen.width - 100, 0, 100, 20), "Highscore: " + gameSetup.getHighscore());
        GUI.Label(new Rect(Screen.width - 100, 20, 100, 20), "Score: " + gameSetup.getScore().ToString());
        // Show Health
        GUI.Label(new Rect(0, 0, 100, 20), playerScript.getHealth().ToString());
        // Restart Button
        if (gameSetup.getGameStatus() == GameSetup.GAME_STATUS_GAME)
        {
            if (GUI.Button(new Rect(0, Screen.height - 50, 50, 50), "Restart"))
            {
                Application.LoadLevel(0);
            }
        }
    }
}
