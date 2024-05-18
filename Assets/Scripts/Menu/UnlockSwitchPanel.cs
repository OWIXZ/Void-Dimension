using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnlockSwitchPanel : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] private Movement Moving;
    public GameObject UnlockSwitchUI;
    public GameObject ObjectToToggle; // GameObject � activer/d�sactiver
    public GameObject SecondObjectToToggle; // Deuxi�me GameObject � activer/d�sactiver

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
        UnlockSwitchUI.SetActive(true);
        UnlockAbilitiesPanel.UnlockAbilitiesIsOpen = true;
        Time.timeScale = 0;
        gameIsPaused = true;
        AudioManager.Instance.PauseAudio();

        if (ObjectToToggle != null)
        {
            ObjectToToggle.SetActive(false); // D�sactiver le premier GameObject
        }

        if (SecondObjectToToggle != null)
        {
            SecondObjectToToggle.SetActive(false); // D�sactiver le deuxi�me GameObject
        }

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
        UnlockSwitchUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.ResumeAudio();
        UnlockAbilitiesPanel.UnlockAbilitiesIsOpen = false;

        if (ObjectToToggle != null)
        {
            ObjectToToggle.SetActive(true); // R�activer le premier GameObject
        }

        if (SecondObjectToToggle != null)
        {
            SecondObjectToToggle.SetActive(true); // R�activer le deuxi�me GameObject
        }
    }
}
