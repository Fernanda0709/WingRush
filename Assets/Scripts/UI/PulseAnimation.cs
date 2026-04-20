using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    [SerializeField] private float minScale = 0.95f;
    [SerializeField] private float maxScale = 1.05f;
    [SerializeField] private float speed = 2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, 
                      (Mathf.Sin(Time.unscaledTime * speed) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }
}