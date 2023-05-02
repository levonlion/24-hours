using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    public int Value { get; private set; }
    public int MaxValue { get; private set; }

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    public void Initialize(int startValue, int maxValue, string name)
    {
        Value = startValue;
        MaxValue = maxValue;
        _text.text = name;
        
        _slider.value = (float)Value / MaxValue;
    }
    
    public void Add(int value)
    {
        Value += value;
        _slider.value = (float)Value / MaxValue;
    }
}
