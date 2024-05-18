using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SpriteManager : MonoBehaviour
{
    private string layerName = "Dimension3";
    public PlayerInput playerInput;
    [SerializeField] ParticleSystem Instinct;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera; // R�f�rence � la cam�ra virtuelle Cinemachine

    private float normalSize = 15f; // Taille orthographique normale
    private float zoomedSize = 13f; // Taille orthographique pour le zoom
    [SerializeField] private float transitionDuration = 0.5f; // Dur�e de la transition en secondes

    private bool isZooming = false; // Contr�le de l'�tat de zoom
    private float targetSize; // Taille cible pour l'interpolation
    private float timeSinceZoomStart; // Suivi du temps depuis le d�but du zoom
    public bool CanInstinct = false;

    void Start()
    {
        ToggleSpriteRenderers(false);
        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine camera not assigned!");
        }
        else
        {
            cinemachineCamera.m_Lens.OrthographicSize = normalSize; // D�finissez la taille orthographique initiale
        }
    }

    void Update()
    {
        if (isZooming)
        {
            if (cinemachineCamera != null)
            {
                // Calcule le temps �coul� divis� par la dur�e totale pour obtenir un ratio
                float ratio = timeSinceZoomStart / transitionDuration;
                // Interpole entre la taille actuelle et la taille cible
                cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.m_Lens.OrthographicSize, targetSize, ratio);
                timeSinceZoomStart += Time.deltaTime;

                // Arr�te le zooming lorsque le temps n�cessaire est atteint
                if (ratio >= 1.0f)
                {
                    isZooming = false;
                }
            }
        }
    }

    public void Dimension3(InputAction.CallbackContext context)
    {
        if (CanInstinct == true)
        {
            if (context.performed)
            {
                ToggleSpriteRenderers(true);
                Instinct.Play();
                if (cinemachineCamera != null)
                {
                    targetSize = zoomedSize;
                    isZooming = true;
                    timeSinceZoomStart = 0;
                }
                Time.timeScale = 0.5f; // R�duire le timescale quand l'instinct est activ�
            }
            else if (context.canceled)
            {
                ToggleSpriteRenderers(false);
                Instinct.Stop();
                if (cinemachineCamera != null)
                {
                    targetSize = normalSize;
                    isZooming = true;
                    timeSinceZoomStart = 0;
                }
                Time.timeScale = 1f; // Revenir au timescale normal quand l'instinct est d�sactiv�
            }
        }
    }

    public void ToggleSpriteRenderers(bool enable)
    {
        int layer = LayerMask.NameToLayer(layerName);
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.gameObject.layer == layer)
            {
                renderer.enabled = enable;
            }
        }
    }
}
