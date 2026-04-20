using System.Collections;
using UnityEngine;

public class BannerPlayer : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private float moveDistance = 50f;
    [SerializeField] private float moveSpeed = 2f;

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        StartCoroutine(AnimateAndHide());
    }

    private IEnumerator AnimateAndHide()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float offset = Mathf.Sin(elapsed * moveSpeed) * moveDistance;
            rectTransform.anchoredPosition = new Vector2(
                startPosition.x, 
                startPosition.y + offset
            );
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}