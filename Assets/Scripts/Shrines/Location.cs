using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    public GameObject marker;
    public bool isTriggered;
    public LayerMask mask;
    public GameObject location_text;
    public Text t;
    public string s;
    public float targetTime = 10.0f;
    public Prompt p;
    public GameObject y_n_prompt;
    public string shrine_passed = "none"; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(y_n_prompt.activeSelf){
            trigger_shrine(shrine_passed);
        }
        isTriggered = Physics.CheckSphere(marker.transform.position, 500f, mask);
        if(isTriggered){
            t.text = s; 
            location_text.SetActive(true);
            // if(targetTime <= 0.0f){
            //     location_text.SetActive(false);
            //     targetTime = 10.0f;
            // }
            // else{
            //     targetTime -= Time.deltaTime;
            // }
        }
        else{
            location_text.SetActive(false);
        }
       
    }
    public void trigger_shrine(string shrine_name){
        Debug.Log("yes is " + p.y);
        Debug.Log("no is " + p.n);
        if(p.y){

            Debug.Log("Trigger Shrine");
            p.y = false; 
            p.n = false;
            y_n_prompt.SetActive(false);
            SceneManager.LoadScene(shrine_name);
        }
    }
}
