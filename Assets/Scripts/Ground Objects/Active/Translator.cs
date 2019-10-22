using UnityEngine;
[AddComponentMenu("_Ground Objects/Active/Translator")]
public class Translator: GroundObject
{
    public WorkingObject workingObject;

    public override void Hit()
    {
        workingObject.ChangeValue(1);
        Lock();
    }
}
