using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selected : MonoBehaviour
{
    private Image _buttonImage;
    private Color _default;
    private Color _selected;

    private void Start() {
        _selected = Color.red;
        _default = Color.white;
        _buttonImage = gameObject.GetComponent<Image>();
    }

    private void Update() => Check();

    private void Check() => _buttonImage.color = EventSystem.current.currentSelectedGameObject == _buttonImage.gameObject ? _selected : _default;
}
