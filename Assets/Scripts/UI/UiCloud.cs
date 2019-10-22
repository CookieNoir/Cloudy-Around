using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Cloudy Around UI/UI Cloud")]
[RequireComponent(typeof(MaskableGraphic))]
public class UiCloud : MonoBehaviour
{
    protected Color color;
    protected float baseAlpha;
    protected float alpha;

    protected virtual void Awake()
    {
        color = GetComponent<MaskableGraphic>().color;
        baseAlpha = color.a;
    }

    public virtual IEnumerator Fade() {
        alpha = baseAlpha;
        while (alpha > 0)
        {
            color.a = alpha;
            GetComponent<MaskableGraphic>().color = color;
            alpha -= 0.03125f;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        alpha = 0;
        color.a = alpha;
        GetComponent<MaskableGraphic>().color = color;
    }

    public virtual IEnumerator Bloom()
    {
        alpha = 0;
        while (alpha < baseAlpha)
        {
            color.a = alpha;
            GetComponent<MaskableGraphic>().color = color;
            alpha += 0.03125f;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        alpha = baseAlpha;
        color.a = alpha;
        GetComponent<MaskableGraphic>().color = color;
    }
}
