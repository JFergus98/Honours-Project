using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private GameObject PlayerPrefab;
    private CinemachineVirtualCamera vCam;
    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private Slider fovSlider;
    [SerializeField] private TextMeshProUGUI fovText;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private TextMeshProUGUI sensText;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        vCam = PlayerPrefab.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        Debug.Log(PlayerPrefs.GetInt("MasterVolume"));
        Debug.Log(PlayerPrefs.GetInt("FOV"));
        Debug.Log(PlayerPrefs.GetInt("MouseSensitivity"));

        volumeSlider.value = PlayerPrefs.GetInt("MasterVolume");
        fovSlider.value = PlayerPrefs.GetInt("FOV");
        sensSlider.value = PlayerPrefs.GetInt("MouseSensitivity");

        Debug.Log(PlayerPrefs.GetInt("MasterVolume"));
        Debug.Log(PlayerPrefs.GetInt("FOV"));
        Debug.Log(PlayerPrefs.GetInt("MouseSensitivity"));
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Master Volume", volume);
        volumeText.text = ((volume+80)).ToString();

        PlayerPrefs.SetInt("MasterVolume", ((int)volume));
    }

    public void SetFOV(float fov)
    {
        fovText.text = (fov+80).ToString();

        PlayerPrefs.SetInt("FOV", ((int)fov));

        vCam.m_Lens.FieldOfView = fov+80;
    }

    public void SetMouseSensitivity(float sens)
    {
        sensText.text = (sens+50).ToString();

        PlayerPrefs.SetInt("MouseSensitivity", ((int)sens));
    }
}
