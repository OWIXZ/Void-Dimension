using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SpriteManager : MonoBehaviour
{
    private string layerName = "Dimension3";
    public PlayerInput playerInput;
    [SerializeField] ParticleSystem Instinct;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera; // Référence à la caméra virtuelle Cinemachine

    [SerializeField] private float transitionDuration = 0.5f; // Durée de la transition 

    private bool isZooming = false;
    private float timeSinceZoomStart = 0;
    public bool CanInstinct = false;
    public bool IsActive { get; private set; } = true;

    public float actualFOV = 60f;
    public float targetFOV = 40f;
    public float baseActualFOV = 60f;
    public float baseTargetFOV = 40f;
    public float newActualFOV = 60f;
    public float newTargetFOV = 40f;
    public float updateTime = 0;

    void Start()
    {
        ToggleSpriteRenderers(false);
        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine camera not assigned!");
        }
        else
        {
            cinemachineCamera.m_Lens.Orthographic = false; // Assurez-vous que la caméra n'est pas en mode orthographique
            cinemachineCamera.m_Lens.FieldOfView = actualFOV;
        }
    }

    void Update()
    {
        if (cinemachineCamera != null)
        {
            updateTime += Time.deltaTime;
            updateTime = Mathf.Clamp(updateTime, 0, 1);
            actualFOV = Mathf.Lerp(baseActualFOV, newActualFOV, updateTime);
            targetFOV = Mathf.Lerp(baseTargetFOV, newTargetFOV, updateTime);
            if (isZooming)
            {
                timeSinceZoomStart += Time.deltaTime * (1 / transitionDuration);
            }
            else
            {
                timeSinceZoomStart -= Time.deltaTime * (1 / transitionDuration);
            }
            timeSinceZoomStart = Mathf.Clamp(timeSinceZoomStart, 0, 1);
            cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(actualFOV, targetFOV, timeSinceZoomStart);
        }
    }

    public void Dimension3(InputAction.CallbackContext context)
    {
        if (CanInstinct == true && IsActive)
        {
            if (context.performed)
            {
                ToggleSpriteRenderers(true);
                Instinct.Play();
                if (cinemachineCamera != null)
                {
                    isZooming = true;
                }
                Time.timeScale = 0.5f;
            }
            else if (context.canceled)
            {
                ToggleSpriteRenderers(false);
                Instinct.Stop();
                if (cinemachineCamera != null)
                {
                    isZooming = false;
                }
                Time.timeScale = 1f;
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

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
        if (!isActive)
        {
            ToggleSpriteRenderers(false);
            Instinct.Stop();
            if (cinemachineCamera != null)
            {
                isZooming = true;
                timeSinceZoomStart = 0;
            }
            Time.timeScale = 1f;
        }
    }
}
