using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]private GhostScriptableObject ghostScrObj;
    [SerializeField]private SeedScriptableObject seedScrObj;

    [SerializeField]private GameObject mainMenu;
    [SerializeField]private GameObject optionsMenu;
    [SerializeField]private GameObject levelSelectMenu;

    private GameObject mainMenuButton;
    private GameObject optionsButton;
    private GameObject levelSelectButton;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        mainMenuButton = mainMenu.transform.GetChild(0).gameObject;
        optionsButton = optionsMenu.transform.GetChild(0).gameObject;
        levelSelectButton = levelSelectMenu.transform.GetChild(0).GetChild(0).gameObject;

        GameObject inputManager = GameObject.Find("InputManager");
        GameObject loopCounter = GameObject.Find("LoopCounter");

        Debug.Log(inputManager);
        Debug.Log(loopCounter);

        if (inputManager)
        {
            Destroy(inputManager);
        }

        if (loopCounter)
        {
            Destroy(loopCounter);
        }
    }

    private void Update(){
        
            Destroy(GameObject.Find("InputManager"));
    }

    // Start is called before the first frame update
    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }
    
    public void Play()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectButton);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButton);
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
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }

    public void LoadLevel(int index)
    {
        ghostScrObj.ghosts.Clear();
        seedScrObj.GenerateSeed();
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
