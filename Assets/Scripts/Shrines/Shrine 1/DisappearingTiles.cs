using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingTiles : MonoBehaviour
{
    public GameObject next_platform;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        Debug.Log("trigger set");
        next_platform.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
