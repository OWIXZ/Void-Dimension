using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnlockSwitchPanel : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] private Movement Moving;
    [SerializeField] private UnifiedTileSwitch tileSwitch;
    [SerializeField] private PlayerInput playerInput;
    public GameObject UnlockSwitchUI;

    [SerializeField] private Button defaultSelectedButton; // R�f�rence au bouton par d�faut

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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

    public void Paused()
    {
        Moving.isMooving = false;
        tileSwitch.enabled = false;
        playerInput.enabled = false;
        UnlockSwitchUI.SetActive(true);
        UnlockAbilitiesPanel.UnlockAbilitiesIsOpen = true;
        Time.timeScale = 0;
        gameIsPaused = true;
        AudioManager.Instance.PauseAudio();

        // D�finir le bouton par d�faut
        if (defaultSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // D�s�lectionne tout autre �l�ment actuellement s�lectionn�
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton.gameObject);
        }
    }

    public void Resume()
    {
        Moving.isMooving = true;
        tileSwitch.enabled = true;
        playerInput.enabled = true;
        UnlockSwitchUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.ResumeAudio();
        UnlockAbilitiesPanel.UnlockAbilitiesIsOpen = false;
    }
}
