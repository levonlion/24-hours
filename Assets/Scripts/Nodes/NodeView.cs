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

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    public void Initialize(Node model)
    {
        _icon.sprite = model.Background;
        model.BackgroundColor.a = 1;
        _background.color = model.BackgroundColor;
        model.TextColor.a = 1;
        _text.color = model.TextColor;
        _text.text = model.Text;
        _leftText.text = model.LeftText;
        _rightText.text = model.RightText;
        _leftButton.gameObject.SetActive(model.LeftNode != null);
        _rightButton.gameObject.SetActive(model.RightNode != null);
    }
}