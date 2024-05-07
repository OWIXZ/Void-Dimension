using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip checkpointSound; // Son du checkpoint
    public Animator checkpointAnimator; // Animator attach� au checkpoint
    public string animationTriggerName = "Activate"; // Trigger de l'animation

    [SerializeField] bool hasBeenActivated = false; // Indicateur si le checkpoint a �t� activ�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenActivated && other.CompareTag("Player"))
        {
            // Jouer le son si non encore jou�
            if (checkpointSound != null)
            {
                AudioManager.Instance.PlaySFX(checkpointSound);
            }

            // D�clencher l'animation si non encore d�clench�e
            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetTrigger(animationTriggerName);
            }

            hasBeenActivated = true; // Marque ce checkpoint comme activ�
        }
    }
}
