using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Location : MonoBehaviour
{
    public GameObject marker;
    public bool isTriggered;
    public LayerMask mask;
    public GameObject location_text;
    public Text t;
    public string s;
    public float targetTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
