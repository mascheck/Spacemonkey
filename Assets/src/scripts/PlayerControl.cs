using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    // ----- Constants -----
    // Movement
    private const float ACCELERATION_MAX = 10f;
    private const float ACCELERATION_BACK_MAX = 3f;
    private const float VELOCITY_MAX = 5f;
    private const float POWER_MAX = 1f;
    private const float POWER_DISTANCE = 10f;
    private const float TURN_SPEED = 250f;
    private int currentControl;
    private const float TILT_MOVE_SCALE = 10f;
    private const float TILT_THRESHOLD = 0.1f;

    // ----- Variables -----
    // Speed
    public float maxspeed;
    public float currentSpeed;
    public GameSetup gameSetup;

    // ----- Methods -----
    // Use this for initialization
    void Start()
    {

        // Load control settings
        currentControl = PlayerPrefs.GetInt("selectedControl");
        if (currentControl == 0)
        {
            // At first start use control 1
            currentControl = 1;
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // When in game, use selected control
        if (gameSetup.getGameStatus() == GameSetup.GAME_STATUS_GAME)
        {
            switch (this.currentControl)
            {
                case 1:
                    // No input -> stop
                    if (!(MouseControl() || TouchControl1()))
                    {
                        stop();
                    }
                    break;
                case 2:
                    TouchControl2();
                    break;
                case 3:
                    TiltControl();
                    break;
            }
        }    
    }

    // ----- Controls -----
    bool MouseControl()
    {
        if (Input.GetMouseButton(0))
        {
            // Get Mouseposition in World
            Vector3 mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            
            // Calculate delta
            Vector2 delta = mousePosition - this.transform.position;
            float power = delta.magnitude / POWER_DISTANCE;
            
            // GO GO GO
            move(power);
            turn(mousePosition);
            return true;
        } else
        {
            return false;
        }
        
    }

    bool TouchControl1()
    {
        if (Input.touchCount > 0)
        {
            // Get positions
            Vector2 touchPosition;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 thisPosition = this.transform.position;
            
            // Calculate delta
            Vector2 delta = touchPosition - thisPosition;
            float power = delta.magnitude / POWER_DISTANCE;
            
            // GO GO GO
            move(power);
            turn(touchPosition);
            return true;
        } else
        {
            return false;
        }
    }

    private bool TouchControl2()
    {
        switch (Input.touchCount)
        {
            case 0:
                return false;
                
            case 1:
                if (Input.GetTouch(0).position.x < Screen.width / 2)
                {
                    turn(1);
                } else
                {
                    turn(-1);
                }
                return true;
                
            case 2:
                if (Input.GetTouch(0).position.y > Screen.height / 2 && Input.GetTouch(1).position.y > Screen.height / 2)
                {
                    move(POWER_MAX);
                } else
                {
                    stop();
                }
                return true;
                
            default:
                return false;
        }
    }
    
    private void TiltControl()
    {
        float tilt = Input.acceleration.x;
        if (Mathf.Abs(tilt) > TILT_THRESHOLD)
        {
            float movex = tilt * TILT_MOVE_SCALE * Time.deltaTime;
            transform.Translate(movex, 0, 0);
        }
        
        this.rigidbody2D.velocity = Vector2.up * VELOCITY_MAX / 2;
    }

    // ----- Movement -----
    void move(float power)
    {
        if (power > POWER_MAX)
        {
            power = POWER_MAX;
        }
        if (power < 0)
        {
            power = 0;
        }
        
        Vector2 dir = this.transform.TransformDirection(Vector2.up);

        // Set Velocity
        Vector2 velocityOld = this.rigidbody2D.velocity;
        float acceleration = power * ACCELERATION_MAX;
        Vector2 velocityNew = dir * acceleration * Time.deltaTime + velocityOld;
        if (velocityNew.magnitude >= VELOCITY_MAX)
        {
            velocityNew = velocityNew.normalized * VELOCITY_MAX;
        }
        this.rigidbody2D.velocity = velocityNew;
    }
    
    private void stop()
    {
        Vector2 velocityOld = this.rigidbody2D.velocity;
        Vector2 velocityNew;
        if ((-ACCELERATION_BACK_MAX * Time.deltaTime * velocityOld.normalized + velocityOld).magnitude <= 0)
        {
            velocityNew = velocityOld;
        } else
        {
            velocityNew = -ACCELERATION_BACK_MAX * Time.deltaTime * velocityOld.normalized + velocityOld;
        }
        
        this.rigidbody2D.velocity = velocityNew;
    }
    
    private int calculateTurnDirection(Vector3 targetPoint)
    {
        Vector3 diff = targetPoint - transform.position;
        diff.Normalize();
        
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(rot_z - 90, Vector3.forward);
        
        if (Mathf.Abs(targetRotation.eulerAngles.z - this.transform.rotation.eulerAngles.z) <= 2)
        {
            return 0;
        }
        
        if (Mathf.Abs(targetRotation.eulerAngles.z - this.transform.rotation.eulerAngles.z) < 180)
        {
            return (int)Mathf.Sign(targetRotation.eulerAngles.z - this.transform.rotation.eulerAngles.z);
        } else
        {
            return (int)-Mathf.Sign(targetRotation.eulerAngles.z - this.transform.rotation.eulerAngles.z);
        }
    }
    
    void turn(Vector3 point)
    {
        Vector3 targetRot = this.transform.eulerAngles;
        targetRot.z += calculateTurnDirection(point) * TURN_SPEED * Time.deltaTime;
        
        this.transform.eulerAngles = targetRot;
    }
    
    void turn(int direction)
    {
        Vector3 targetRot = this.transform.eulerAngles;
        targetRot.z += direction * TURN_SPEED * Time.deltaTime;
        
        this.transform.eulerAngles = targetRot;
    }

    // ----- Getter & Setter -----
    public void setCurrentControl(int control)
    {
        this.currentControl = control;
        PlayerPrefs.SetInt("selectedControl", control);
    }
    
    public int getCurrentControl() {
        return this.currentControl;
    }
}
