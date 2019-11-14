using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[AddComponentMenu("_Localization/Localizator")]
public class Localizator : MonoBehaviour
{
    public static Dictionary<string, string> dictionary;

    public delegate void OnLanguageChange();
    public static event OnLanguageChange LanguageChange;

    private void Awake()
    {
            dictionary = new Dictionary<string, string>();
            SetLanguage(PlayerPrefs.GetString("Language", "Localization/English"));
    }

    public static void SetLanguage(string path)
    {
        /*Doesn't work with APK
        StreamReader reader = new StreamReader(Application.dataPath + "/" + path);
        using (reader)
        {
            do
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    int index = line.IndexOf(' ');
                    string id = line.Substring(0, index);
                    string word = line.Substring(index + 1, line.Length - index - 1);
                    dictionary[id] = word;
                }
            }
            while (line != null);

            reader.Close();
        }
        */
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        string text = textAsset.text;
        string[] textLines = Regex.Split(text, "\n|\r|\r\n");

        for (int i = 2; i < textLines.Length; i+=2)
        {
            int index = textLines[i].IndexOf(' ');
            string id = textLines[i].Substring(0, index);
            string word = textLines[i].Substring(index + 1, textLines[i].Length - index - 1);
            dictionary[id] = word;
        }

        PlayerPrefs.SetString("Language", path);

        LanguageChange?.Invoke();
    }
}
