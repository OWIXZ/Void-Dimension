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
        if (collision.CompareTag("Player") && damageCoroutine == null) // V�rifier que c'est bien le joueur qui entre
        {
            LifeBar lifeBar = collision.GetComponent<LifeBar>();
            if (lifeBar != null)
            {
                damageCoroutine = StartCoroutine(ApplyDamage(lifeBar, 1)); // Commence � appliquer des dommages
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damageCoroutine != null) // V�rifier que c'est bien le joueur qui sort
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null; // Arr�te les dommages continus
        }
    }

    private IEnumerator ApplyDamage(LifeBar lifeBar, float damage)
    {
        while (true)
        {
            audioManager.PlaySFX(audioManager.Domage);
            lifeBar.Damage(damage); // Appel la m�thode Damage du LifeBar
            yield return new WaitForSeconds(2); // Attendre une seconde entre chaque dommage
        }
    }
}
