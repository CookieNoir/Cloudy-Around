using UnityEngine;
[AddComponentMenu("_Ground Objects/Ground Objects/Tree")]
public class GroundTree : GroundObject
{
    public Material barkMaterial;
    public Material leavesMaterial;

    protected override void Start()
    {
        gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_OutlineColor", Color.clear);
        gameObject.GetComponent<MeshRenderer>().materials[1].SetColor("_OutlineColor", Color.clear);
        pointOfInterest = transform.TransformPoint(pointOfInterest);
    }

    public override void UpdateCollisions(int value)
    {
        if (!hitByLightning)
        {
            collisions += value;
            switch (collisions)
            {
                case 0:
                    {
                        isCollided = false;
                        gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_OutlineColor", Color.clear);
                        gameObject.GetComponent<MeshRenderer>().materials[1].SetColor("_OutlineColor", Color.clear);
                        CloudCollector.ChangeCount(-1);
                        break;
                    }
                case 1:
                    {
                        isCollided = true;
                        gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_OutlineColor", Color.white);
                        gameObject.GetComponent<MeshRenderer>().materials[1].SetColor("_OutlineColor", Color.white);
                        if (value > 0) CloudCollector.ChangeCount(1);
                        break;
                    }
            }
        }
    }

    public override void Hit()
    {
        Material[] mats = gameObject.GetComponent<MeshRenderer>().materials;
        mats[0] = barkMaterial;
        mats[1] = leavesMaterial;
        gameObject.GetComponent<MeshRenderer>().materials = mats;
        Lock();
    }
}
