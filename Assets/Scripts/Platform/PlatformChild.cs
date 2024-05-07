using UnityEngine;

public class PlatformChild : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet entrant doit devenir enfant de la plateforme
        if (other.gameObject.tag == "Player") // Assurez-vous que le GameObject à attacher a le tag "Player"
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Vérifie si l'objet sortant doit être détaché de la plateforme
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
