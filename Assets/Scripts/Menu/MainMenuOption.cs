using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuOption : MonoBehaviour
{
    public GameObject Panel;
    bool visible = false;

    public TMP_Dropdown DResolution;
    public bool test = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && test)
        {
            visible = !visible;
            Panel.SetActive(visible);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            visible = !visible;
            Panel.SetActive(false);
        }
    }

    public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(640, 360, true);
                break;

            case 1:
                Screen.SetResolution(1280, 720, true);
                break;

            case 2:
                Screen.SetResolution(1440 , 900, true);
                break;

            case 3:
                Screen.SetResolution(1920, 1080, true);
                break;

            case 4:
                Screen.SetResolution(2560, 1080, true);
                break;


            default:
            Screen.SetResolution(1920,1080, true);


            break;
        }
    }
}
