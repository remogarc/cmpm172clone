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
        // Validate essential references first
        if (camera == null)
        {
            Debug.LogError($"Options on '{gameObject.name}': CinemachineFreeLook camera is not assigned!");
            return;
        }
        
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if(brain == null){
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
        camera.m_CommonLens = true;

        // X Sensitivity setup
        if (sx != null && currentXSensitivity != null)
        {
            sx.value = (float)camera.m_XAxis.m_MaxSpeed/(float)316.66;
            currentXSensitivity.text = ((double)camera.m_XAxis.m_MaxSpeed).ToString("0");
        }
        else
        {
            if (sx == null) Debug.LogWarning($"Options on '{gameObject.name}': X Sensitivity slider 'sx' is not assigned!");
            if (currentXSensitivity == null) Debug.LogWarning($"Options on '{gameObject.name}': X Sensitivity text 'currentXSensitivity' is not assigned!");
        }

        // Y Sensitivity setup
        if (sy != null && currentYSensitivity != null)
        {
            sy.value = (float)camera.m_YAxis.m_MaxSpeed/(float)5.0;
            currentYSensitivity.text = ((double)camera.m_YAxis.m_MaxSpeed * 100.0).ToString("0");
        }
        else
        {
            if (sy == null) Debug.LogWarning($"Options on '{gameObject.name}': Y Sensitivity slider 'sy' is not assigned!");
            if (currentYSensitivity == null) Debug.LogWarning($"Options on '{gameObject.name}': Y Sensitivity text 'currentYSensitivity' is not assigned!");
        }

        // FOV setup
        if (fov != null && currentFOV != null)
        {
            fov.value = (float)camera.m_Lens.FieldOfView/(float)100.0;
            currentFOV.text = ((double)camera.m_Lens.FieldOfView * 100.0).ToString("0");
        }
        else
        {
            if (fov == null) Debug.LogWarning($"Options on '{gameObject.name}': FOV slider 'fov' is not assigned!");
            if (currentFOV == null) Debug.LogWarning($"Options on '{gameObject.name}': FOV text 'currentFOV' is not assigned!");
        }

        // Invert axis toggles
        if (x != null)
        {
            if(camera.m_XAxis.m_InvertInput){
                x.isOn = true;
            }
        }
        else
        {
            Debug.LogWarning($"Options on '{gameObject.name}': X invert toggle 'x' is not assigned!");
        }
        
        if (y != null)
        {
            if(camera.m_YAxis.m_InvertInput){
                y.isOn = true;
            }
        }
        else
        {
            Debug.LogWarning($"Options on '{gameObject.name}': Y invert toggle 'y' is not assigned!");
        }

        // Music volume setup
        if (musicVolumeSlider != null && musicSource != null && currentVolumeText != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            musicSource.volume = musicVolumeSlider.value;
            currentVolumeText.text = (musicVolumeSlider.value * 100f).ToString("0");
            musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        }
        else
        {
            if (musicVolumeSlider == null) Debug.LogWarning($"Options on '{gameObject.name}': Music volume slider 'musicVolumeSlider' is not assigned!");
            if (musicSource == null) Debug.LogWarning($"Options on '{gameObject.name}': Audio source 'musicSource' is not assigned!");
            if (currentVolumeText == null) Debug.LogWarning($"Options on '{gameObject.name}': Volume text 'currentVolumeText' is not assigned!");
        }
    }



    public void changeXSensitivity(){
        if (currentXSensitivity != null && sx != null && camera != null)
        {
            currentXSensitivity.text = (sx.value * 316.66).ToString();
            camera.m_XAxis.m_MaxSpeed =  (float)316.66 * (float)sx.value;
        }
    }

    public void changeYSensitivity(){
        if (currentYSensitivity != null && sy != null && camera != null)
        {
            currentYSensitivity.text = (sy.value * 500.0).ToString();
            camera.m_YAxis.m_MaxSpeed =  (float)5.0 * (float)sy.value;
        }
    }
    
    public void changeFOV(){
        if (currentFOV != null && fov != null && camera != null)
        {
            currentFOV.text = (fov.value * 100.0).ToString();
            camera.m_Lens.FieldOfView =  (float)100.0 * (float)fov.value;
        }
    }
    
    public void invertXAxis(){
        if (x != null && camera != null)
        {
            camera.m_XAxis.m_InvertInput = x.isOn ? true : false;
        }
    }

    public void invertYAxis(){
        if (y != null && camera != null)
        {
            camera.m_YAxis.m_InvertInput = y.isOn ? true : false;
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        if (musicSource != null && currentVolumeText != null)
        {
            musicSource.volume = volume;
            currentVolumeText.text = (volume * 100f).ToString("0");
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }
}
