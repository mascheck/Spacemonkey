using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    // ----- Constants -----
    private const float SCALE_MAX = 10f;
    private const float SCALE_MIN = 3f;
    private const float SPEED_MAX = 0.1f;
    private const float DEFAULT_DAMAGE = 10f;

    // ----- Methods -----
    // Use this for initialization
    void Start()
    {
        //Scale();
        SetRandomDirection();
        SetMovement();
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            float damage = coll.relativeVelocity.magnitude * DEFAULT_DAMAGE;
            coll.gameObject.SendMessage("ApplyDamage", damage);
        }
    }

    void Scale()
    {
        float scale = Random.value * (SCALE_MAX - SCALE_MIN) + SCALE_MIN;
        this.transform.localScale = new Vector3(scale, scale, 0f);
    }

    void SetRandomDirection()
    {
        float degree = Random.value * 360f;
        this.transform.rotation = Quaternion.AngleAxis(degree, Vector3.back);
    }

    void SetMovement()
    {
        double speedX = 2 * (Random.value - 0.5) * SPEED_MAX;
        double speedY = 2 * (Random.value - 0.5) * SPEED_MAX;
        this.rigidbody2D.velocity = new Vector2((float)speedX, (float)speedY);
    }
}
