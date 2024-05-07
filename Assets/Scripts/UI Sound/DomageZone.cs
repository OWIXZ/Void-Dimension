using System.Collections;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private Coroutine damageCoroutine;
    [Header("Sound")]
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damageCoroutine == null) // Vérifier que c'est bien le joueur qui entre
        {
            LifeBar lifeBar = collision.GetComponent<LifeBar>();
            if (lifeBar != null)
            {
                damageCoroutine = StartCoroutine(ApplyDamage(lifeBar, 1)); // Commence à appliquer des dommages
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damageCoroutine != null) // Vérifier que c'est bien le joueur qui sort
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null; // Arrête les dommages continus
        }
    }

    private IEnumerator ApplyDamage(LifeBar lifeBar, float damage)
    {
        while (true)
        {
            audioManager.PlaySFX(audioManager.Domage);
            lifeBar.Damage(damage); // Appel la méthode Damage du LifeBar
            yield return new WaitForSeconds(2); // Attendre une seconde entre chaque dommage
        }
    }
}
