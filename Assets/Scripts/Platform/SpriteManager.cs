using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteManager : MonoBehaviour
{
    // Nom du Layer à contrôler
    private string layerName = "Dimension3";
    public PlayerInput playerInput;

    void Start()
    {
        // Désactive tous les SpriteRenderer des GameObjects sur le layer "Dimension3" au début du jeu.
        ToggleSpriteRenderers(false);
    }



    // Méthode pour réagir aux changements d'état de l'action de Input System
    public void Dimension3(InputAction.CallbackContext context)
    {
        if (context.performed) // L'action est déclenchée, correspondant à un appui sur la touche
        {
            ToggleSpriteRenderers(true); // Active les SpriteRenderer
        }
        else if (context.canceled) // L'action est terminée, correspondant au relâchement de la touche
        {
            ToggleSpriteRenderers(false); // Désactive les SpriteRenderer
        }
    }

    // Méthode pour activer ou désactiver les SpriteRenderer
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
