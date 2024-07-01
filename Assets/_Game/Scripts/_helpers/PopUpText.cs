using TMPro;
using UnityEngine;
using DG.Tweening;

public class PopUpText : MonoBehaviour
{
    private TextMeshPro _textComponent;
    private float _scaleMultiplier;
    public void SetText(string text)
    {
        _textComponent.text = text;
    }

    private void OnEnable()
    {
        _textComponent = GetComponent<TextMeshPro>();
        _scaleMultiplier = transform.localScale.x;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * _scaleMultiplier, 0.5f);
    }
}