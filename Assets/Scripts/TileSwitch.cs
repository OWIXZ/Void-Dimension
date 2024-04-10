using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileSwitch : MonoBehaviour
{
    private SpriteRenderer RE1;
    private BoxCollider2D BO1;
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
        RE1 = GetComponent <SpriteRenderer>();
        BO1 = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
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

        if (Input.GetKeyDown(KeyCode.W) && canSwitch && AbilitiesSwitch)
        {
            StartCoroutine(Switch());
            RE1.enabled = !RE1.enabled;
            BO1.enabled = !BO1.enabled;
        }
    }
}