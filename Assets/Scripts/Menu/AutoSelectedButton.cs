using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelectButton : MonoBehaviour
{
    public GameObject panel; // Le panneau qui s'ouvre ou se ferme
    public Button defaultButton; // Le bouton � s�lectionner automatiquement lorsque le panneau est ouvert

    private GameObject lastSelectedButton; // Pour m�moriser le dernier bouton s�lectionn� avant l'ouverture du panneau
    private bool panelWasActiveLastFrame = false; // Pour suivre l'�tat du panneau � la derni�re frame

    void Update()
    {
        if (panel.activeSelf)
        {
            if (!panelWasActiveLastFrame)
            {
                lastSelectedButton = EventSystem.current.currentSelectedGameObject;
                if (defaultButton != null && defaultButton.gameObject.activeInHierarchy && defaultButton.interactable)
                {
                    EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
                }
            }
        }
        else if (panelWasActiveLastFrame && lastSelectedButton != null)
        {
            if (((GameObject)lastSelectedButton).activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
            }
            lastSelectedButton = null;
        }

        panelWasActiveLastFrame = panel.activeSelf;
    }
}
