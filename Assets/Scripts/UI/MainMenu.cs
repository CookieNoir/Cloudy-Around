using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("_Cloudy Around UI/Main Menu")]
public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;

    }

    public void LoadLevel() {
        SceneManager.LoadScene(1);
    }
}
