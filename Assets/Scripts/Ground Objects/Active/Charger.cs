using UnityEngine;
[AddComponentMenu("_Ground Objects/Active/Charger")]
public class Charger : GroundObject
{
    public WorkingObject workingObject;
    private float power = 0f;
    public float maxPower = 5f;

    public override void Hit()
    {
        power++;
        workingObject.ChangeValue(1);
        if (power >= maxPower) {
            Lock();
        }
    }
}
