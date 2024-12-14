using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.Progress;

public class RockPoison : MonoBehaviour
{
    [SerializeField] private QuickSlotManager _qsm;
    public ItemScriptableObject item;
    [SerializeField] private bool _onTrig;
    [SerializeField] private GameObject _liftButton;
    private bool _actionPerformed;

    [SerializeField] private Transform _rockPos;

    private void Start()
    {
        _liftButton.SetActive(false);
        _rockPos = GetComponent<Transform>();
        _actionPerformed = false; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _onTrig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _onTrig = false;
        }
    }

    private void Update()
    {
        toUsePoison();
    }

    private void toUsePoison()
    {
        if (_onTrig && _qsm.isPotionInQuickSlot && !_actionPerformed)
        {
            _liftButton.SetActive(true);
        }
    }

    public void liftAStone()
    {
        Debug.Log("Yes");
        StartCoroutine(SmoothLift());
        _qsm.losePoison(item);
        _actionPerformed = true;
        Destroy(_liftButton);
    }

    private IEnumerator SmoothLift()
    {
        float targetY = _rockPos.position.y + 3.5f; // Целевая позиция
        float duration = 1f; // Длительность анимации
        float elapsedTime = 0f;

        Vector3 startPosition = _rockPos.position; // Начальная позиция
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        while (elapsedTime < duration)
        {
            _rockPos.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Ждём следующий кадр
        }

        // Убедимся, что объект точно в целевой позиции
        _rockPos.position = targetPosition;
    }

}
