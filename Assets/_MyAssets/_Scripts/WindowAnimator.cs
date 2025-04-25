using DG.Tweening;
using UnityEngine;

public class WindowAnimator : MonoBehaviour
{
    private float animationDuration = 0.4f;
    [SerializeField] private Ease ease = Ease.OutBack;

    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);

        var animated = window.transform.GetChild(0);
        animated.localScale = Vector3.zero;

        animated.DOScale(Vector3.one, animationDuration)
            .SetEase(ease);
    }

    public void CloseWindow(GameObject window)
    {
        var animated = window.transform.GetChild(0);

        animated.DOScale(Vector3.zero, animationDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => window.SetActive(false));
    }
}
