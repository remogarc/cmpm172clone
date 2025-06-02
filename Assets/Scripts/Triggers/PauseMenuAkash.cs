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
    public GameObject audio;
    public GameObject lang;
    public GameObject control;
    public PlayerMovementAlt pm; 
    public PlayerMovementAcc pmacc; 
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
    public GameObject audio_back;
    public GameObject audio_button;
    public GameObject lang_back;
    public GameObject lang_button;
    public GameObject control_back;
    public GameObject control_button;
    public DetectionManager dm;
    public Fader f;
    private bool NoSubMenusActive = true;
    public bool acc_mode = false;
    public Toggle acc_mode_toggle;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                Time.timeScale = 0f;
            }

            main_cam.transform.position = Vector3.MoveTowards(main_cam.transform.position, menu_cam.transform.position, speed * Time.deltaTime);
        }
        pos = player.transform.position;
        if((Input.GetKeyDown(KeyCode.Escape) || triangle || Input.GetKeyDown(KeyCode.Mouse4)) && NoSubMenusActive)
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
            if(acc_mode){
                pmacc.enabled = false;
            }
            else{
                pm.enabled = false; 
            }             EventSystem.current.SetSelectedGameObject(null); // Reset selection
            EventSystem.current.firstSelectedGameObject = option_button;
            EventSystem.current.SetSelectedGameObject(option_button); // Apply selection
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // Time.timeScale = 0f;

        }    
    }

    public void BackButtonPause(){
        if(acc_mode){
            pmacc.enabled = true;
        }
        else{
            pm.enabled = true; 
        }        
        cb.enabled = true;
        escape = false;
        Time.timeScale = 1f;
        mc.fieldOfView = 55.2f;
        grace_ui.SetActive(false);
        dm.grace = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ResetTriggers(pause);
    }
    public void BackButtonGrace(){
        if(acc_mode){
            pmacc.enabled = true;
        }
        else{
            pm.enabled = true; 
        }         
        cb.enabled = true;
        dm.grace = false;
        escape = false;
        pause.SetActive(false);
        mc.fieldOfView = 55.2f;
        grace_ui.SetActive(false);
    }
    public void BackButtonOptions(){
        ResetTriggers(options);
        pause.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = option_button;
        EventSystem.current.SetSelectedGameObject(option_button); // Apply selection
        NoSubMenusActive = true;
    }
    public void BackButtonCamera()
    {
        ResetTriggers(camera);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = camera_button;
        EventSystem.current.SetSelectedGameObject(camera_button); // Apply selection
        NoSubMenusActive = false;
    }
    public void BackButtonAudio()
    {
        ResetTriggers(audio);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = audio_button;
        EventSystem.current.SetSelectedGameObject(audio_button); // Apply selection
        NoSubMenusActive = false;
    }
    public void BackButtonLang()
    {
        ResetTriggers(lang);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = lang_button;
        EventSystem.current.SetSelectedGameObject(lang_button); // Apply selection
        NoSubMenusActive = false;
    }
    public void BackButtonControl()
    {
        ResetTriggers(control);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = control_button;
        EventSystem.current.SetSelectedGameObject(control_button); // Apply selection
        NoSubMenusActive = false;
    }
    public void AccModeControl()
    {
        acc_mode = acc_mode_toggle.isOn ? true : false;
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
        NoSubMenusActive = false;
    }
    public void CameraButtonPause()
    {
        ResetTriggers(options);
        camera.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = camera_back;
        EventSystem.current.SetSelectedGameObject(camera_back); // Apply selection
        NoSubMenusActive = false;
    }
    public void AudioButtonPause()
    {
        ResetTriggers(options);
        audio.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = audio_back;
        EventSystem.current.SetSelectedGameObject(audio_back); // Apply selection
        NoSubMenusActive = false;
    }
    public void LangButtonPause()
    {
        ResetTriggers(options);
        lang.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = lang_back;
        EventSystem.current.SetSelectedGameObject(lang_back); // Apply selection
        NoSubMenusActive = false;
    }
    public void ControlButtonPause()
    {
        ResetTriggers(options);
        control.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); // Reset selection
        EventSystem.current.firstSelectedGameObject = control_back;
        EventSystem.current.SetSelectedGameObject(control_back); // Apply selection
        NoSubMenusActive = false;
    }

    public void SaveGame(){
        SaveSystem.SavePlayer(player.transform.position);
        Debug.Log("saved.");
    }
    public void StartGame(){
        f.FadeToLevel("Overworld");
    }
    public void LoadGame(){
        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.position[0]);
        Debug.Log(data.position[1]);
        Debug.Log(data.position[2]);
        if(acc_mode){
            pmacc.enabled = true;
        }
        else{
            pm.enabled = true; 
        }         
        cb.enabled = true;
        escape = false;
        ResetTriggers(pause);
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Debug.Log(player.transform.position); 
    }

    private void ResetTriggers(GameObject menu)
    {
        Animator[] animators = menu.GetComponentsInChildren<Animator>(true);

        foreach (Animator animator in animators)
        {
            animator.Play("Normal", 0, 0f);
        }

        StartCoroutine(DelayedDeactivate(menu));
    }
    IEnumerator DelayedDeactivate(GameObject menu)
    {
        yield return null; // Wait one frame
        menu.SetActive(false);
    }
}
