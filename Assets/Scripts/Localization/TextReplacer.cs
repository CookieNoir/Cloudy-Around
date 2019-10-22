using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Localization/UI Text Replacer")]
[RequireComponent(typeof(Text))]
public class TextReplacer : MonoBehaviour
{
    public string text_id;

    void Start()
    {
        OnLanguageChange();
        Localizator.LanguageChange += OnLanguageChange;
    }

    protected virtual void OnLanguageChange()
    {
        GetComponent<Text>().text = Localizator.dictionary[text_id];
    }

    protected void OnDestroy()
    {
        Localizator.LanguageChange -= OnLanguageChange;
    }
}
