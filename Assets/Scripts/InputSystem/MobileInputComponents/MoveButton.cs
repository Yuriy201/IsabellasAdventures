using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public float Direction => _direction;

    [SerializeField] private Side _side;
    private float _direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_side)
        {
            case Side.left:
                _direction = -1;
                break;
            case Side.right:
                _direction = 1;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData) => _direction = 0;

    public enum Side
    {
        left,
        right
    }
}
