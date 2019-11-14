using UnityEngine;
[AddComponentMenu("_Ground Objects/Ground Object")]
[RequireComponent(typeof(Collider))]
public class GroundObject : MonoBehaviour
{
    public int priority = 0;
[Tooltip("Local Coordinates")]
    public Vector3 pointOfInterest;
    public int collisions = 0;
    public bool isCollided = false;
    public bool hitByLightning = false;
    public virtual void Hit() { }

    protected virtual void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.clear);
        pointOfInterest = transform.TransformPoint(pointOfInterest);
    }

    public virtual void UpdateCollisions(int value) {
        if (!hitByLightning)
        {
            collisions += value;
            switch (collisions) {
                case 0:
                    {
                        isCollided = false;
                        gameObject.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor",Color.clear);
                        CloudCollector.ChangeCount(-1);
                        break;
                    }
                case 1:
                    {
                        isCollided = true;
                        gameObject.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.white);
                        if (value > 0) CloudCollector.ChangeCount(1);
                        break;
                    }
            }
        }
    }

    protected void Lock() {
        GetComponent<Collider>().enabled = false;
        if (isCollided)
        {
            CloudCollector.ChangeCount(-1);
            isCollided = false;
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.clear);
            collisions = 0;
        }
        hitByLightning = true;
    }

    protected void Unlock()
    {
        hitByLightning = false;
        GetComponent<Collider>().enabled = true;
    }
}
