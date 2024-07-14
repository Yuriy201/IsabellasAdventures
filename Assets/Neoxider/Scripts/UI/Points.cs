using UnityEngine;
using UnityEngine.UI;


namespace NeoxiderUi
{
    public class Points : MonoBehaviour
    {
        [SerializeField] private Image[] points;
        [SerializeField] private Sprite[] sprites_off_on = new Sprite[2];
        [SerializeField] private bool fill;
        [SerializeField] private bool flip;

        [SerializeField] private bool zeroPoints;

        [SerializeField] private int id;

        public void SetPoint(int value)
        {
            id = value;
            value = SafeId(value);

            if (zeroPoints)
                value -= 1;

            if (flip)
                value = points.Length - 1 - value;

            if (sprites_off_on.Length == 2)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    int i = j;
                    bool activ = fill ? (flip ? value <= i : i <= value) : i == value;

                    Sprite sprite = GetSprite(activ);

                    if (sprite != null)
                        points[i].sprite = sprite;
                    else
                    {
                        Color color = points[i].color;
                        color.a = activ ? 1f : 0f;
                        points[i].color = color;
                    }
                }
            }
            else
            {

            }
        }

        public void SetPoint(float floatValue)
        {
            int maxIndex = points.Length;
            int intValue = Mathf.RoundToInt(floatValue * maxIndex);
            if(floatValue == 1f)
                intValue = maxIndex;

            SetPoint(intValue);
        }

        private Sprite GetSprite(bool type)
        {
            return type ? sprites_off_on[1] : sprites_off_on[0];
        }

        private void OnValidate()
        {
            if (sprites_off_on.Length != 2)
            {
                sprites_off_on = new Sprite[2];
            }

            id = SafeId(id);

            points = new Image[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                points[i] = transform.GetChild(i).GetComponent<Image>();
            }

            SetPoint(id);
        }

        private int SafeId(int id)
        {
            int count = zeroPoints ? points.Length + 1 : points.Length;

            if (id >= count)
                return count - 1;
            else if (id < 0)
                return 0;

            return id;
        }
    }
}
