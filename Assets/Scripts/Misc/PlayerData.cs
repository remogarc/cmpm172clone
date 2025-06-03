using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public string scene;
    public int diary_count;
    public string[] diaries;
    // Corrected constructor where the parameter 'pc' is assigned to 'player_coordinates'
    public PlayerData(Vector3 pc, List<string> f_diaries)
    {
        position = new float[3];
        position[0] = pc.x;
        position[1] = pc.y;
        position[2] = pc.z;

        diaries = new string[11];
        int count = 0;

        foreach(var x in f_diaries){
            diaries[count] = x; 
            count+=1;
        }
        while(count < 11){
            diaries[count] = "none";
            count+=1;
        }
    }
}
