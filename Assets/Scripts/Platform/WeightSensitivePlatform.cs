using UnityEngine;

public class WeightSensitivePlatform : MonoBehaviour
{
    public float sinkAmount = 0.3f;  // La distance que la plateforme descend lorsque le joueur est dessus.
    public float sinkSpeed = 0.5f;  // Vitesse à laquelle la plateforme descend par seconde.

    private Vector3 originalPosition;
    public float VerticalOffset { get; private set; } = 0f;  // Offset vertical actuel
    private bool playerOnPlatform = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float targetOffset = playerOnPlatform ? -sinkAmount : 0;

        // Mise à jour de VerticalOffset à une vitesse constante vers la cible
        if (VerticalOffset != targetOffset)
        {
            float change = sinkSpeed * Time.deltaTime;
            if (VerticalOffset > targetOffset)
            {
                VerticalOffset = Mathf.Max(VerticalOffset - change, targetOffset);
            }
            else
            {
                VerticalOffset = Mathf.Min(VerticalOffset + change, targetOffset);
            }
        }

        // Mise à jour de la position verticale sans affecter les autres scripts
        transform.position = new Vector3(transform.position.x, originalPosition.y + VerticalOffset, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOnPlatform = false;
        }
    }
}
