using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public string activation_diary;
    public Diary d;
    public GameObject obstacles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(d.found_diaries.Contains(activation_diary)){
            obstacles.SetActive(true); 
        }
    }
}
