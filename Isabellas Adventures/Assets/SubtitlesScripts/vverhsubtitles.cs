using UnityEngine;
using TMPro;

public class _Privet : MonoBehaviour
{
    public TMP_Text _glavnoeText;
    public float _skolzhenieSkorosti = 50f;
    private RectTransform _textRectTransform;
    private float _textHeight;
    private float _parentHeight;
    private bool _isSkolzhenie = false;

    private void Start()
    {
        _textRectTransform = _glavnoeText.rectTransform;
        _textHeight = _textRectTransform.rect.height;
        _parentHeight = _textRectTransform.parent.GetComponent<RectTransform>().rect.height;

        StartCoroutine(SkolzhenieTexta());
    }

    private System.Collections.IEnumerator SkolzhenieTexta()
    {
        _isSkolzhenie = true;
        while (_isSkolzhenie)
        {
            float currentYPosition = _textRectTransform.anchoredPosition.y;
            float targetYPosition = _parentHeight + _textHeight;

            while (currentYPosition < targetYPosition)
            {
                currentYPosition += _skolzhenieSkorosti * Time.deltaTime;
                _textRectTransform.anchoredPosition = new Vector2(_textRectTransform.anchoredPosition.x, currentYPosition);
                yield return null;
            }

            _textRectTransform.anchoredPosition = new Vector2(_textRectTransform.anchoredPosition.x, -_textHeight);

            yield return new WaitForSeconds(1f);
        }
    }
}
