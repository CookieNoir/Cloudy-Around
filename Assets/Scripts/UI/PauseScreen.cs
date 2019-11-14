using System.Collections;
using UnityEngine;
[AddComponentMenu("_Cloudy Around UI/Pause Screen")]
public class PauseScreen : MonoBehaviour
{
    public GameObject pauseMenu;
    private UiCloud[] uiClouds;
    private UiMovement[] uiMovements;
    private int uiCloudsLength;
    private int uiMovementsLength;
    private bool locked = false;

    private void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("UI Cloud");
        uiCloudsLength = gos.Length;
        uiClouds = new UiCloud[uiCloudsLength];
        for (int i = 0; i<uiCloudsLength; ++i)
            uiClouds[i] = gos[i].GetComponent<UiCloud>();
        gos = GameObject.FindGameObjectsWithTag("UI Movement");
        uiMovementsLength = gos.Length;
        uiMovements = new UiMovement[uiMovementsLength];
        for (int i = 0; i < uiMovementsLength; ++i)
            uiMovements[i] = gos[i].GetComponent<UiMovement>();
        pauseMenu.SetActive(false);
    }

    public void TurnOff()
    {
        if (!locked)
        {
            locked = true;
            for (int i = 0; i < uiCloudsLength; ++i)
                StartCoroutine(uiClouds[i].Fade());
            for (int i = 0; i < uiMovementsLength; ++i)
                uiMovements[i].Translate();
            StartCoroutine(TurnOffCooldown());
        }
    }

    public void TurnOn()
    {
        if (!locked)
        {
            locked = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            for (int i = 0; i < uiCloudsLength; ++i)
                StartCoroutine(uiClouds[i].Bloom());
            for (int i = 0; i < uiMovementsLength; ++i)
                uiMovements[i].Translate();
            StartCoroutine(TurnOnCooldown());
        }
    }

    IEnumerator TurnOffCooldown()
    {
        for (float f = 1f; f > 0f; f -= 0.03125f) {
            Camera.main.fieldOfView = 60 + 10 * f * f;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        Camera.main.fieldOfView = 60f;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        locked = false;
    }

    IEnumerator TurnOnCooldown()
    {
        for (float f = 1f; f > 0f; f -= 0.03125f)
        {
            Camera.main.fieldOfView = 70 - 10 * f * f;
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        Camera.main.fieldOfView = 70f;
        locked = false;
    }
}
