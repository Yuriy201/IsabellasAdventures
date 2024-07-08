using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NeoxiderUi
{
    public class SwitchButton : MonoBehaviour
    {
        public Image image;
        public Sprite[] sprites;
        public string[] textsButton;
        public TextMeshProUGUI text;
        public bool enable = true;

        public UnityEvent<bool> OnSwitch;

        public void Switch()
        {
            enable = !enable;

            Visual();
            OnSwitch?.Invoke(enable);
        }

        private void Visual()
        {
            if (image != null && sprites.Length == 2)
                image.sprite = enable ? sprites[0] : sprites[1];

            if (text != null && textsButton.Length == 2)
                text.text = enable ? textsButton[0] : textsButton[1];
        }

        private void OnValidate()
        {
            Visual();
        }
    }
}