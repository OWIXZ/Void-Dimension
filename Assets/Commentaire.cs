using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commentaire : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Panel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Panel.SetActive(false);
        }
    }

}
