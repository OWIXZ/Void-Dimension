using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteManager : MonoBehaviour
{
    // Nom du Layer � contr�ler
    private string layerName = "Dimension3";
    public PlayerInput playerInput;

    void Start()
    {
        // D�sactive tous les SpriteRenderer des GameObjects sur le layer "Dimension3" au d�but du jeu.
        ToggleSpriteRenderers(false);
    }



    // M�thode pour r�agir aux changements d'�tat de l'action de Input System
    public void Dimension3(InputAction.CallbackContext context)
    {
        if (context.performed) // L'action est d�clench�e, correspondant � un appui sur la touche
        {
            ToggleSpriteRenderers(true); // Active les SpriteRenderer
        }
        else if (context.canceled) // L'action est termin�e, correspondant au rel�chement de la touche
        {
            ToggleSpriteRenderers(false); // D�sactive les SpriteRenderer
        }
    }

    // M�thode pour activer ou d�sactiver les SpriteRenderer
    public void ToggleSpriteRenderers(bool enable)
    {
        int layer = LayerMask.NameToLayer(layerName);
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.gameObject.layer == layer)
            {
                renderer.enabled = enable;
            }
        }
    }
}
