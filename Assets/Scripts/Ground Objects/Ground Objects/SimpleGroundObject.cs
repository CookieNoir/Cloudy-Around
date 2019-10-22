using UnityEngine;
[AddComponentMenu("_Ground Objects/Ground Objects/Simple Object")]
public class SimpleGroundObject : GroundObject
{
    public Material material;

    public override void Hit()
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
        Lock();
    }
}
