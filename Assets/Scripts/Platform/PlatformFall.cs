using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public float delay = 0.5f; // D�lai avant que la plateforme commence � tomber

    private Vector3 initialPosition; // Position initiale de la plateforme
    private Quaternion initialRotation; // Rotation initiale de la plateforme
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifier si le joueur touche la zone d'activation d�limit�e
        if (other.CompareTag("Player"))
        {
            Invoke("StartFalling", delay);
        }
    }

    void StartFalling()
    {
        rb.isKinematic = false;
    }

    public void ResetPosition()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
