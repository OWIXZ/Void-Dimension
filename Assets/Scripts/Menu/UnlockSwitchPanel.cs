using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnlockSwitchPanel : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] private Movement Moving;
    public GameObject UnlockSwitchUI;
    public GameObject ObjectToToggle; // GameObject à activer/désactiver
    public GameObject SecondObjectToToggle; // Deuxième GameObject à activer/désactiver

    [SerializeField] private Button defaultSelectedButton; // Référence au bouton par défaut

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
            ObjectToToggle.SetActive(false); // Désactiver le premier GameObject
        }

        if (SecondObjectToToggle != null)
        {
            SecondObjectToToggle.SetActive(false); // Désactiver le deuxième GameObject
        }

        // Définir le bouton par défaut
        if (defaultSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // Désélectionne tout autre élément actuellement sélectionné
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
            ObjectToToggle.SetActive(true); // Réactiver le premier GameObject
        }

        if (SecondObjectToToggle != null)
        {
            SecondObjectToToggle.SetActive(true); // Réactiver le deuxième GameObject
        }
    }
}
