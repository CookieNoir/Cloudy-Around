using UnityEngine;
[AddComponentMenu("_Ground Objects/Active/Switcher")]
public class Switcher : GroundObject
{
    public WorkingObject workingObject;
    private bool on = false;

    public override void Hit() {
        on = !on;
        if (on) workingObject.ChangeValue(1);
        else workingObject.ChangeValue(-1);
    }
}
