using UnityEngine;
using TMPro;

public class Titles : MonoBehaviour 
{
    //сами титры
    public TMP_Text _mainText;
    //скорость скольжения
    public float _speedSlip = 50f;
    private RectTransform _textRectTransform;
    private float _textHeight;
    private float _parentHeight;
    private bool _isSlip = false;

    private void Start()
    {
        _textRectTransform = _mainText.rectTransform;
        _textHeight = _textRectTransform.rect.height;
        _parentHeight = _textRectTransform.parent.GetComponent<RectTransform>().rect.height;

        StartCoroutine(SlipText());
    }
    //функция отвечающая за скольжение титров вверх
    private System.Collections.IEnumerator SlipText()
    {
        _isSlip = true;
        while (_isSlip)
        {
            float currentYPosition = _textRectTransform.anchoredPosition.y;
            float targetYPosition = _parentHeight + _textHeight;

            while (currentYPosition < targetYPosition)
            {
                currentYPosition += _speedSlip* Time.deltaTime;
                _textRectTransform.anchoredPosition = new Vector2(_textRectTransform.anchoredPosition.x, currentYPosition);
                yield return null;
            }

            _textRectTransform.anchoredPosition = new Vector2(_textRectTransform.anchoredPosition.x, -_textHeight);

            yield return new WaitForSeconds(1f);
        }
    }
}
