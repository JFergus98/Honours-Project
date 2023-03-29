using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;

    private CinemachineVirtualCamera vCamPrefab;
    private CinemachineVirtualCamera vCam;
    
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private TextMeshProUGUI volumeText;
    [SerializeField]
    private Slider fovSlider;
    [SerializeField]
    private TextMeshProUGUI fovText;
    [SerializeField]
    private Slider sensSlider;
    [SerializeField]
    private TextMeshProUGUI sensText;

    [SerializeField]
    private Toggle invertVerticalCameraToggle;
    [SerializeField]
    private Toggle invertHorizontalCameraToggle;

    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject customiseKeybindsMenu;

    [SerializeField]
    private GameObject firstOptionsButton;
    [SerializeField]
    private GameObject firstCustomiseKeybindsButton;

    private const float defaultMasterVolume = 1;
    private const int defaultFOV = 80;
    private const float defaultMouseSensitivity = 0.125f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        PlayerPrefs.DeleteAll();

        player = GameObject.Find("Player");
        
        if (player)
        {
            vCam = player.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();
        }

        vCamPrefab = playerPrefab.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();

        checkSetDefaultSetting();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        optionsMenu.SetActive(true);
        customiseKeybindsMenu.SetActive(false);
        
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        fovSlider.value = PlayerPrefs.GetInt("FOV");
        sensSlider.value = PlayerPrefs.GetFloat("MouseSensitivity")*400;
        invertVerticalCameraToggle.isOn = (PlayerPrefs.GetInt("InvertVerticalCamera") == 1);
        invertHorizontalCameraToggle.isOn = (PlayerPrefs.GetInt("InvertHorizontalCamera") == 1);
    }

    private void checkSetDefaultSetting()
    {
        if (PlayerPrefs.GetFloat("MasterVolume") == 0)
        {
            PlayerPrefs.SetFloat("MasterVolume", defaultMasterVolume);
        }

        if (PlayerPrefs.GetInt("FOV") == 0)
        {
            PlayerPrefs.SetInt("FOV", defaultFOV);
        }

        if (PlayerPrefs.GetFloat("MouseSensitivity") == 0)
        {
            PlayerPrefs.SetFloat("MouseSensitivity", defaultMouseSensitivity);
        }
    }

    public void SetVolume(float volume)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }
        
        audioMixer.SetFloat("Master Volume", Mathf.Log10(volume) * 20);
        volumeText.text = (volume*100).ToString("N0");

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetFOV(float fov)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }
        
        fovText.text = (fov).ToString();

        PlayerPrefs.SetInt("FOV", ((int)fov));

        if (vCam)
        {
            vCam.m_Lens.FieldOfView = fov;
        }
        
        if (vCamPrefab)
        {
            vCamPrefab.m_Lens.FieldOfView = fov;
        }
    }

    public void SetMouseSensitivity(float sens)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }

        sensText.text = (sens).ToString();

        PlayerPrefs.SetFloat("MouseSensitivity", (sens/400));
    }

    public void SetInvertVerticalCamera(bool isInverted)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }

        PlayerPrefs.SetInt("InvertVerticalCamera", (isInverted ? 1 : 0));
    }

    public void SetInvertHorizontalCamera(bool isInverted)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }

        PlayerPrefs.SetInt("InvertHorizontalCamera", (isInverted ? 1 : 0));
    }

    public void CustomizeKeyBindings()
    {
        optionsMenu.SetActive(false);
        customiseKeybindsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCustomiseKeybindsButton);
    }

    public void Back()
    {
        customiseKeybindsMenu.SetActive(false);
        optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionsButton);
    }
}
