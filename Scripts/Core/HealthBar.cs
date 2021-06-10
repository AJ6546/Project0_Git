using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent;
    [SerializeField] RectTransform foreground;
    void Start()
    {
        healthComponent = GetComponentInParent<Health>();
    }

    void Update()
    {
        foreground.localScale = new Vector3(Mathf.Max(healthComponent.GetHealthFactor(),0),1, 1);
    }
}
