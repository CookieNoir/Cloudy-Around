using UnityEngine;
[AddComponentMenu("_Special/Special")]
public class Special : MonoBehaviour
{
    private static Special instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
            instance = this as Special;
        }
        else Destroy(gameObject);
    }
}
