using UnityEngine;

public class PlatformChild : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si l'objet entrant doit devenir enfant de la plateforme
        if (other.gameObject.tag == "Player") // Assurez-vous que le GameObject � attacher a le tag "Player"
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // V�rifie si l'objet sortant doit �tre d�tach� de la plateforme
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
