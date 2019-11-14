using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("_Cloudy Around UI/Scene Changer")]
public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        DarkScreen.LoadAnotherScene(sceneName);
    }

    public void ReloadScene()
    {
        DarkScreen.LoadAnotherScene(SceneManager.GetActiveScene().name);
    }
}
