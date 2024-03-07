using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] Movement Moving;

    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
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
        Time.timeScale = 1;   
        gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
