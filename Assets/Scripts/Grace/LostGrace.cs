using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostGrace : MonoBehaviour
{
    public List<string> discovered_wells = new List<string>();
    public GameObject grace_text;
    public Text t;
    public float timer = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void new_grace(){
        Debug.Log("function called.");
        grace_text.SetActive(true);
        t.text = "Wishing Well Discovered";
        // while(timer > 0f){
        //     timer -= Time.deltaTime;
        // }
        // grace_text.SetActive(false);
    }

    public void grace(){
        
    }
}
