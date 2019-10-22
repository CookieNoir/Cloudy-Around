using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Cloudy Around UI/Language Button")]
public class LanguageButton : MonoBehaviour
{
    public Text buttonText;
    public string path;

    public void SetProperties(string text, string newPath)
    {
        buttonText.text = text;
        path = newPath;
    }

    public void SetLanguage()
    {
        Localizator.SetLanguage(path);
    }
}
