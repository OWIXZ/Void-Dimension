using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public float delay = 0.5f; // D�lai avant que la plateforme commence � tomber

    private Rigidbody2D rb;

    void Awake()
    {
        // R�cup�rer le Rigidbody2D attach� � la plateforme
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // V�rifier si l'objet qui entre en collision est le joueur
        if (collision.collider.CompareTag("Player"))
        {
            // Appeler la fonction de chute avec un d�lai
            Invoke("StartFalling", delay);
        }
    }

    void StartFalling()
    {
        // Changer le mode du Rigidbody2D pour que la plateforme commence � tomber
        rb.isKinematic = false;
    }
}

