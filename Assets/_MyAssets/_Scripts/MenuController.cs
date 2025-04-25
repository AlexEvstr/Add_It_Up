using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void QuitWithDelay()
    {
        StartCoroutine(QuitCoroutine());
    }

    private IEnumerator QuitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
