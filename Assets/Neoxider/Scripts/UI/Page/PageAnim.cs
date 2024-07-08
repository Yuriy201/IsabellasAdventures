using UnityEngine;

namespace NeoxiderUi
{
    [RequireComponent(typeof(Animator))]
    public class PageAnim : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _startAnimTrigger;

        void Start()
        {

        }

        internal void StartAnim()
        {
            if (_startAnimTrigger != "")
                _animator.SetTrigger(_startAnimTrigger);
        }

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }
    }
}