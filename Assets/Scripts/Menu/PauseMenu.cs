using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private Movement Moving; // Script de mouvement
    [SerializeField] private UnifiedTileSwitch tileSwitch; // Script de changement de tuile
    [SerializeField] private PlayerInput playerInput; // Composant d'input du joueur
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
        tileSwitch.enabled = false;
        playerInput.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        AudioManager.Instance.PauseAudio();
    }

    public void Resume()
    {
        Moving.isMooving = true;
        tileSwitch.enabled = true;
        playerInput.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.ResumeAudio();
    }


    public void LoadMainMenu()
    {
        Moving.isMooving = true;
        tileSwitch.enabled = true; // Assurez-vous de réactiver le script lors du chargement du menu
        playerInput.enabled = true; // Réactive l'input du joueur
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene("Main_Menu");
    }
}

