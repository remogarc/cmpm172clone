using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public bool y = false; 
    public bool n = false;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void yes_pressed(){
        y = true; 
        n = false; 
        count+=1;
        Debug.Log("y is now: " + y);
    }
    public void no_pressed(){
        n = true; 
        y = false;
        Debug.Log("n is now: " + n);
    }
}
