using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    public event Action OnValueChanged;
    public int Value { get; private set; }
    public int MaxValue { get; private set; }
    
    private bool _isTime;
    
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _endscreenText;

    public void Initialize(int startValue, int maxValue, string name, bool isTime)
    {
        Value = startValue;
        MaxValue = maxValue;
        _text.text = name;
        _isTime = isTime;

        _slider.value = (float)Value / MaxValue;
        
        if(isTime)
            UpdateTime();
    }
    
    public void Add(int value)
    {
        Value += value;
        Value = Mathf.Min(Value, MaxValue);
        _slider.value = (float)Value / MaxValue;
        
        if (_isTime)
            UpdateTime();
        else
            _endscreenText.text = (Mathf.Max(Value, 0)).ToString();

        OnValueChanged?.Invoke();

        if (Value <= 0)
            GameManager.Instance.Lose();
    }
    
    private void UpdateTime()
    {
        var span = TimeSpan.FromMinutes(Value).ToString(@"hh\:mm");;
        _text.text = (Value < 0 ? "-" : " ") + span;
    }
}
