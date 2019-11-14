using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[AddComponentMenu("_Special/Dark Screen")]
public class DarkScreen : MonoBehaviour
{
    private static DarkScreen instance;
    private static MaskableGraphic darkScreen;
    private static GameObject canvas;
    private static float alpha = 1;
    private static Color color;
    private static IEnumerator darkCoroutine;
    private static string sceneName;

    private void Start()
    {
        instance = this as DarkScreen;
        darkScreen = GameObject.FindWithTag("Dark Screen").GetComponent<MaskableGraphic>();
        canvas = GameObject.FindWithTag("Dark Screen Canvas");
        color = darkScreen.color;
        color.a = alpha;
        darkScreen.color = color;
        darkCoroutine = LevelStart();
        instance.StartCoroutine(darkCoroutine);
        SceneManager.sceneLoaded += OnSceneLoaded;// OnSceneLoaded will not be called at the first cycle
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        instance.StopCoroutine(darkCoroutine);
        darkCoroutine = LevelStart();
        instance.StartCoroutine(darkCoroutine);
    }

    private static IEnumerator LevelStart()
    {
        while (alpha > 0)
        {
            alpha -= 0.0625f;
            color.a = alpha;
            darkScreen.color = color;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        alpha = 0;
        color.a = alpha;
        canvas.SetActive(false);
    }

    private static IEnumerator LevelEnd()
    {
        while (alpha < 1)
        {
            alpha += 0.0625f;
            color.a = alpha;
            darkScreen.color = color;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        alpha = 1f;
        color.a = alpha;
        darkScreen.color = color;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadAnotherScene(string name)
    {
        instance.StopCoroutine(darkCoroutine);
        darkCoroutine = LevelEnd();
        sceneName = name;
        canvas.SetActive(true);
        instance.StartCoroutine(darkCoroutine);
    }
}
