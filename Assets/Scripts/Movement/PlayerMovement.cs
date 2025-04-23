using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 20f;
    public float sprint_speed = 25f;
    public float normal_speed = 15f;

    [SerializeField] float jumpHeight = 20f;

    public bool is_player_grounded = true;
    public bool can_jump_again = true;
    
    public float turn_smooth_time = 0.1f;
    float turn_smooth_velocity;
    public Animator hooman;

    public static bool ps5;
    public Vector3 velocity;
    public float gravity = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        DetectController();

        // Listen for device connection changes
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Change move speed to sprint speed if LSHIFT is held
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprint_speed;
        }

        else
        {
            speed = normal_speed;
        }

        if(direction.magnitude >= 0.01f)
        {
            hooman.SetTrigger("walking");
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turn_smooth_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, target_angle, 0f);

            Vector3 move_dir = Quaternion.Euler(0f, target_angle, 0.1f) * Vector3.forward;
            controller.Move(move_dir.normalized * speed * Time.deltaTime);
        }

        else
        {
            hooman.ResetTrigger("walking");
        }
        velocity.y +=gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Let the player jump if they're grounded
        if (Input.GetKeyDown(KeyCode.Space) && is_player_grounded == true)
        {
            is_player_grounded = false;
            can_jump_again = false;
            StartCoroutine(PlayerJump());
        }

    }

    // Detect if player is grounded
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Terrain") && can_jump_again == true)
        {
            is_player_grounded = true;
        }
    }

    //Player jumps + 1 second jump cooldown
    IEnumerator PlayerJump()
    {
        Debug.Log("start jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        yield return new WaitForSeconds(1f);
        can_jump_again = true;

    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Reconnected)
        {
            DetectController();
        }
        else if (change == InputDeviceChange.Removed)
        {
            Debug.Log("A controller was disconnected.");
        }
    }

    void DetectController()
    {
        if (Gamepad.current != null)
        {
            string controllerName = Gamepad.current.displayName;
            
            if (controllerName.Contains("Xbox"))
            {
                Debug.Log("Xbox Controller Connected");
            }
            else if (controllerName.Contains("Wireless Controller"))
            {
                Debug.Log("PlayStation Controller Connected");
                ps5 = true;
            }
            else
            {
                Debug.Log("Unknown Controller Connected: " + controllerName);
            }
        }
        else
        {
            Debug.Log("No gamepad detected.");
        }
    }
}
