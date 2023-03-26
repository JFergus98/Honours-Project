using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private InputManager inputManager;
    private LoopCounter loopCounter;

    [SerializeField]
    private GhostScriptableObject ghostScrObj;
    [SerializeField]
    private SeedScriptableObject seedScrObj;

    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject levelCompletedMenu;
    [SerializeField]
    private TextMeshProUGUI timer;
    
    [SerializeField]
    private GameObject firstPauseMenuButton;
    [SerializeField]
    private GameObject firstOptionsButton;
    [SerializeField]
    private GameObject firstLevelCompletedButton;

    [SerializeField]
    private TextMeshProUGUI loopValue;
    [SerializeField]
    private TextMeshProUGUI timeValue;

    private float time;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        background.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelCompletedMenu.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        inputManager = InputManager.Instance;
        loopCounter = LoopCounter.Instance;

        // Set time to 0
        time = 0;

        loopCounter.incrementLoopCount();
    }

    // Update is called once per frame
    private void Update()
    {
        if (inputManager.Pause() && !levelCompletedMenu.activeInHierarchy)
        {
            if (background.activeInHierarchy)
            {
                Debug.Log("resume");
                ResumeGame();
            }
            else
            {
                Debug.Log("pause");
                PauseGame();
            }
        }

        // Increment time
        time += Time.deltaTime;

        timer.text = (time).ToString("00.00");
        timeValue.text = (time).ToString("00.00");
        loopValue.text = loopCounter.loopCount.ToString();
    }
    
    public void ResumeGame()
    {
        background.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
    }
    
    private void PauseGame()
    {
        Time.timeScale = 0;

        background.SetActive(true);
        pauseMenu.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseMenuButton);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionsButton);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(0,  LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");  // testing

        Application.Quit();
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseMenuButton);
    }

    public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index > SceneManager.sceneCountInBuildSettings)
        {
            ReturnToMenu();
        }

        LoadLevel(index);
    }

    public void ReloadLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        LoadLevel(index);
    }

    private void LoadLevel(int index)
    {
        Destroy(inputManager);
        Destroy(loopCounter);

        ghostScrObj.ghosts.Clear();
        seedScrObj.GenerateSeed();

        SceneManager.LoadScene(index, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        Time.timeScale = 1;
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0;

        if (SceneManager.GetActiveScene().buildIndex - 1 > PlayerPrefs.GetInt("CompletedLevels"))
        {
            PlayerPrefs.SetInt("CompletedLevels", SceneManager.GetActiveScene().buildIndex - 1);
        }

        background.SetActive(true);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        levelCompletedMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstLevelCompletedButton);
    }
}
