using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Ground Objects/Working Object")]
public class WorkingObject : MonoBehaviour
{
    public float requiredValue = 1f;
    private float value = 0f;

    public void ChangeValue(int amount) {
        value += amount;
        if (value >= requiredValue) Work();
    }

    public virtual void Work() {
        Debug.Log("FireWork!");
    }
}
