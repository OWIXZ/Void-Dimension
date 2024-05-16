using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockSwitchTrigger : MonoBehaviour
{
    public UnifiedTileSwitch switchController;
    public PlayerInput playerInput;  // Ajouter une référence publique à PlayerInput

    void Start()
    {
        // Désactiver la possibilité de switch au début, désactiver Dimension2 et les entrées du joueur
        if (switchController != null)
        {
            switchController.enabled = false;
            switchController.SetLayerVisibility("Dimension2", false);  // Désactiver Dimension2 dès le début
        }
        if (playerInput != null)
        {
            playerInput.enabled = false;  // Désactiver Player Input au début
            Debug.Log("Player input disabled at start.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            // Activer le script UnifiedTileSwitch
            if (switchController != null)
            {
                switchController.enabled = true;
                Debug.Log("Switching enabled!");
            }
            // Réactiver les entrées du joueur
            if (playerInput != null)
            {
                playerInput.enabled = true;  // Réactiver Player Input
                Debug.Log("Player input re-enabled upon collision.");
            }

            // Détruire l'objet après activation
            //Destroy(gameObject);
        }
    }
}



