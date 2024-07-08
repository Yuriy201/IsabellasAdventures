using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeoxiderUi
{
    public class VisualTogle : MonoBehaviour
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
            public Color start;
            public Color end;
            public bool use_text = false;
            public string start_t;
            public string end_t;
        }

        [System.Serializable]
        public class GameObjectVariant
        {
            public GameObject start;
            public GameObject end;
        }

        public ImageVariant[] imageV = new ImageVariant[0];
        public TmpColorTextVariant[] textColor = new TmpColorTextVariant[0];
        public GameObjectVariant[] variants = new GameObjectVariant[0];

        public bool activ;

        public void Press()
        {
            activ = true;
            Visual();
        }

        public void EndPress()
        {
            activ = false;
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
            this.activ = activ;
            Visual();
        }

        private void ImageVisual()
        {
            foreach (ImageVariant v in imageV)
            {
                v.image.sprite = activ ? v.end : v.start;
            }
        }

        private void TextColorVisual()
        {
            foreach (TmpColorTextVariant t in textColor)
            {
                if (t.tmp != null)
                {
                    t.tmp.color = activ ? t.end : t.start;

                    if (t.use_text)
                    {
                        t.tmp.text = activ ? t.end_t : t.start_t;
                    }
                }
            }
        }

        private void VariantVisual()
        {
            foreach (GameObjectVariant v in variants)
            {
                v.start.SetActive(!activ);
                v.end.SetActive(activ);
            }
        }

        private void OnValidate()
        {
            if (!activ)
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