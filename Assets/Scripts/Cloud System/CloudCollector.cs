using System.Collections;
using UnityEngine;
[AddComponentMenu("_Cloud System/Cloud Collector")]
public class CloudCollector : MonoBehaviour
{
    public static GroundObject[] targets;
    public static int selectedCount;

    private int[] finalTargets;
    private int targetsLength;

    private int highestPriority;
    private int finalCount;
    private bool canCastLightning;

    public GameObject lightning;
    private Vector3 lightningSafePosition;
    public Light sun;
    public Color mainSunColor;
    public Color lightningSunColor;
    public Color mainAmbientColor;
    public Color lightningAmbientColor;

    private IEnumerator fadeCoroutine;

    public delegate void OnSelectedChange();
    public static event OnSelectedChange SelectedChange;

    void Awake()
    {
        GameObject[] allGrounds = GameObject.FindGameObjectsWithTag("Ground");
        targetsLength = allGrounds.Length;
        targets = new GroundObject[targetsLength];
        finalTargets = new int[targetsLength];
        for (int i = 0; i < targetsLength; ++i) {
            targets[i] = allGrounds[i].GetComponent<GroundObject>();
        }
        selectedCount = 0;
        lightningSafePosition = lightning.transform.position;
        canCastLightning = true;
        fadeCoroutine = Fade();
        SelectedChange = null;
    }

    public void LaunchLightning()
    {
        if (canCastLightning)
        {
            highestPriority = -1;
            for (int i = 0; i < targetsLength; ++i)
            {
                if (targets[i].isCollided&&!targets[i].hitByLightning)
                    if (targets[i].priority > highestPriority)
                    {
                        highestPriority = targets[i].priority;
                        finalTargets[0] = i;
                        finalCount = 1;
                    }
                    else if (targets[i].priority == highestPriority)
                    {
                        finalTargets[finalCount] = i;
                        finalCount++;
                    }
            }
            if (highestPriority == -1)
            {
                canCastLightning = true;
                return;
            }
            StopCoroutine(fadeCoroutine);
            RenderSettings.ambientLight = lightningAmbientColor;
            sun.color = lightningSunColor;
            StartCoroutine(MoveLightning());
        }
    }

    IEnumerator MoveLightning()
    {
        for (int i = 0; i < finalCount; ++i)
        {
                lightning.transform.position = targets[finalTargets[i]].pointOfInterest;
                targets[finalTargets[i]].Hit();
                yield return new WaitForSeconds(0.0625f);
        }
        lightning.transform.position = lightningSafePosition;
        canCastLightning = true;
        fadeCoroutine = Fade();
        StartCoroutine(fadeCoroutine);
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.03125f)
        {
            Color c = Vector4.Lerp(mainSunColor, lightningSunColor, f);
            Color d = Vector4.Lerp(mainAmbientColor, lightningAmbientColor, f);
            sun.color = c;
            RenderSettings.ambientLight = d;
            yield return new WaitForSeconds(0.03125f);
        }
        RenderSettings.ambientLight = mainAmbientColor;
        sun.color = mainSunColor;
    }

    public static void ChangeCount(int value) {
        selectedCount += value;
        SelectedChange?.Invoke();
    }
}
