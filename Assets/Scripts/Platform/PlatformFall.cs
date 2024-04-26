using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public float delay = 0.5f; // Délai avant que la plateforme commence à tomber

    private Rigidbody2D rb;

    void Awake()
    {
        // Récupérer le Rigidbody2D attaché à la plateforme
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet qui entre en collision est le joueur
        if (collision.collider.CompareTag("Player"))
        {
            // Appeler la fonction de chute avec un délai
            Invoke("StartFalling", delay);
        }
    }

    void StartFalling()
    {
        // Changer le mode du Rigidbody2D pour que la plateforme commence à tomber
        rb.isKinematic = false;
    }
}

