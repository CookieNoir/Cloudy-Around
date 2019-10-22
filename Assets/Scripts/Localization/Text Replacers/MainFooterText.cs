using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Localization/Text Replacers/UI Main Footer Text")]
[RequireComponent(typeof(Text))]
public class MainFooterText : TextReplacer
{
    public string version_id;
    protected override void OnLanguageChange()
    {
        GetComponent<Text>().text = Localizator.dictionary[version_id] + ' ' + Application.version + ' ' + Localizator.dictionary[text_id];
    }
}
