using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Instance statique accessible globalement
    public static AudioManager Instance { get; private set; }

    [Header("------- Audio Source -------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip Domage;
    public AudioClip checkpoint;
    public AudioClip portal;
    public AudioClip timersound;
    public AudioClip shockwave;
    public AudioClip dash;
    public AudioClip jump;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Assurez-vous que l'AudioManager persiste entre les changements de scène
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Évite les doublons d'instance
        }
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
