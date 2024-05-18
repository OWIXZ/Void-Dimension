
using UnityEngine;

public class UnlockInstinct : MonoBehaviour
{
    public SpriteManager Instinct;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instinct.CanInstinct = true;
            //Destroy(gameObject);
        }
    }
}