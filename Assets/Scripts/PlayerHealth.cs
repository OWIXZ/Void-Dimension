using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealtBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Game_Over");
        }
    }

    void TakeDomage(int domage)
    {
        currentHealth -= domage;
        healthBar.SetHealth(currentHealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            TakeDomage(10);
        }
    }
}

