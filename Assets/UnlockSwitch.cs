using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSwitch : MonoBehaviour
{
    public TileSwitch Switch1;
    public TileSwitch2 Switch2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Switch1.AbilitiesSwitch = true;
            Switch1.AbilitiesSwitch = true;
            Destroy(gameObject);
        }
    }
}