using System.Collections.Generic;
using UnityEngine;

public class GraphRenderer1 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public RectTransform xAxisRect;
    public RectTransform yAxisRect;
    public int maxPoints = 50;
    private List<Vector3> dataPoints = new List<Vector3>();
    private float changeRate = 1f;
    private float dataValue = 0f;
    private float newYvalue;


    public float xSkillVariable;
    public float yChallengeVariable;

    public bool hasChallengeChanged;



    void Start()
    {


        // Initialize the data points list with the first point at the bottom left corner
        Vector3 firstDataPoint = new Vector3(GetScreenLeftBound(), GetScreenBottomBound(), 0f);
        dataPoints.Add(firstDataPoint);

        UpdateLineRenderer();
        UpdateAxes();
    }

    void Update()
    {
        // Check if the user presses the spacebar to fetch new data
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FetchNewData();
        }

        // Increment the change rate to create the diagonal line
        changeRate += Time.deltaTime;

        // Update the line renderer
        UpdateLineRenderer();
    }

    void FetchNewData()
    {
        // Calculate the position of the new data point based on the existing graph's position
        float newX = NewXData(); // Adjust the X position as needed

        if (hasChallengeChanged)
        {
            newYvalue = NewYData();
        }
        else
        {
            newYvalue = OldYdata();
        }

        // Adjust the Y position as needed less hardcoded
        Vector3 newDataPoint = new Vector3(newX, newYvalue, 0f);
        dataPoints.Add(newDataPoint);

        // Remove the oldest data point if the maximum number of points is reached
        if (dataPoints.Count > maxPoints)
        {
            dataPoints.RemoveAt(0);
        }
    }


    float NewXData()
    {

        Vector3 lastDataPoint = dataPoints[dataPoints.Count - 1];
        float newX = lastDataPoint.x + xSkillVariable;

        return newX;

    }



    float NewYData()
    {
        // Calculate the new position based on the data value

        Vector3 lastDataPoint = dataPoints[dataPoints.Count - 1];
        float newY = lastDataPoint.y + yChallengeVariable;
        //float newY = Random.Range(-5f, 1f);
        return newY;



    }

    float OldYdata()
    {
        Vector3 lastDataPoint = dataPoints[dataPoints.Count - 1];
        float newY = lastDataPoint.y;
        return newY;
    }


    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = dataPoints.Count;
        lineRenderer.SetPositions(dataPoints.ToArray());
    }


    void UpdateAxes()
    {
        // Calculate the minimum and maximum values of the data points
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;
        foreach (Vector3 dataPoint in dataPoints)
        {
            minX = Mathf.Min(minX, dataPoint.x);
            maxX = Mathf.Max(maxX, dataPoint.x);
            minY = Mathf.Min(minY, dataPoint.y);
            maxY = Mathf.Max(maxY, dataPoint.y);
        }

        // Adjust the x-axis position and anchor
        Vector2 xAxisPosition = xAxisRect.anchoredPosition;
        xAxisPosition.y = GetScreenBottomBound();
        xAxisRect.anchoredPosition = xAxisPosition;
        xAxisRect.anchorMin = new Vector2(0f, 0f);
        xAxisRect.anchorMax = new Vector2(1f, 0f);

        // Adjust the y-axis position and anchor
        Vector2 yAxisPosition = yAxisRect.anchoredPosition;
        yAxisPosition.y = GetScreenBottomBound();
        yAxisRect.anchoredPosition = yAxisPosition;
        yAxisRect.anchorMin = new Vector2(0f, 0f);
        yAxisRect.anchorMax = new Vector2(0f, 1f);

        // Adjust the x-axis scale
        Vector2 xAxisScale = xAxisRect.sizeDelta;
        xAxisScale.x = GetScreenRightBound() - GetScreenLeftBound();
        xAxisRect.sizeDelta = xAxisScale;

        // Adjust the y-axis scale
        Vector2 yAxisScale = yAxisRect.sizeDelta;
        yAxisScale.y = GetScreenTopBound() - GetScreenBottomBound();
        yAxisRect.sizeDelta = yAxisScale;
    }

    float GetScreenLeftBound()
    {
        Vector3 bottomLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);
        return bottomLeftCorner.x;
    }

    float GetScreenRightBound()
    {
        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        return topRightCorner.x;
    }

    float GetScreenBottomBound()
    {
        Vector3 bottomLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);
        return bottomLeftCorner.y;
    }

    float GetScreenTopBound()
    {
        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        return topRightCorner.y;
    }

}
