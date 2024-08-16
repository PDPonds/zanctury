using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider _slider;
    public void SetMaxValue(float maxValue,float _Value)
    {
        _slider.maxValue = maxValue;
        _slider.value = _Value;
    }
    public void SetValue(float currentValue)
    {
        _slider.value = currentValue;
    }
}
