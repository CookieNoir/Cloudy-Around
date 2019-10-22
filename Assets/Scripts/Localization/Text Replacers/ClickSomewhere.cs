using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Localization/Text Replacers/UI Click Somewhere Text")]
[RequireComponent(typeof(Text))]
public class ClickSomewhere : TextReplacer
{
    public string text2_id;
    protected override void OnLanguageChange()
    {
        GetComponent<Text>().text = Localizator.dictionary[text_id] + "\n" + Localizator.dictionary[text2_id];
    }
}
