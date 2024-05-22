using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private Movement Moving;
    [SerializeField] private UnifiedTileSwitch tileSwitch;
    [SerializeField] private PlayerInput playerInput;
    public GameObject pauseMenuUI;
    [SerializeField] private SpriteManager spriteManager; // Ajoutez cette r�f�rence
    [SerializeField] private UnifiedTileSwitch SwitchManager; // Ajoutez cette r�f�rence

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
        spriteManager.SetActive(false); // D�sactiver le SpriteManager
        SwitchManager.SetActive(false); // D�sactiver le SwitchManager
        tileSwitch.enabled = false;
        playerInput.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        AudioManager.Instance.PauseAudio();
    }

    public void Resume()
    {
        Moving.ResetMovement(); // R�initialise le mouvement du joueur
        Moving.isMooving = true;
        spriteManager.SetActive(true); // R�activer le SpriteManager
        SwitchManager.SetActive(true); // Active le SwitchManager
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
        tileSwitch.enabled = true;
        playerInput.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene("Main_Menu");
    }
}
