using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class ShrineTimer : MonoBehaviour
{
    public bool out_of_time;
    float timer = 300f;
    public Text t;
    public PauseMenuAkash pma;
    public PlayerMovementAlt pm;
    public GameObject game_over;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0f){
            out_of_time = true; 
            pm.enabled = false;
            game_over.SetActive(true); 
        }
        else{
            if(pma.escape){timer -= 0f;}
            else{timer -= Time.deltaTime;}

            t.text = timer.ToString("0");
            out_of_time = false;
        }
    }
    public void return_to_overworld(){
        game_over.SetActive(false);
        SceneManager.LoadScene("Overworld");
    }
}
