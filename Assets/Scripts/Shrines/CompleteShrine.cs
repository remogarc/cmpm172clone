using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteShrine : MonoBehaviour
{
    public bool shrine_done = false;
    public List<string> finished_shrines = new List<string>();
    public GameObject complete_text;
    public Text t;
    public float timer = 4f;
    public GameObject y_n_prompt;
    public Text exit_text;
    public DetectionManager dm;
    public PlayerMovementAlt pm;
    public Prompt p;
    public Fader f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(y_n_prompt.activeSelf){
            if(p.y && p.count >= 1){
                p.count = 0;
                Debug.Log("Trigger Shrine");
                p.y = false; 
                p.n = false;
                y_n_prompt.SetActive(false);
                f.FadeToLevel("Overworld");
            }
            else if(p.n){
                p.count = 0;
                p.y = false; 
                p.n = false;
                y_n_prompt.SetActive(false);
            }
        }
    }
    public void finish_shrine(){    
        finished_shrines.Add(SceneManager.GetActiveScene().name);
        t.text = "Shrine Complete";
        complete_text.SetActive(true);
    }

    public void exit_shrine(){
        Debug.Log("exit prompt starting.."); 
        exit_text.text = "Exit to overworld?";
        dm.prompt_check = true;
        pm.enabled = false;
        y_n_prompt.SetActive(true);
    }
}
