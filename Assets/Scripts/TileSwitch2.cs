using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileSwitch2 : MonoBehaviour
{
    private SpriteRenderer RE2;
    private BoxCollider2D BO2;
    public bool canSwitch = true;
    public bool AbilitiesSwitch = false;
    [SerializeField] float SwitchingCooldown = 0.5f;
    [SerializeField] float tm;
    private IEnumerator coroutine;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        RE2 = GetComponent<SpriteRenderer>();
        BO2 = GetComponent<BoxCollider2D>();
        RE2.enabled = false;
        BO2.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Mecha2();
    }

    IEnumerator Switch()
    {
        canSwitch = false;
        tm = Time.time;
        yield return new WaitForSeconds(SwitchingCooldown);
        canSwitch = true;
    }

    public void Mecha2()
    {
        if (Input.GetKeyDown(KeyCode.W) && canSwitch && AbilitiesSwitch)
        {
            StartCoroutine(Switch());
            RE2.enabled = !RE2.enabled;
            BO2.enabled = !BO2.enabled;
        }
    }
}
