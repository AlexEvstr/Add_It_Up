using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonScaleEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float pressedScale = 0.9f;
    [SerializeField] private float duration = 0.1f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(originalScale * pressedScale, duration).SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(originalScale, duration).SetEase(Ease.OutBack);
    }
}