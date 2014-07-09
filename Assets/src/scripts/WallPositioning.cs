using UnityEngine;
using System.Collections;

public class WallPositioning : MonoBehaviour {

    // ----- Variables -----
    // Complex
    // Walls
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject bottomWall;
    public GameObject bottomLimitation;

    // Colliders
    private BoxCollider2D leftWallCollider;
    private BoxCollider2D rightWallCollider;
    private BoxCollider2D bottomWallCollider;
    private BoxCollider2D bottomLimitationCollider;

    // ----- Methods -----
	// Use this for initialization
	void Start () {
        // Initiate colliders
        leftWallCollider = (BoxCollider2D) leftWall.collider2D;
        rightWallCollider = (BoxCollider2D) rightWall.collider2D;
        bottomWallCollider = (BoxCollider2D) bottomWall.collider2D;
        bottomLimitationCollider = (BoxCollider2D) bottomLimitation.collider2D;

        // Set wallsize
        float wallHeight = CalculateWallHeight();
        float wallWidth = CalculateWallWidth();
        leftWallCollider.size = new Vector2(1f, wallHeight);
        rightWallCollider.size = new Vector2(1f, wallHeight);
        bottomWallCollider.size = new Vector2(wallWidth, 1f);
        bottomLimitationCollider.size = new Vector2(wallWidth, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        updateWallPosition();
	}

    private void updateWallPosition()
    {
        // Put walls in position
        leftWall.transform.position = CalculateLeftWallPosition();
        rightWall.transform.position = CalculateRightWallPosition();
        bottomWall.transform.position = CalculateBottomWallPosition();
        bottomLimitation.transform.position = CalculateBottomWallPosition();
    }

    private Vector2 CalculateLeftWallPosition() {
        Vector2 result = Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height / 2));
        result.x = result.x - leftWallCollider.size.x / 2;
        return result;
    }

    private Vector2 CalculateRightWallPosition() {
        Vector2 result = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2));
        result.x = result.x + rightWallCollider.size.x / 2;
        return result;
    }

    private Vector2 CalculateBottomWallPosition() {
        Vector2 result = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0f));
        result.y = result.y - bottomWallCollider.size.y / 2;
        return result;
    }

    private float CalculateWallHeight() {
        float yTop = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).y;
        float yBottom = Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height)).y;
        return Mathf.Abs(yTop - yBottom);
    }

    private float CalculateWallWidth() {
        float xLeft = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
        float xRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x;
        return xRight - xLeft;
    }
}
