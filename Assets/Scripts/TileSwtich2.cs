using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class tileSwtich2 : MonoBehaviour
{


    private SpriteRenderer tileMap2;
    private BoxCollider2D tileCo2;
    // Start is called before the first frame update
    void Start()
    {
        tileMap2 = GetComponent<SpriteRenderer>();
        tileCo2 = GetComponent<BoxCollider2D>();
        tileMap2.enabled = false;
        tileCo2.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Mecha2();
    }

    public void Mecha2()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            tileMap2.enabled = !tileMap2.enabled;
            tileCo2.enabled = !tileCo2.enabled;
        }
    }
}
