using UnityEngine;
[AddComponentMenu("_Cloud System/Collision Checker")]
public class CollisionChecker : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.GetComponent<GroundObject>().UpdateCollisions(1);
    }
    void OnCollisionExit(Collision collision)
    {
        collision.collider.gameObject.GetComponent<GroundObject>().UpdateCollisions(-1);
    }
}
