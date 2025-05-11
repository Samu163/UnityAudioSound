using UnityEngine;
using UnityEngine.Splines;

public class SplineAudioFollower : MonoBehaviour
{
    [Header("References")]
    public SplineContainer spline;
    public Transform player;

    [Header("Settings")]
    public int samplePoints = 100;

    void Update()
    {
        if (spline == null || player == null)
            return;

        float closestT = 0f;
        float closestDistance = float.MaxValue;

        for (int i = 0; i <= samplePoints; i++)
        {
            float t = (float)i / samplePoints;
            Vector3 splinePos = spline.EvaluatePosition(t);
            float dist = Vector3.Distance(player.position, splinePos);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestT = t;
            }
        }

        Vector3 closestPoint = spline.EvaluatePosition(closestT);
        transform.position = closestPoint;
    }
}