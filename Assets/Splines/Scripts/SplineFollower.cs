using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] SplineContainer splineContainer;
    [SerializeField, Range(0, 40)] float speed = 1;
    [SerializeField, Range(0, 1)] float tdistance = 0; // debug use - normalized distance along spline (0-1)
    [SerializeField] bool reverse = false;

    public float Speed { get { return speed; } set { speed = value; } }

    // length in world coordinates
    public float Length => cachedLength;

    // distance in world coordinates
    public float Distance
    {
        get { return tdistance * Length; }
        set { tdistance = value / Length; }
    }

    float cachedLength;

    private void OnEnable() => cachedLength = splineContainer.CalculateLength();

    void Update()
    {
        Distance += speed * Time.deltaTime * (reverse ? -1.0f : 1.0f);
        UpdatePositionAndRotation(math.frac(tdistance));
    }

    void UpdatePositionAndRotation(float t)
    {
        Vector3 position = splineContainer.EvaluatePosition(t);
        Vector3 up = splineContainer.EvaluateUpVector(t);
        Vector3 forward = Vector3.Normalize(splineContainer.EvaluateTangent(t));

        transform.position = position;
        transform.rotation = Quaternion.LookRotation(forward, up);
    }
}