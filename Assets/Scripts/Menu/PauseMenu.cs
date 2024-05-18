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
    public GameObject objectToToggle; // R�f�rence au GameObject � activer/d�sactiver

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // V�rifiez que ni le panneau des capacit�s ni celui du switch ne sont ouverts
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
            objectToToggle.SetActive(false); // D�sactiver le GameObject
        }
    }

    public void Resume()
    {
        Moving.ResetMovement(); // R�initialise le mouvement du joueur
        Moving.isMooving = true;
        tileSwitch.enabled = true;
        playerInput.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.ResumeAudio();

        if (objectToToggle != null)
        {
            objectToToggle.SetActive(true); // R�activer le GameObject
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
