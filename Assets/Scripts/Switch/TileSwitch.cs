using System.Collections;
using UnityEngine;
using Cinemachine;

public class TileSwitch : MonoBehaviour
{
    private SpriteRenderer RE1;
    private BoxCollider2D BO1;
    public bool canSwitch = true;
    [SerializeField] float SwitchingCooldown = 1;
    [SerializeField] float tm;
    private IEnumerator coroutine;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private CinemachineImpulseSource impulseSource;
    [SerializeField] private ScreenShake profile;

    //[SerializeField] ParticleSystem ShockWave;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        RE1 = GetComponent<SpriteRenderer>();
        BO1 = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        Mecha();
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
        if (Input.GetKeyDown(KeyCode.W) && canSwitch)
        {
            //ShockWave.Play();
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);
            StartCoroutine(Switch());
            RE1.enabled = !RE1.enabled;
            BO1.enabled = !BO1.enabled;
        }
    }
}