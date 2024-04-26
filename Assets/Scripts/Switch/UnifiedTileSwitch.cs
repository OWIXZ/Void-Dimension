using System.Collections;
using UnityEngine;
using Cinemachine;

public class UnifiedTileSwitch : MonoBehaviour
{
    private SpriteRenderer rendererUni;
    private BoxCollider2D colliderUni;
    public bool canSwitch = true;
    [SerializeField] float SwitchingCooldown = 1;
    [SerializeField] float tm;
    private IEnumerator coroutine;
    [SerializeField] private Rigidbody2D rb;
    private CinemachineImpulseSource impulseSource;
    [SerializeField] private ScreenShake profile;
    //[SerializeField] ParticleSystem ShockWave;

    // Mode selection
    public enum Mode { Mode1, Mode2 }
    public Mode currentMode = Mode.Mode1;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        rendererUni = GetComponent<SpriteRenderer>();
        colliderUni = GetComponent<BoxCollider2D>();

        // Initialize based on mode
        if (currentMode == Mode.Mode2)
        {
            rendererUni.enabled = false;
            colliderUni.enabled = false;
        }
    }

    void Update()
    {
        switch (currentMode)
        {
            case Mode.Mode1:
                Mecha();
                break;
            case Mode.Mode2:
                Mecha2();
                break;
        }
    }

    IEnumerator Switch()
    {
        canSwitch = false;
        tm = Time.time;
        yield return new WaitForSeconds(SwitchingCooldown);
        canSwitch = true;
    }

    public void Mecha()
    {
        if (Input.GetKeyDown(KeyCode.B) && canSwitch)
        {
            //ShockWave.Play();
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);
            StartCoroutine(Switch());
            rendererUni.enabled = !rendererUni.enabled;
            colliderUni.enabled = !colliderUni.enabled;
        }
    }

    public void Mecha2()
    {
        if (Input.GetKeyDown(KeyCode.B) && canSwitch)
        {
            //ShockWave.Play();
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);
            StartCoroutine(Switch());
            rendererUni.enabled = !rendererUni.enabled;
            colliderUni.enabled = !colliderUni.enabled;
        }
    }

    // Optionally, add a method to switch modes at runtime
    public void ToggleMode()
    {
        currentMode = currentMode == Mode.Mode1 ? Mode.Mode2 : Mode.Mode1;

        // Reset component states when toggling mode
        rendererUni.enabled = false;
        colliderUni.enabled = false;
    }
}
