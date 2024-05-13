using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Pour le Button
using UnityEngine.EventSystems; // Pour EventSystem

public class UnlockAbilitiesPanel : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private Movement Moving;
    [SerializeField] private UnifiedTileSwitch tileSwitch;
    [SerializeField] private PlayerInput playerInput;
    public GameObject UnlockDashUI;

    [SerializeField] private Button defaultSelectedButton; // R�f�rence au bouton Resume


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
        UnlockDashUI.SetActive(true);
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
        UnlockDashUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.ResumeAudio();
    }
}




















