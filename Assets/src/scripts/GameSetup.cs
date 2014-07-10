using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour
{
    // ----- Constants -----
    public const int CURRENT_BUILD = 6;

    // GameStatus
    public const int GAME_STATUS_MENU = 0;
    public const int GAME_STATUS_GAME = 1;

    // ----- Variables -----
    // Simple
    private int gameStatus;
    private int score;
    private int highscore;

        
    // ----- Methods -----
    // Use this for initialization
    void Start()
    {
        gameStatus = 0;
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore");    
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    public int UpdateScore()
    {
        setHighscore();
        return this.score = (int)Camera.main.transform.position.y;
    }

    // ----- Getter & Setter -----
    public int getGameStatus() {
        return this.gameStatus;
    }

    public void setGameStatus(int status) {
        this.gameStatus = status;
    }

    public int getScore() {
        return this.score;
    }

    public void setScore(int score) {
        this.score = score;
    }

    public int getHighscore() {
        return this.highscore;
    }

    public void setHighscore() {
        if (this.score > this.highscore)
        {
            this.highscore = this.score;
            PlayerPrefs.SetInt("highscore", this.highscore);
        }
    }
}
