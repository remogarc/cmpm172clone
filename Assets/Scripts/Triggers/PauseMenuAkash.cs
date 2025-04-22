using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;

public class PauseMenuAkash : MonoBehaviour
{

    // public GameObject pause;
    public GameObject options;
    public GameObject camera;
    public PlayerMovement pm; 
    public GameObject player; 
    public Vector3 pos;
    public CinemachineBrain cb;    // public Slider mouseSens;
    public GameObject menu_cam;
    public GameObject main_cam;
    public bool escape = false;
    public float speed = 20f;
    public GameObject pause;
    public GameObject grace_ui;
    public GameObject option_back;
    public GameObject option_button;
    public GameObject camera_back;
    public GameObject camera_button;
    public DetectionManager dm;

    bool triangle;
    PlayerControls pc;
    public Vector3 offset = new Vector3(0f, 10f, -20f); // how far up and back to move
    private Vector3 targetPosition;
    public Camera mc;    

    // public float mouseValue;
    // public int test;=
    void Awake(){
        pc = new PlayerControls();
        pc.Gameplay.EscapePS5.performed += ctx => triangle = true;
        pc.Gameplay.EscapePS5.canceled += ctx => triangle = false;
    }
    void OnEnable() {
        pc.Gameplay.Enable();
    }
    void OnDisable(){
        pc.Gameplay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        // pause.SetActive(false);
        // mouseValue = mouseSens.value;
        targetPosition = transform.position + offset;
    }
    // Update is called once per frame
    void Update()
    {
        if(escape){
            if(Vector3.Distance(transform.position, targetPosition) < 0.01f){
                escape = false;
                mc.fieldOfView = 137.0f;
            }

            main_cam.transform.position = Vector3.MoveTowards(main_cam.transform.position, menu_cam.transform.position, speed * Time.deltaTime);
        }
        pos = player.transform.position;
        if(Input.GetKeyDown(KeyCode.Escape) || triangle)
        {
            // pause.SetActive(true);
            // options.SetActive(false);

            // mouseValue = mouseSens.value;
            if(dm.grace || dm.prompt_check){
                return;
            }
            cb.enabled = false;
            escape = true; 
            main_cam.transform.rotation = menu_cam.transform.rotation;
            // main_cam.transform.position = menu_cam.transform.position;
            pause.SetActive(true);
            pm.enabled = false;
            EventSystem.current.SetSelectedGameObject(null); // Reset selection
            EventSystem.current.firstSelectedGameObject = option_button;
            EventSystem.current.SetSelectedGameObject(option_button); // Apply selection
            // Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            // Time.timeScale = 0f;

        }    
    }

    public void BackButtonPause(){
        pm.enabled =true;
        cb.enabled = true;
        escape = false;
        pause.SetActive(false);
        mc.fieldOfView = 55.2f;
        grace_ui.SetActive(false);
        dm.grace = false;
    }
    public void BackButtonGrace(){
        pm.enabled =true;
        cb.enabled = true;
        dm.grace = false;
        escape = false;
        pause.SetActive(false);
        mc.fieldOfView = 55.2f;
        grace_ui.SetActive(false);
    }
    public void BackButtonOptions(){
        options.SetActive(false);
        pause.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = option_button;
        EventSystem.current.SetSelectedGameObject(option_button); // Apply selection
    }
    public void BackButtonCamera()
    {
        camera.SetActive(false);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = camera_button;
        EventSystem.current.SetSelectedGameObject(camera_button); // Apply selection
    }

    public void ExitButtonPause(){
        Application.Quit();
    }

    public void OptionsButtonPause(){
        pause.SetActive(false);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = option_back;
        EventSystem.current.SetSelectedGameObject(option_back); // Apply selection
    }
    public void CameraButtonPause()
    {
        options.SetActive(false);
        camera.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = camera_back;
        EventSystem.current.SetSelectedGameObject(camera_back); // Apply selection
    }

    public void SaveGame(){
        SaveSystem.SavePlayer(player.transform.position);
        Debug.Log("saved.");
    }
    public void LoadGame(){
        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.position[0]);
        Debug.Log(data.position[1]);
        Debug.Log(data.position[2]);
        pm.enabled =true;
        cb.enabled = true;
        escape = false;
        pause.SetActive(false);
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Debug.Log(player.transform.position); 
    }
}
