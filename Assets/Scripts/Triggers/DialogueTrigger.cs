using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public LayerMask mask;
    public Transform dialogueSphere;
    public float dialogueDistance = 10f;
    public bool isTriggered;

    public bool enabled = true;

    public GameObject comp_selector;
    public GameObject ps5_selector;
    void Update()
    {
        isTriggered = Physics.CheckSphere(dialogueSphere.position, dialogueDistance, mask);
        if(isTriggered && enabled){
            if(PlayerMovement.ps5){
                ps5_selector.SetActive(true);
            }
            else{
                comp_selector.SetActive(true);
            }
            // else{
            //     box.SetActive(false);
            // }
        }
        else{
            // Debug.Log("Not within range.");
            comp_selector.SetActive(false);
            ps5_selector.SetActive(false);
            // box.SetActive(false);
        }
        // if(!Input.GetKeyDown("e") && !isTriggered){
        //     box.SetActive(false);
        // }
        // else{
        //     box.SetActive(true);
        // }
    }
}
