using System.Collections;
using UnityEngine;
[AddComponentMenu("_Ground Objects/Active/Timer")]
public class Timer : GroundObject
{
    public WorkingObject workingObject;
    public float setTime = 5f;
    protected float timer = 0f;
    protected float step = Time.fixedDeltaTime;

    public override void Hit()
    {
        if (!hitByLightning)
        {
            timer = setTime;
            workingObject.ChangeValue(1);
            Lock();
            StartCoroutine("TimeRoutine");
        }
    }

    IEnumerable TimeRoutine() {
        while (timer > 0)
        {
            timer -= step;
            yield return new WaitForSeconds(step);
        }
        workingObject.ChangeValue(-1);
        Unlock();
    }

}
