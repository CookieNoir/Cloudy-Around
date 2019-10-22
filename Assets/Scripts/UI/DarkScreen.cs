using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("_Cloudy Around UI/Dark Screen")]
[RequireComponent(typeof(MaskableGraphic))]
public class DarkScreen : UiCloud
{
    protected override void Awake()
    {
        color = Color.black;
        baseAlpha = 1f;

    }

    void Update()
    {
        
    }
}
