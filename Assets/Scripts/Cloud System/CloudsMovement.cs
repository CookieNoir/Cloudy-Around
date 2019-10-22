using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Cloud System/Clouds Movement")]
public class CloudsMovement : MonoBehaviour
{
    public float curSpeed = 1f;
    public float maxSpeed = 3f;
    public float border = 16f;

    void Start()
    {
        curSpeed *= Time.fixedDeltaTime;
        maxSpeed *= Time.fixedDeltaTime;
        border = Mathf.Abs(border);
    }
    void FixedUpdate()
    {
        transform.position += Vector3.right * curSpeed;
        if (transform.position.x >= border) transform.position = new Vector3(-border,transform.position.y, transform.position.z);
        else if (transform.position.x < -border) transform.position = new Vector3(border, transform.position.y, transform.position.z);
    }
}
