using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeoxiderUi
{
    public class VisualToggle : MonoBehaviour
    {
        [System.Serializable]
        public class ImageVariant
        {
            public Image image;
            public Sprite start;
            public Sprite end;
        }

        [System.Serializable]
        public class TmpColorTextVariant
        {
            public TextMeshProUGUI tmp;
            public Color start = Color.white;
            public Color end = Color.white;
            public bool use_text = false;
            public string start_t;
            public string end_t;
        }

        [System.Serializable]
        public class GameObjectVariant
        {
            public GameObject[] starts;
            public GameObject[] ends;
        }

        public ImageVariant[] imageV = new ImageVariant[0];
        public TmpColorTextVariant[] textColor = new TmpColorTextVariant[0];
        public GameObjectVariant variants;

        public bool end;

        public void Press()
        {
            end = true;
            Visual();
        }

        public void EndPress()
        {
            end = false;
            Visual();
        }

        public void Visual()
        {
            if (imageV != null)
                ImageVisual();

            if (textColor != null)
                TextColorVisual();

            if (variants != null)
                VariantVisual();
        }

        public void Visual(bool activ)
        {
            this.end = activ;
            Visual();
        }

        private void ImageVisual()
        {
            foreach (ImageVariant v in imageV)
            {
                v.image.sprite = end ? v.end : v.start;
            }
        }

        private void TextColorVisual()
        {
            foreach (TmpColorTextVariant t in textColor)
            {
                if (t.tmp != null)
                {
                    t.tmp.color = end ? t.end : t.start;

                    if (t.use_text)
                    {
                        t.tmp.text = end ? t.end_t : t.start_t;
                    }
                }
            }
        }

        private void VariantVisual()
        {
            for (int i = 0; i < variants.starts.Length; i++)
            {
                variants.starts[i].SetActive(!end);
            }

            for (int i = 0; i < variants.ends.Length; i++)
            {
                variants.ends[i].SetActive(end);
            }
        }

        private void OnValidate()
        {
            if (!end)
            {
                foreach (ImageVariant v in imageV)
                {
                    if (v.image != null)
                        if (v.start == null)
                            v.start = v.image.sprite;
                }

                foreach (TmpColorTextVariant t in textColor)
                {
                    if (t.tmp != null)
                    {
                        if (t.start == null)
                            t.start = t.tmp.color;

                        if (!t.use_text)
                            t.start_t = t.tmp.text;
                    }
                }
            }

            Visual();
        }
    }
}