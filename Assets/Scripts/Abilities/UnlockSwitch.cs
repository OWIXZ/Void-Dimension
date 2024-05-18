
using UnityEngine;

public class UnlockSwitch : MonoBehaviour
{
    public UnifiedTileSwitch SwitchON;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SwitchON.SwitchON = true;
            //Destroy(gameObject);
        }
    }
}