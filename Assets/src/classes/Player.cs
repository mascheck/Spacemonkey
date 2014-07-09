using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // ----- Variables -----
    // Complex 
    public GameSetup gameSetup;

    // Simple  
    private float health = 100;
  
    // ----- Methods -----
    void ApplyDamage(float damage)
    {
        this.health -= Mathf.Round(damage);
        if (this.health <= 0)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        // Save Highscore
        gameSetup.setHighscore();
        // Restart Level
        Application.LoadLevel(0);
    }

    // ------------------- Getter & Setter --------------------
    public float getHealth()
    {
        return this.health;
    }
}
