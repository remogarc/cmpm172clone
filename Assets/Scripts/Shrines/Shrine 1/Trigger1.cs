using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public bool switch1;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider check)
    {
        switch1 = true;
    }
    void OnTriggerExit()
    {
        switch1 = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
