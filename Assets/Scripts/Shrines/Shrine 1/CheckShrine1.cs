using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckShrine1 : MonoBehaviour
{
    public Trigger1 t1;
    public Trigger2 t2;
    public bool complete = false;
    public GameObject big_text; 
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(t1.switch1 && t2.switch2){
            complete = true;
            big_text.SetActive(true);
            t.text = "Shrine Complete";
        }
    }
}
