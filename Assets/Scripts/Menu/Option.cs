using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    public GameObject Panel;
    bool visible = false;

    public TMP_Dropdown DResolution;
    public AudioSource audiosource;
    public AudioSource SFX;
    public Slider SliderV;
    public Slider SliderSFX;
    public TMP_Text TxtVolume;
    public TMP_Text TxtSFX;
    public bool test = false;

    private void Start()
    {
        SliderChange();
        SliderChangeSFX();
    }
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
                Screen.SetResolution(2560, 1440, true);
                break;

            case 5:
                Screen.SetResolution(3840, 2160, true);
                break;

        }
    }

    public void SliderChange()
    {
        audiosource.volume = SliderV.value;
        TxtVolume.text = "Volume" + (audiosource.volume * 100).ToString("  00") + "%";
    }

    public void SliderChangeSFX()
    {
        SFX.volume = SliderSFX.value;
        TxtSFX.text = "SFX" + (SFX.volume * 100).ToString("     00") + "%";
    }
}
