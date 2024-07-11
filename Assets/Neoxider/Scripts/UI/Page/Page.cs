using UnityEngine;

namespace NeoxiderUi
{
    public class Page : MonoBehaviour
    {
        public PageType pageType = PageType.Other;
        [SerializeField] private PageAnim _pageAnim;

        public virtual void StartActiv()
        {
            if (_pageAnim != null)
            {
                _pageAnim.StartAnim();
            }
        }

        public virtual void EndActiv()
        {

        }

        private void OnValidate()
        {
            name = "Page " + pageType.ToString();
            _pageAnim = GetComponent<PageAnim>();

            //FindFirstObjectByType<PagesManager>().OnValidate();
        }

        public void SetActive(bool value) => gameObject.SetActive(value);
    }
}
