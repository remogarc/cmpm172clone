using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour
{
    public bool switch2;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        switch2 = true;
    }
    void OnTriggerExit()
    {
        switch2 = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
