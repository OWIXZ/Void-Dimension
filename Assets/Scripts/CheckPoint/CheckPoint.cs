using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Movement movement;
    public bool CheckPointON = true;

    public void checkPointPass()
    {
        movement.audioManager.PlaySFX(movement.audioManager.checkpoint);
        CheckPointON = false;
    }
}

