using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingBarRx : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    private string sceneToLoad = "MenuScene";

    private const float duration = 2.5f;

    private void Start()
    {
        Time.timeScale = 1;
        loadingImage.fillAmount = 0f;
        StartCoroutine(LoadProgress());
    }

    private IEnumerator LoadProgress()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            loadingImage.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
