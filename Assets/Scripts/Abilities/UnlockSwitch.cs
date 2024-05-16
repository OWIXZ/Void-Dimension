using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockSwitchTrigger : MonoBehaviour
{
    public UnifiedTileSwitch switchController;
    public PlayerInput playerInput;  // Ajouter une r�f�rence publique � PlayerInput

    void Start()
    {
        // D�sactiver la possibilit� de switch au d�but, d�sactiver Dimension2 et les entr�es du joueur
        if (switchController != null)
        {
            switchController.enabled = false;
            switchController.SetLayerVisibility("Dimension2", false);  // D�sactiver Dimension2 d�s le d�but
        }
        if (playerInput != null)
        {
            playerInput.enabled = false;  // D�sactiver Player Input au d�but
            Debug.Log("Player input disabled at start.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifier si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            // Activer le script UnifiedTileSwitch
            if (switchController != null)
            {
                switchController.enabled = true;
                Debug.Log("Switching enabled!");
            }
            // R�activer les entr�es du joueur
            if (playerInput != null)
            {
                playerInput.enabled = true;  // R�activer Player Input
                Debug.Log("Player input re-enabled upon collision.");
            }

            // D�truire l'objet apr�s activation
            //Destroy(gameObject);
        }
    }
}



