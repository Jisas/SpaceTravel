using System.Collections;
using UnityEngine;

public class UILinesManager : MonoBehaviour
{
    [SerializeField] private Transform pointsParent;
    [SerializeField] private bool animateLine;
    [SerializeField] private float animDuration;

    public delegate void OnFinishAnimationAction();
    public static event OnFinishAnimationAction OnFinishAnimation;

    private LineRenderer lineRenderer;
    private Vector3[] linePoints;
    private int pointsCount;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = pointsParent.childCount;
        pointsCount = lineRenderer.positionCount;
        linePoints = new Vector3[pointsCount];

        SetPointsPosition();
    }

    public static void ResetEvents()
    {
        OnFinishAnimation = null;
    }

    private void SetPointsPosition()
    {
        if (lineRenderer != null && lineRenderer.positionCount > 0)
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                var position = pointsParent.GetChild(i).localPosition;
                lineRenderer.SetPosition(i, position);
                linePoints[i] = lineRenderer.GetPosition(i);
            }

            if (animateLine) StartCoroutine(AnimateLine());
        }
    }
    public IEnumerator AnimateLine()
    {
        float segmentDuration = animDuration / pointsCount;

        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;
            Vector3 startPos = linePoints[i];
            Vector3 endPos = linePoints[i + 1];
            Vector3 currentPos = startPos;

            while (currentPos != endPos)
            {

                float t = (Time.time - startTime) / segmentDuration;
                currentPos = Vector3.Lerp(startPos, endPos, t);

                for (int j = i+1; j < pointsCount; j++)
                    lineRenderer.SetPosition(j, currentPos);

                yield return null;
            }
        }

        OnFinishAnimation.Invoke();
    }
}