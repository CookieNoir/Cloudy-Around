using UnityEngine;
[AddComponentMenu("_Navigation/Bezier Curve")]
[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    [Range(0,1)] public float t;
    public Vector3 P0;
    public Vector3 P1;
    public Vector3 P2;

    public Vector3 BezierCurveFunction(float step, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return (1 - step) * (1 - step) * p0 + 2 * step * (1 - step) * p1 + step * step * p2;
    }

    private void OnValidate()
    {
        Vector3[] points = new Vector3[101];
        for (int i = 0; i < 101; ++i)
        {
            points[i] = BezierCurveFunction(0.01f*i,P0,P1,P2);
        }
        GetComponent<LineRenderer>().positionCount = 101;
        GetComponent<LineRenderer>().SetPositions(points);
        transform.position = BezierCurveFunction(t, P0, P1, P2);
    }
}
