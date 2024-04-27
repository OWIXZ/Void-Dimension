using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenuOption : MonoBehaviour
{
    public GameObject Panel;
    bool visible = false;

    public TMP_Dropdown DResolution;


    private GameObject originalFirstSelected; // Store the original First Selected object

    private void Start()
    {
        // Store the original First Selected object from the Event System
        if (EventSystem.current != null)
        {
            originalFirstSelected = EventSystem.current.firstSelectedGameObject;
        }
    }

    // Method to manage closing the options panel
    public void EscapePause(InputAction.CallbackContext context)
    {
        if (context.performed && visible)
        {
            visible = false;
            Panel.SetActive(false);
            // Restore the original First Selected when closing the panel
            RestoreOriginalFocus();
        }
    }

    // Method to open the options panel
    public void OpenOptionsPanel()
    {
        visible = true;
        Panel.SetActive(true);
        // Focus on the resolution dropdown or any element intended as first
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(DResolution.gameObject);
    }

    public void RestoreOriginalFocus()
    {
        // Ensure that there is a reference to the original first selected
        if (originalFirstSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(originalFirstSelected);
        }
        else
        {
            Debug.LogWarning("Original first selected GameObject is not set. Please check your EventSystem settings.");
        }
    }

    public void SetResolution()
    {
        // Logic to change the screen resolution based on dropdown
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(640, 360, true);
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                break;
            case 2:
                Screen.SetResolution(1440, 900, true);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 4:
                Screen.SetResolution(2560, 1080, true);
                break;
            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }
}


