using UnityEngine;
[AddComponentMenu("_Cloud System/Collision Checker")]
public class CollisionChecker : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponent<GroundObject>().UpdateCollisions(1);
    }
    void OnTriggerExit(Collider collider)
    {
        collider.gameObject.GetComponent<GroundObject>().UpdateCollisions(-1);
    }
}
