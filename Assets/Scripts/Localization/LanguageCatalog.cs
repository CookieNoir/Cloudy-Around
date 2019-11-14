using UnityEngine;
[AddComponentMenu("_Localization/Language Catalog")]
public class LanguageCatalog : MonoBehaviour
{
    public RectTransform panel;
    public SliderAction sliderAction;
    public GameObject languageButtonShort;
    public GameObject languageButtonLong;
    public int longToShortAmount;
    private int buttonAmount = 0;

    void Start()
    {
        /*Doesn't work with APK
        DirectoryInfo info = new DirectoryInfo(Application.dataPath + "/Localization");
        FileInfo[] fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".txt")
            {
                GameObject button = Instantiate(languageButtonPrefab);
                button.transform.SetParent(panel, false);
                button.GetComponent<LanguageButton>().SetProperties(Path.GetFileNameWithoutExtension(file.FullName), "Localization/"+file.Name);
                button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x,buttonAmount*(-30));
                buttonAmount += 1;
            }
        }
        */
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Localization");
        buttonAmount = textAssets.Length;
        for (int i = 0; i < textAssets.Length; ++i)
        {
            string text = textAssets[i].text;
            int index = text.IndexOf('@');
            string name = text.Substring(0, index);
            GameObject button;
            if (buttonAmount<longToShortAmount) button = Instantiate(languageButtonLong);
            else button = Instantiate(languageButtonShort);
            button.transform.SetParent(panel, false);
            button.GetComponent<LanguageButton>().SetProperties(name, "Localization/" + name);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x, i * (-30));
        }
        panel.sizeDelta = new Vector2(panel.sizeDelta.x,buttonAmount*30);
        sliderAction.RefreshSlider();
    }

    void Update()
    {
        
    }
}
