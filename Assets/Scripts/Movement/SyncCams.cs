using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCams : MonoBehaviour
{
    public GameObject main_cam; 
    public GameObject cam2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam2.transform.position = main_cam.transform.position;
        cam2.transform.rotation = main_cam.transform.rotation;
    }
}
