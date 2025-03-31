using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectionManager : MonoBehaviour
{
    // The detection range of the sphere that is created to find objects nearby the player
    [SerializeField] private float detectionRange = 1f;
    // If we want to search for a specific layer of objects so not everything is being searched, we use this.
    [SerializeField] private LayerMask detectionLayer; 
    PlayerControls pc;

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
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed");
            DetectObjects();
        }
    }

    private void DetectObjects()
    {
        Debug.Log("1");
        // Creates a sphere around the player and checks for colliders nearby
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);
        Debug.Log("1.5");
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
                // if(obj.tag == "Collectible"){
                //     im.CollectItem(obj, Inventory.ItemType.Ingredient);
                // }
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
                return true; // Accept any matching tags
                Debug.Log("found something");
            default:
                return false; //appropriate tag not found
                Debug.Log("found nothing");
        }
    }
}
