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
    public PlayerMovement pm; 
    public GameObject player; 
    public Vector3 pos;
    public CinemachineBrain cb;    // public Slider mouseSens;
    public GameObject menu_cam;
    public GameObject main_cam;
    public bool escape = false;
    public float speed = 20f;
    public GameObject pause;
    public GameObject option_back;
    public GameObject option_button;
    bool triangle;
    PlayerControls pc;
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
    }
    // Update is called once per frame
    void Update()
    {
        if(escape){
            if(main_cam.transform.position.x <= -80f){
                escape = false;
            }
            main_cam.transform.position += menu_cam.transform.position.normalized * speed * Time.deltaTime;
        }
        pos = player.transform.position;
        if(Input.GetKeyDown(KeyCode.Escape) || triangle)
        {
            // pause.SetActive(true);
            // options.SetActive(false);

            // mouseValue = mouseSens.value;
            cb.enabled = false;
            escape = true; 
            main_cam.transform.rotation = menu_cam.transform.rotation;
            pause.SetActive(true);
            pm.enabled = false;
            // Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            // Time.timeScale = 0f;
        }    
    }

    public void BackButtonPause(){
        pm.enabled =true;
        cb.enabled = true;
        escape = false;
        ResetTriggers(pause);
    }
    public void BackButtonOptions(){
        ResetTriggers(options);
        pause.SetActive(true);
        EventSystem.current.firstSelectedGameObject = option_button;
        EventSystem.current.SetSelectedGameObject(option_button); // Apply selection
    }

    public void ExitButtonPause(){
        Application.Quit();
    }

    public void OptionsButtonPause(){
        ResetTriggers(pause);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = option_back;
        EventSystem.current.SetSelectedGameObject(option_back); // Apply selection
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
        ResetTriggers(pause);
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Debug.Log(player.transform.position); 
    }

    private void ResetTriggers(GameObject menu) {
        Animator[] animators = menu.GetComponentsInChildren<Animator>(true);

        foreach (Animator animator in animators)
        {
            animator.Play("Normal", 0, 0f);
        }

        StartCoroutine(DelayedDeactivate(menu));
    }
    IEnumerator DelayedDeactivate(GameObject menu) {
        yield return null; // Wait one frame
        menu.SetActive(false);
    }
}
