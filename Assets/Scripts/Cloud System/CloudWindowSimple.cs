#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using UnityEditor;

public class CloudWindowSimple : EditorWindow
{
    Vector2 scrollPos;

    Texture2D cloudTexture;
    int scaleX = 16;
    int scaleY = 16;
    int offsetX = 0;
    int offsetY = 0;
    float step = 1f;
    float threshold = 0.5f;
    bool tearX = false;
    bool tearY = false;
    GameObject empty;
    GameObject cloudLone;
    GameObject cloudCentral;
    GameObject cloudCornerTL;
    GameObject cloudDemiIslandT;

    [MenuItem("Window/Cloud Generator Simple")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CloudWindowSimple));
    }

    private static Texture2D TextureField(string name, Texture2D texture)
    {
        GUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 100;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.EndVertical();
        return result;
    }

    private void SpawnClouds()
    {
        if (empty && cloudLone && cloudCentral && cloudCornerTL && cloudDemiIslandT)
        {
            bool modified = false;
            int iHeight = cloudTexture.height, iWidth = cloudTexture.width;
            if (scaleX > cloudTexture.width) { scaleX = cloudTexture.width; modified = true; }
            if (scaleY > cloudTexture.height) { scaleY = cloudTexture.height; modified = true; }
            if (modified) Debug.Log("Scale changed to " + scaleX + ":" + scaleY);
            GameObject em = Instantiate(empty, new Vector3(0, 0, 0), empty.transform.rotation);
            GameObject child;
            for (int width = 0; width < scaleX; width++)
                for (int height = 0; height < scaleY; height++)
                    if (cloudTexture.GetPixel(width+offsetX, height+offsetY).r >= threshold)
                    {
                        int num = 0, rot = 0;
                        bool top = false, left = false, right = false, bottom = false;
                        if ((!tearY || height + 1 < scaleY) && cloudTexture.GetPixel(((width) % scaleX + offsetX) % iWidth, ((height + 1) % scaleY + offsetY) % iHeight).r >= threshold) { num++; top = true; }
                        if ((!tearY || height - 1 >= 0) && cloudTexture.GetPixel(((width) % scaleX + offsetX) % iWidth, ((height - 1) % scaleY + offsetY) % iHeight).r >= threshold) { num++; bottom = true; }
                        if ((!tearX || width + 1 < scaleX) && cloudTexture.GetPixel(((width + 1) % scaleX + offsetX) % iWidth, ((height) % scaleY + offsetY) % iHeight).r >= threshold) { num++; right = true; }
                        if ((!tearX || width - 1 >= 0) && cloudTexture.GetPixel(((width - 1) % scaleX + offsetX) % iWidth, ((height) % scaleY + offsetY) % iHeight).r >= threshold) { num++; left = true; }
                        switch (num)
                        {
                            case 0:
                                {
                                    child = cloudLone;
                                    break;
                                }
                            case 1:
                                {
                                    if (bottom) { child = cloudDemiIslandT; }
                                    else
                                    if (left) { child = cloudDemiIslandT; rot = 1; }
                                    else
                                    if (top) { child = cloudDemiIslandT; rot = 2; }
                                    else
                                        { child = cloudDemiIslandT; rot = 3; }
                                    break;
                                }
                            case 2:
                                {
                                    if (bottom && right) { child = cloudCornerTL; }
                                    else
                                    if (bottom && left) { child = cloudCornerTL; rot = 1; }
                                    else
                                    if (top && left) { child = cloudCornerTL; rot = 2; }
                                    else
                                    if (top && right) { child = cloudCornerTL; rot = 3; }
                                    else
                                        child = cloudCentral;
                                    break;
                                }
                            default:
                                {
                                    child = cloudCentral;
                                    break;
                                }
                        }
                        child = Instantiate(child, new Vector3((width - scaleX / 2 + 0.5f) * step, 0, (height - scaleY / 2 + 0.5f) * step), Quaternion.Euler(0,90*rot,0));
                        child.transform.parent = em.transform;
                    }
        }
        else Debug.Log("Some components aren't set.");
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        GUILayout.Label("Texture and step size", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        scaleX = EditorGUILayout.IntField("Scale X, pixels", scaleX);
        scaleY = EditorGUILayout.IntField("Scale Y, pixels", scaleY);
        offsetX = EditorGUILayout.IntField("Offset X, pixels", offsetX);
        offsetY = EditorGUILayout.IntField("Offset Y, pixels", offsetY);
        step = EditorGUILayout.FloatField("Step", step);
        threshold = EditorGUILayout.Slider("Threshold", threshold, 0, 1);
        tearX = EditorGUILayout.Toggle("Tear along the X axis", tearX);
        tearY = EditorGUILayout.Toggle("Tear along the Y axis", tearY);
        EditorGUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        cloudTexture = TextureField("Texture (R)", cloudTexture);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Cloud parts", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        empty = (GameObject)EditorGUILayout.ObjectField("Empty (will contain all clouds)", empty, typeof(GameObject), false);
        GUILayout.Space(10);
        cloudLone = (GameObject)EditorGUILayout.ObjectField("Lone cloud", cloudLone, typeof(GameObject), false);
        cloudCentral = (GameObject)EditorGUILayout.ObjectField("Central cloud", cloudCentral, typeof(GameObject), false);
        cloudCornerTL = (GameObject)EditorGUILayout.ObjectField("Top-left corner", cloudCornerTL, typeof(GameObject), false);
        cloudDemiIslandT = (GameObject)EditorGUILayout.ObjectField("Top demi-island", cloudDemiIslandT, typeof(GameObject), false);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        GUILayout.Space(30);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(Screen.width / 2 - 50);
        if (GUILayout.Button("Spawn clouds", GUILayout.Height(60), GUILayout.Width(100)))
        {
            SpawnClouds();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }
}
#endif