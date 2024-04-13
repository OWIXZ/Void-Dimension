using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    public Movement Dash;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Dash.AbilitiesDash = true;
            Destroy(gameObject);
        }
    }
}