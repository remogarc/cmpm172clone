using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PlayerMovement;

public class Options : MonoBehaviour
{
    public GameObject settings; 
    public GameObject escape;
    public CinemachineFreeLook camera;    
    public Text currentXSensitivity;
    public Slider sx;
    
    public Text currentYSensitivity;
    public Slider sy;
    public Toggle x;
    public Toggle y;
    
    public Slider turn;
    public Text currentTurn; 


    public Text currentFOV;
    public Slider fov;

    public Slider musicVolumeSlider;
    public Text currentVolumeText;
    public AudioSource musicSource;

    private void Awake(){
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if(brain == null){
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
        camera.m_CommonLens = true;

        sx.value = (float)camera.m_XAxis.m_MaxSpeed/(float)316.66;
        currentXSensitivity.text = ((double)camera.m_XAxis.m_MaxSpeed).ToString("0");

        sy.value = (float)camera.m_YAxis.m_MaxSpeed/(float)5.0;
        currentYSensitivity.text = ((double)camera.m_YAxis.m_MaxSpeed * 100.0).ToString("0");

        fov.value = (float)camera.m_Lens.FieldOfView/(float)100.0;
        currentFOV.text = ((double)camera.m_Lens.FieldOfView * 100.0).ToString("0");

        if(camera.m_XAxis.m_InvertInput){
            x.isOn = true;
        }
        if(camera.m_YAxis.m_InvertInput){
            y.isOn = true;
        }

        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = musicVolumeSlider.value;
        currentVolumeText.text = (musicVolumeSlider.value * 100f).ToString("0");

        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }



    public void changeXSensitivity(){
        currentXSensitivity.text = (sx.value * 316.66).ToString();
        camera.m_XAxis.m_MaxSpeed =  (float)316.66 * (float)sx.value;
    }

    public void changeYSensitivity(){
        currentYSensitivity.text = (sy.value * 500.0).ToString();
        camera.m_YAxis.m_MaxSpeed =  (float)5.0 * (float)sy.value;
    }
    public void changeFOV(){
        currentFOV.text = (fov.value * 100.0).ToString();
        camera.m_Lens.FieldOfView =  (float)100.0 * (float)fov.value;
    }
    public void invertXAxis(){
        camera.m_XAxis.m_InvertInput = x.isOn ? true : false;
    }

    public void invertYAxis(){
        camera.m_YAxis.m_InvertInput = y.isOn ? true : false;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
        currentVolumeText.text = (volume * 100f).ToString("0");
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
