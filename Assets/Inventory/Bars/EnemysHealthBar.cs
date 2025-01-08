using UnityEngine;
using UnityEngine.UI;

public class EnemysHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 offset;

    private Transform target;

    public void Initialize(Transform targetTransform, float maxHealth)
    {
        target = targetTransform;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateHealth(float Health)
    {
        healthSlider.value = Health;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }
}
