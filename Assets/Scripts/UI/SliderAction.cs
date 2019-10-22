using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Cloudy Around UI/Slider Action")]
[RequireComponent(typeof(RectTransform))]
public class SliderAction : MonoBehaviour
{
    public float minValue;
    public Slider slider;

    public void OnValueChange()
    {
        if (GetComponent<RectTransform>().sizeDelta.y > minValue)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, slider.value * (GetComponent<RectTransform>().sizeDelta.y - minValue));
    }

    public void RefreshSlider()
    {
        if (GetComponent<RectTransform>().sizeDelta.y <= minValue)
            slider.gameObject.SetActive(false);
    }
}
