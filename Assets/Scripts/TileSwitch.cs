using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileSwtich : MonoBehaviour
{
    private SpriteRenderer tileMap;
    private BoxCollider2D tileCo;

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

    public void Mecha()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tileMap.enabled = !tileMap.enabled;
            tileCo.enabled = !tileCo.enabled;
        }
    }
}