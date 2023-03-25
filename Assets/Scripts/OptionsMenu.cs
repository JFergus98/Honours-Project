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

    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    private CinemachineVirtualCamera vCamPrefab;
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
        player = GameObject.Find("Player");
        
        if (player)
        {
            vCam = player.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();
        }

        vCamPrefab = playerPrefab.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        volumeSlider.value = PlayerPrefs.GetInt("MasterVolume");
        fovSlider.value = PlayerPrefs.GetInt("FOV");
        sensSlider.value = PlayerPrefs.GetFloat("MouseSensitivity")*100;

        Debug.Log(PlayerPrefs.GetFloat("MouseSensitivity")*100); // Testing
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

        if (vCam)
        {
            vCam.m_Lens.FieldOfView = fov+80;
        }

        vCamPrefab.m_Lens.FieldOfView = fov+80;
    }

    public void SetMouseSensitivity(float sens)
    {
        sensText.text = (sens+50).ToString();

        PlayerPrefs.SetFloat("MouseSensitivity", (sens/100));

        //Debug.Log(PlayerPrefs.GetFloat("MouseSensitivity")); // Testing
    }
}
