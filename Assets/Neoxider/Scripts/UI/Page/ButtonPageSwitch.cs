using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NeoxiderUi
{
    public class ButtonPageSwitch : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private PageType _pageType = PageType.Menu;

        /// <summary>
        /// меняет одну на другую или выкключает все и включает только одну
        /// </summary>
        [SerializeField]
        private bool _change = true;

        /// <summary>
        /// Вернуть на предыдущую страницу
        /// </summary>
        [SerializeField]
        private bool _switchLastPage = false;

        [SerializeField]
        private PagesManager _uiManager;

        private void Awake()
        {

        }

        private void OnEnable()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(SwitchPage);
            }
        }

        private void OnDisable()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(SwitchPage);
            }
        }

        private void OnMouseDown()
        {
            if (_button == null)
            {
                SwitchPage();
            }
        }

        private void SwitchLastPage()
        {
            _uiManager.SwitchLastPage();
        }

        public void SwitchPage()
        {
           

            if (_change)
            {
                if (_switchLastPage)
                {

                    SwitchLastPage();
                }
                else
                {
                    _uiManager.ChangePage(_pageType);
                }

            }
            else
            {
                _uiManager.SetPage(_pageType);
            }

        }

        private void OnValidate()
        {
#if UNITY_2023_1_OR_NEWER
            uiManager = FindFirstObjectByType<ScreenManager>();
#else
                _uiManager = FindObjectOfType<PagesManager>();
#endif 

#if UNITY_EDITOR
            if (!AssetDatabase.Contains(this))
            {
                if (_uiManager == null)
                {
                    Debug.LogWarning("Need UiManager");
                }
            }
#endif
            if (_change)
            {
                if (_switchLastPage)
                    _pageType = PageType.None;
            }
            else
            {
                _switchLastPage = false;
            }


            _button = GetComponent<Button>();
        }
    }
}
