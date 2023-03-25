using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField]private GameObject background;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject optionsMenu;
    
    private GameObject pauseMenuButton;
    private GameObject optionsButton;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        pauseMenuButton = pauseMenu.transform.GetChild(1).gameObject;
        optionsButton = optionsMenu.transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    private void Start()
    {
        inputManager = InputManager.Instance;
        background.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);    
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("esc");
        if (inputManager.Pause())
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
        EventSystem.current.SetSelectedGameObject(pauseMenuButton);
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
        EventSystem.current.SetSelectedGameObject(optionsButton);
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
        EventSystem.current.SetSelectedGameObject(pauseMenuButton);
    }
}
