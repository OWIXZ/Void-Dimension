using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] Movement Moving;
    public GameObject pauseMenuUI;

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }


    void Paused()
    {
            Moving.isMooving = false;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            gameIsPaused = true;
        
    }

    public void Resume()
    {
            Moving.isMooving = true;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
        Moving.isMooving = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene("Main_Menu");
    }
}
