using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.EventSystems;

public class DetectionManager : MonoBehaviour
{
    // The detection range of the sphere that is created to find objects nearby the player
    [SerializeField] private float detectionRange = 1f;
    // If we want to search for a specific layer of objects so not everything is being searched, we use this.
    [SerializeField] private LayerMask detectionLayer; 
    PlayerControls pc;
    public PlayerMovementAlt pm;
    public PlayerMovementAcc pmacc;
    public LostGrace lg;
    public GameObject grace_text;
    public Camera mc;
    public CinemachineBrain cb;    // public Slider mouseSens;
    public GameObject grace_ui;
    public GameObject prompt;
    public GameObject grace_back;
    public bool grace = false;
    public bool prompt_check = false;
    public GameObject yes_button;
    public PauseMenuAkash pma; 
    public Prompt p;
    public Location l;
    public CompleteShrine cs;

    private void Awake(){
        pc = new PlayerControls();
        pc.Gameplay.SelectPS5.performed += ctx => DetectObjects();
    }
    void OnEnable() {
        pc.Gameplay.Enable();
    }
    void OnDisable(){
        pc.Gameplay.Disable();
    }
    private void Update()
    {
        // Pressing E to interact with objects/npcs from a range
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Pressed");
            DetectObjects();
        }
    }

    private void DetectObjects()
    {
        // Debug.Log("1");
        // Creates a sphere around the player and checks for colliders nearby
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);
        // Debug.Log("1.5");
        // Checks for each object found within the array of colliders and does something to each one
        foreach (Collider collider in hitColliders)
        {
            Debug.Log("2");
            // Sets collider as an object reference
            GameObject obj = collider.gameObject;
            //Debug.Log("Hit: " + obj);
            Debug.Log(obj.tag);
            Debug.Log(obj.name);
            // Returns true/false as it calls a function to compare the object's tag, will follow through if true.
            if (MatchesTag(obj))
            {
                // Debug.Log(obj.tag);
                if(obj.tag == "Grace" && pma.escape == false && prompt_check == false){
                    Debug.Log("23940oefijsodijflksdjf");
                    grace = true;
                    if(!lg.discovered_wells.Contains(obj.name)){
                        lg.discovered_wells.Add(obj.name);
                        lg.new_grace();
                    }
                    else{
                        if(!grace_ui.activeSelf){
                            EventSystem.current.SetSelectedGameObject(null); // Reset selection
                            EventSystem.current.firstSelectedGameObject = grace_back;
                            EventSystem.current.SetSelectedGameObject(grace_back); // Apply selection
                        }
                        grace_text.SetActive(false);
                        Transform grace_cam = obj.transform.Find("Camera");
                        mc.transform.rotation = grace_cam.rotation;
                        mc.transform.position = grace_cam.position;
                        if(pma.acc_mode){
                            pmacc.enabled = false;
                        }
                        else{
                            pm.enabled = false; 
                        }
                        cb.enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        grace_ui.SetActive(true);

                    }

                }
                else if(obj.tag == "Shrine" && pma.escape == false && grace == false){
                    Debug.Log("LERROOOOOOOOYYYYYY");
                    if(!prompt.activeSelf){
                        EventSystem.current.SetSelectedGameObject(null); // Reset selection
                        EventSystem.current.firstSelectedGameObject = yes_button;
                        EventSystem.current.SetSelectedGameObject(yes_button); // Apply selection
                    }
                    Debug.Log("JENKINSSSSSSSS!");
                    //pause the player script
                    prompt.SetActive(true);
                    pm.enabled = false;
                    prompt_check = true; 
                    Debug.Log(obj.name);
                    l.shrine_passed = obj.name;
                }
                else if(obj.tag == "Exit" && pma.escape == false && grace == false){
                    Debug.Log("3049-sdfsjdlfkjlskdfj");
                    if(!prompt.activeSelf){
                        EventSystem.current.SetSelectedGameObject(null); // Reset selection
                        EventSystem.current.firstSelectedGameObject = yes_button;
                        EventSystem.current.SetSelectedGameObject(yes_button); // Apply selection
                    }
                    cs.exit_shrine();
                }
                // else if(obj.tag == "Stations"){
                //     Debug.Log("found a station. ");
                //     switch(obj.name){
                //         case "Fridge":
                //             fridge.FridgeMenu();
                //             break; 
                //         case "Kitchen":
                //             kitchen.KitchenMenu();
                //             break;
                //         case "Oven":
                //             oven.KitchenMenu();
                //             break;
                //         case "Mixing":
                //             mix.KitchenMenu();
                //             break; 
                //         default: 
                //             break;
                //     }
                // }
                // Debug.Log("Interaction found");
                // // Makes a interactionhandler reference and connects with the script on the object found
                InteractionHandler interaction = obj.GetComponent<InteractionHandler>();
                // If the script on the object does exist, do the thing
                if (interaction != null)
                {

                    // Debug.Log("Call interactor");
                    interaction.Interact(); // Call the interactionhandler's interact function
                    if(obj.name == "diary"){
                        cs.finish_shrine();
                    }
                }
                else{
                    Debug.Log(interaction);
                }
            }
        }
    }

    // Function to compare the object's tag to see if it matches
    private bool MatchesTag(GameObject obj)
    {
        switch (obj.tag)
        {
            case "NPC":
            case "Object":
            case "Objective":
            case "Collectible":
            case "Stations":
            case "Grace":
            case "Shrine":
            case "diary":
            case "Exit":
                return true; // Accept any matching tags
                Debug.Log("found something");
            default:
                return false; //appropriate tag not found
                Debug.Log("found nothing");
        }
    }
}
