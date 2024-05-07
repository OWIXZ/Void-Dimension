using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Annuler la vélocité verticale existante
            rb.velocity = new Vector2(rb.velocity.x, 0);

            // Ajouter une force verticale au Rigidbody2D
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
