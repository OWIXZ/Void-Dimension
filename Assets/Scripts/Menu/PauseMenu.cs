using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private Movement Moving;
    [SerializeField] private UnifiedTileSwitch tileSwitch;
    [SerializeField] private PlayerInput playerInput;
    public GameObject pauseMenuUI;
    public GameObject objectToToggle; // Référence au GameObject à activer/désactiver

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Vérifiez que ni le panneau des capacités ni celui du switch ne sont ouverts
            if (!UnlockAbilitiesPanel.UnlockAbilitiesIsOpen)
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
    }

    public void Paused()
    {
        Moving.isMooving = false;
        tileSwitch.enabled = false;
        playerInput.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        AudioManager.Instance.PauseAudio();

        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false); // Désactiver le GameObject
        }
    }

    public void Resume()
    {
        Moving.ResetMovement(); // Réinitialise le mouvement du joueur
        Moving.isMooving = true;
        tileSwitch.enabled = true;
        playerInput.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.ResumeAudio();

        if (objectToToggle != null)
        {
            objectToToggle.SetActive(true); // Réactiver le GameObject
        }
    }

    public void LoadMainMenu()
    {
        Moving.isMooving = true;
        tileSwitch.enabled = true;
        playerInput.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene("Main_Menu");
    }
}
