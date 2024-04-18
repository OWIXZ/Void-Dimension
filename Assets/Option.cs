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
    public Slider slider;
    public TMP_Text TxtVolume;

    private void Start() => SliderChange();
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            visible = !visible;
            Panel.SetActive(visible);
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
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void SliderChange()
    {
        audiosource.volume = slider.value;
        TxtVolume.text = "Volume" + (audiosource.volume * 100).ToString("00") + "%";
    }
}
