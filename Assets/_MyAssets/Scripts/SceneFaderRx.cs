using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFaderRx : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private float fadeDuration = 0.5f;

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(FadeIn());
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        fadeImage.color = new Color(1f, 1f, 1f, 1f);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = 1f - (time / fadeDuration);
            fadeImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(1f, 1f, 1f, 0f); // гарантируем полную прозрачность
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float time = 0f;
        fadeImage.color = new Color(1f, 1f, 1f, 0f);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = time / fadeDuration;
            fadeImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(1f, 1f, 1f, 1f); // гарантируем полную непрозрачность

        SceneManager.LoadScene(sceneName);
    }
}
