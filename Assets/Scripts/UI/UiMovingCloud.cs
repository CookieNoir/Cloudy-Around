using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Cloudy Around UI/UI Moving Cloud")]
public class UiMovingCloud : UiCloud
{
    private Vector2 innerpoint;
    private Vector2 outerpoint;
    protected override void Awake()
    {
        color = GetComponent<MaskableGraphic>().color;
        baseAlpha = color.a;
        innerpoint = GetComponent<RectTransform>().anchoredPosition;
        outerpoint = innerpoint+300*innerpoint.normalized;
    }

    public override IEnumerator Fade()
    {
        alpha = baseAlpha;
        while (alpha > 0)
        {
            color.a = alpha;
            GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(innerpoint, outerpoint, (baseAlpha - alpha) * (baseAlpha - alpha));
            GetComponent<MaskableGraphic>().color = color;
            alpha -= 0.03125f;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        yield return new WaitForSecondsRealtime(0.9f-baseAlpha);
        alpha = 0;
        color.a = alpha;
        GetComponent<RectTransform>().anchoredPosition = outerpoint;
        GetComponent<MaskableGraphic>().color = color;
    }

    public override IEnumerator Bloom()
    {
        alpha = 0;
        while (alpha < baseAlpha)
        {
            color.a = alpha;
            GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(innerpoint, outerpoint, (baseAlpha - alpha) * (baseAlpha - alpha));
            GetComponent<MaskableGraphic>().color = color;
            alpha += 0.03125f;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        //yield return new WaitForSecondsRealtime(0.9f - baseAlpha);
        alpha = baseAlpha;
        color.a = alpha;
        GetComponent<RectTransform>().anchoredPosition = innerpoint;
        GetComponent<MaskableGraphic>().color = color;
    }
}
