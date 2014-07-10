using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour {

    // ----- Variables -----
    public GameObject asteroidPrefab1;
    public GameObject asteroidPrefab2;
    public GameObject asteroidPrefab3;
    public GameObject asteroidPrefab4;
    public GameSetup gameSetup;

    // Asteroids
    private int maxAsteroidsInARow;
    public float yOnLastGeneration = 0;

	private float lengthPerLevel = 20f;

    // ----- Methods -----
	// Use this for initialization
	void Start () {
        maxAsteroidsInARow = calculateMaxAsteroidsInARow(0);
	}
	
	// Update is called once per frame
	void Update () {
        maxAsteroidsInARow = calculateMaxAsteroidsInARow(0);
        generateAsteroids();
	}

    int calculateMaxAsteroidsInARow(int x)
    {

            float screenWidth = 2 * Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x;
            float asteroidWidth = asteroidPrefab2.renderer.bounds.max.x - asteroidPrefab2.renderer.bounds.min.x;
            int result = (int)((screenWidth / asteroidWidth)-asteroidWidth);

        return result;
    }

    private float levelMultiplier() {
        float score = gameSetup.getScore();

		float result = (int)(score / lengthPerLevel);
			if (!(result > 1)) {
			result = 2;
		}

		result = maxAsteroidsInARow / result;
		return result;
    }

    private GameObject ChoosePrefab() {
        int random = (int) (Random.value * 100 % 3);
        switch (random)
        {
            case 0:
                return asteroidPrefab1;
            case 1:
                return asteroidPrefab2;
            case 2:
                return asteroidPrefab3;
            case 3: 
                return asteroidPrefab4;
            default :
                goto case 1;
        }
    }

    void generateAsteroids()
    {
        float currentY = Camera.main.transform.position.y + Camera.main.orthographicSize + 3;
        if (currentY - yOnLastGeneration > 3)
        {
            float screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x; 
            for (int i = 0; i < maxAsteroidsInARow - levelMultiplier(); i++)
            {
           
                Instantiate(ChoosePrefab(), new Vector3((float)(screenWidth * 2f * (Random.value - 0.5)), (float)(currentY + 2 * (Random.value - 0.5)), 0), Quaternion.identity);

            }
            yOnLastGeneration = currentY;;
            
        }
    }
}
