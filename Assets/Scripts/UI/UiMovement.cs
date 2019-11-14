using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Cloudy Around UI/UI Movement")]
[RequireComponent(typeof(RectTransform))]
public class UiMovement : MonoBehaviour
{
    public enum lines { Top, Right, Local_Center, Bottom_Right, Top_Right };
    public enum functions { Linear, Square, Cubic };
    private delegate float movingFunc(float value);

    public lines line;
    public functions function;
    public float stepSize;

    private Vector2 defaultPosition;
    private Vector2 newPosition;
    private bool moved = false; // If moved, makes a step back
    private movingFunc movingFunction;
    private IEnumerator activeMovement;

    private void Start()
    {
        defaultPosition = GetComponent<RectTransform>().anchoredPosition;
        switch (line)
        {
            case lines.Top:
                {
                    newPosition = new Vector2(defaultPosition.x, defaultPosition.y + stepSize);
                    break;
                }
            case lines.Right:
                {
                    newPosition = new Vector2(defaultPosition.x + stepSize, defaultPosition.y);
                    break;
                }
            case lines.Local_Center:
                {
                    newPosition = defaultPosition + defaultPosition.normalized * stepSize;
                    break;
                }
            case lines.Bottom_Right:
                {
                    newPosition = new Vector2(defaultPosition.x + stepSize, defaultPosition.y - stepSize);
                    break;
                }
            case lines.Top_Right:
                {
                    newPosition = new Vector2(defaultPosition.x + stepSize, defaultPosition.y + stepSize);
                    break;
                }
        }
        switch (function)
        {
            case functions.Linear:
                {
                    movingFunction = Linear;
                    break;
                }
            case functions.Square:
                {
                    movingFunction = Square;
                    break;
                }
            case functions.Cubic:
                {
                    movingFunction = Cubic;
                    break;
                }
        }
        activeMovement = SmoothMove(newPosition, defaultPosition, movingFunction);
    }

    private float Linear(float f)
    {
        return f;
    }
    private float Square(float f)
    {
        return f * f;
    }
    private float Cubic(float f)
    {
        return f * f * f;
    }

    public void Translate()
    {
        StopCoroutine(activeMovement);
        if (!moved)
            {
            activeMovement = SmoothMove(GetComponent<RectTransform>().anchoredPosition, newPosition, movingFunction);
            }
        else
            {
            activeMovement = SmoothMove(GetComponent<RectTransform>().anchoredPosition, defaultPosition, movingFunction);
            }
        StartCoroutine(activeMovement);
        moved = !moved;
    }

    private IEnumerator SmoothMove(Vector2 startPosition, Vector2 endPosition, movingFunc func)
    {
        for (float f = 1; f > 0; f -= 0.03125f)
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(endPosition, startPosition, func(f));
            yield return new WaitForSecondsRealtime(0.03125f);
        }
        GetComponent<RectTransform>().anchoredPosition = endPosition;
    }
}
