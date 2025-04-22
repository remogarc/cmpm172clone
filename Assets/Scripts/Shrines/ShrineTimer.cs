using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShrineTimer : MonoBehaviour
{
    public bool out_of_time;
    float timer = 300f;
    public Text t;
    public PauseMenuAkash pma;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer == 0f){
            out_of_time = true; 
            t.text = "Time's up!";
        }
        else{
            if(pma.escape){timer -= 0f;}
            else{timer -= Time.deltaTime;}

            t.text = timer.ToString("0");
            out_of_time = false;
        }
    }
}
