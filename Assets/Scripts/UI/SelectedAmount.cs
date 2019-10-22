using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Cloudy Around UI/Amount Of Selected")]
[RequireComponent(typeof(Text))]
public class SelectedAmount : MonoBehaviour
{
    private void Start()
    {
        CloudCollector.SelectedChange += ChangeNumber;
    }
    public void ChangeNumber()
    {
        GetComponent<Text>().text = CloudCollector.selectedCount.ToString();
    }
}
