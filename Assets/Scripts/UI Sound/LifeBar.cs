using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private SpriteRenderer characterSprite; // R�f�rence au SpriteRenderer du personnage
    [SerializeField] private float healthQuantity = 100f;

    [Header("Damage Effect")]
    [SerializeField] private Color damageColor = Color.red; // Couleur quand le personnage prend des d�g�ts
    [SerializeField] private float colorChangeDuration = 0.5f; // Dur�e de l'effet de couleur

    private Color originalColor; // Pour stocker la couleur originale du sprite

    [Header("Sound")]
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        if (characterSprite != null)
        {
            originalColor = characterSprite.color; // Sauvegarde de la couleur originale
        }
    }

    void Update()
    {
        if (healthQuantity <= 0)
        {
            SceneManager.LoadScene("Game_Over");
        }
    }

    public void Damage(float damage)
    {
        healthQuantity -= damage;
        healthBar.fillAmount = healthQuantity / 100f;

        if (characterSprite != null)
        {
            StopCoroutine("HandleDamageEffect");
            StartCoroutine("HandleDamageEffect");
        }
    }

    private IEnumerator HandleDamageEffect()
    {
        characterSprite.color = damageColor; // Change la couleur en rouge
        yield return new WaitForSeconds(colorChangeDuration); // Attendez la dur�e d�finie
        characterSprite.color = originalColor; // Revenir � la couleur originale
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            audioManager.PlaySFX(audioManager.Domage);
            Damage(5);
        }
    }
}
