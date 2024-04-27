using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    void Update()
    {
        // Vérifie si un des joysticks de la manette est utilisé
        if (Gamepad.current != null && (Gamepad.current.leftStick.ReadValue() != Vector2.zero || Gamepad.current.rightStick.ReadValue() != Vector2.zero))
        {
            Cursor.visible = false;
        }
        else
        {
            // Vous pouvez choisir de remettre le curseur visible si la souris est bougée
            if (Mouse.current.delta.ReadValue() != Vector2.zero)
            {
                Cursor.visible = true;
            }
        }
    }
}
