using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCam : MonoBehaviour
{
    [SerializeField] private float newActualSize = 16;
    [SerializeField] private float newTargetSize = 18;
    [SerializeField] SpriteManager spriteManager;

    private int transitionDuration;

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spriteManager.baseActualFOV = spriteManager.actualFOV;
            spriteManager.baseTargetFOV = spriteManager.targetFOV;
            spriteManager.newActualFOV = newActualSize;
            spriteManager.newTargetFOV = newTargetSize;
            spriteManager.updateTime = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            spriteManager.newActualFOV = spriteManager.baseActualFOV;
            spriteManager.newTargetFOV = spriteManager.baseTargetFOV;
            spriteManager.newActualFOV = newActualSize;
            spriteManager.newTargetFOV = newTargetSize;
            spriteManager.updateTime = 0;
        }
    }
}
