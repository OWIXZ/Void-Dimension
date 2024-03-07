using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int heatlh)
    {
        slider.maxValue = heatlh;
        slider.value = heatlh;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int heatlh)
    {
        slider.value = heatlh;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

