using TMPro;
using UnityEngine;


namespace NeoxiderUi
{
    public class ButtonPrice : MonoBehaviour
    {
        public enum ButtonType
        {
            Choosen,
            Choose,
            Buy
        }

        [SerializeField] private int _price = 1000;
        [SerializeField] private bool _price_0 = false; //На покупку писать цена 0 
        [SerializeField] private TextMeshProUGUI _textPrice;
        [SerializeField] private ButtonType _type = ButtonType.Buy;
        [SerializeField] private GameObject[] _visualButton;

        public void SetVisual(int price, ButtonType bType = ButtonType.Buy)
        {
            if (!_price_0 && bType == ButtonType.Buy && price == 0)
                bType = ButtonType.Choose;

            if (price > 0 && ButtonType.Buy != bType)
            {
                Debug.LogWarning("Не правильный тип");
                bType = ButtonType.Buy;
            }


            _type = bType;
            _price = price;
            _textPrice.text = price.ToString("N0");

            switch (_type)
            {
                case ButtonType.Choosen:
                    Visual(0);
                    break;
                case ButtonType.Choose:
                    Visual(1);
                    break;
                case ButtonType.Buy:
                    Visual(2);
                    break;
            }
        }

        private void Visual(int id)
        {
            for (int i = 0; i < _visualButton.Length; i++)
            {
                _visualButton[i].SetActive(i == id);
            }
        }

        private void OnValidate()
        {
            SetVisual(_price, _type);
        }
    }
}
