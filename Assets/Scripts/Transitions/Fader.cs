using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Animator a;
    public GameObject fade; 
    string level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(string scene_name){
        fade.SetActive(true);
        level = scene_name;
    }

    public void OnCompleteFade(){
        SceneManager.LoadScene(level);
    }
}
