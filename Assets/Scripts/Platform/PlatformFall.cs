using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public float delay = 0.5f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("StartFalling", delay);
        }
    }

    void StartFalling()
    {
        rb.isKinematic = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            DeactivatePlatform(); // Appel � la fonction de d�sactivation modifi�e
        }
    }

    void DeactivatePlatform()
    {
        spriteRenderer.enabled = false; // Rendre le sprite invisible
        rb.isKinematic = true; // Rendre la plateforme non physique
        this.enabled = false; // D�sactive ce script mais pas le GameObject entier
    }

    public void ResetPosition()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        spriteRenderer.enabled = true;
        this.enabled = true; // R�activer ce script
    }
}
