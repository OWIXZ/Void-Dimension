using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip checkpointSound; // Son du checkpoint
    public Animator checkpointAnimator; // Animator attaché au checkpoint
    public string animationTriggerName = "Activate"; // Trigger de l'animation

    [SerializeField] bool hasBeenActivated = false; // Indicateur si le checkpoint a été activé

    public Vector3 CurrentCheckpoint { get; private set; }

    private void Start()
    {
        CurrentCheckpoint = transform.position;  // Initialise le checkpoint à la position initiale.
    }

    public void SetNewCheckpoint(Vector3 newCheckpointPosition)
    {
        CurrentCheckpoint = newCheckpointPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenActivated && other.CompareTag("Player"))
        {
            // Jouer le son si non encore joué
            if (checkpointSound != null)
            {
                AudioManager.Instance.PlaySFX(checkpointSound);
            }

            // Déclencher l'animation si non encore déclenchée
            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetTrigger(animationTriggerName);
            }

            hasBeenActivated = true; // Marque ce checkpoint comme activé
        }
    }
}
