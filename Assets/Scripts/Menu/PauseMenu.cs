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
    [SerializeField] private SpriteManager spriteManager; // Ajoutez cette référence
    [SerializeField] private UnifiedTileSwitch SwitchManager; // Ajoutez cette référence

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
        spriteManager.SetActive(false); // Désactiver le SpriteManager
        SwitchManager.SetActive(false); // Désactiver le SwitchManager
        tileSwitch.enabled = false;
        playerInput.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        AudioManager.Instance.PauseAudio();
    }

    public void Resume()
    {
        Moving.ResetMovement(); // Réinitialise le mouvement du joueur
        Moving.isMooving = true;
        spriteManager.SetActive(true); // Réactiver le SpriteManager
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
