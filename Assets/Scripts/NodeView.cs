using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _leftText;
    [SerializeField] private TMP_Text _rightText;
    [SerializeField] private TMP_Text _downText;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _downButton;

    public void Initialize(Node model)
    {
        _icon.sprite = model.Background;
        _background.color = model.BackgroundColor;
        _text.text = model.Text;
        _leftText.text = model.LeftText;
        _rightText.text = model.RightText;
        _downText.text = model.DownText;
        _leftButton.gameObject.SetActive(model.LeftNode != null);
        _rightButton.gameObject.SetActive(model.RightNode != null);
        _downButton.gameObject.SetActive(model.DownNode != null);
    }
}