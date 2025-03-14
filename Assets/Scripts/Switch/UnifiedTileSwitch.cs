using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class UnifiedTileSwitch : MonoBehaviour
{
    [SerializeField] private float SwitchingCooldown = 1;
    private CinemachineImpulseSource impulseSource;
    [SerializeField] private ScreenShake profile;
    [SerializeField] private ParticleSystem ShockWave;
    public PlayerInput playerInput;
    public bool SwitchON = false;

    [Header("Son")]
    public AudioManager audioManager;

    // S�lection de mode
    public enum Mode { Dimension1, Dimension2 }
    public Mode currentMode = Mode.Dimension1;

    private float lastSwitchTime = 0;  // Timestamp de la derni�re commutation

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        // Initialisation des visibilit�s de chaque dimension au d�marrage
        InitializeDimensions();
    }

    void InitializeDimensions()
    {
        SetLayerVisibility("Dimension1", true);  // Active les objets de Dimension1
        SetLayerVisibility("Dimension2", false); // D�sactive les objets de Dimension2
    }

    public void Switch(InputAction.CallbackContext context)
    {
        if (SwitchON && Time.time >= lastSwitchTime + SwitchingCooldown)
        {
            if (context.performed)
            {
                StartVibration(0.05f, 0.3f);
                StartCoroutine(ToggleDimensions());
            }
        }
    }

    IEnumerator ToggleDimensions()
    {
        lastSwitchTime = Time.time;  // Mettre � jour le temps de la derni�re commutation

        ToggleMode();

        ShockWave.Play();
        audioManager.PlaySFX(audioManager.portal);
        CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);

        if (currentMode == Mode.Dimension1)
        {
            SetLayerVisibility("Dimension1", true);
            SetLayerVisibility("Dimension2", false);
        }
        else
        {
            SetLayerVisibility("Dimension1", false);
            SetLayerVisibility("Dimension2", true);
        }

        yield return new WaitForSeconds(SwitchingCooldown);
    }

    void ToggleMode()
    {
        currentMode = currentMode == Mode.Dimension1 ? Mode.Dimension2 : Mode.Dimension1;
    }

    public void SetLayerVisibility(string layerName, bool isVisible)
    {
        int layer = LayerMask.NameToLayer(layerName);
        GameObject[] objects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objects)
        {
            if (obj.layer == layer)
            {
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                if (renderer != null) renderer.enabled = isVisible;

                // D�sactiver ou activer tous les colliders sur l'objet
                Collider2D[] colliders = obj.GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = isVisible;
                }
            }
        }
    }

    private void StartVibration(float intensity, float duration)
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(intensity, intensity);
            StartCoroutine(StopVibration(gamepad, duration));
        }
    }

    private IEnumerator StopVibration(Gamepad gamepad, float delay)
    {
        yield return new WaitForSeconds(delay);
        gamepad.ResetHaptics();
    }

    public void SetActive(bool isActive)
    {
        this.enabled = isActive; // Active ou d�sactive ce script
        gameObject.SetActive(isActive); // Active ou d�sactive le GameObject attach�
    }
}
