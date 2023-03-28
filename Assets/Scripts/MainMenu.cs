using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GhostScriptableObject ghostScrObj;
    [SerializeField]
    private SeedScriptableObject seedScrObj;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject levelSelectMenu;

    [SerializeField]
    private GameObject firstMenuButton;
    [SerializeField]
    private GameObject firstOptionsButton;
    [SerializeField]
    private GameObject firstLevelSelectButton;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Image[] levelCompletedImages = levelSelectMenu.transform.GetChild(1).gameObject.GetComponentsInChildren<Image>();

        for (int i = 0; i < levelCompletedImages.Length; i++)
        {
            Image levelCompletedImage = levelCompletedImages[i];

            if (PlayerPrefs.GetInt("CompletedLevel" + (i+1)) == 1)
            {
                levelCompletedImage.enabled = true;
            }else{
                levelCompletedImage.enabled = false;
            }
        }

        GameObject inputManager = GameObject.Find("InputManager");
        GameObject loopCounter = GameObject.Find("LoopCounter");

        if (inputManager)
        {
            Destroy(inputManager);
        }

        if (loopCounter)
        {
            Destroy(loopCounter);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstMenuButton);
    }
    
    public void Play()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstLevelSelectButton);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionsButton);
    }

    public void Quit()
    {
        Debug.Log("Quit");  // testing

        Application.Quit();
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstMenuButton);
    }

    public void LoadLevel(int index)
    {
        ghostScrObj.ghosts.Clear();
        seedScrObj.GenerateSeed();
        
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
