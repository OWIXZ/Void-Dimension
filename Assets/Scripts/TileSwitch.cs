using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileSwtich : MonoBehaviour
{
    private SpriteRenderer tileMap;
    private BoxCollider2D tileCo;
    [SerializeField] bool canSwitch = true;
    [SerializeField] float SwitchingCooldown = 0.5f;
    [SerializeField] float tm;
    private IEnumerator coroutine;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = GetComponent <SpriteRenderer>();
        tileCo = GetComponent<BoxCollider2D>();
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

        if (Input.GetKeyDown(KeyCode.W) && canSwitch == true)
        {
            StartCoroutine(Switch());
            tileMap.enabled = !tileMap.enabled;
            tileCo.enabled = !tileCo.enabled;
        }
    }
}