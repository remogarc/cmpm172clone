using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    
    public float turn_smooth_time = 0.1f;
    float turn_smooth_velocity;
    public Animator hooman;
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

        if(direction.magnitude >= 0.1f){
            hooman.SetTrigger("walking");
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turn_smooth_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, target_angle, 0f);

            Vector3 move_dir = Quaternion.Euler(0f, target_angle, 0.1f) * Vector3.forward;
            controller.Move(move_dir.normalized * speed * Time.deltaTime);
        }
        else{
            hooman.ResetTrigger("walking");
        }
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
            else if (controllerName.Contains("DualShock") || controllerName.Contains("PlayStation"))
            {
                Debug.Log("PlayStation Controller Connected");
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
