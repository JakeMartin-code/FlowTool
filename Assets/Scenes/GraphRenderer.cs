using System.Collections.Generic;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int maxPoints = 50;
    private List<Vector3> dataPoints = new List<Vector3>();
    private float dataValue = 0f;
    private float newYvalue;


    //adjust these two variables to determine how much of a jump each data point is
    public float xSkillVariable;
    public float yChallengeVariable;

    public bool hasChallengeChanged;



    void Start()
    {
 

        Vector3 firstDataPoint = new Vector3(GetScreenLeftBound(), GetScreenBottomBound(), 0f);
        dataPoints.Add(firstDataPoint);

        UpdateLineRenderer();
        UpdateAxes();
    }

    void Update()
    {
        // This is a test function on getting new data, you should insert you own logic to call this function
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FetchNewData();
        }

        // Increment the change rate to create the diagonal line
     //   changeRate += Time.deltaTime;

 
        UpdateLineRenderer();
    }

    void FetchNewData()
    {
       
        float newX = NewXData(); 

        if (hasChallengeChanged)
        {
            newYvalue = NewYData();
        }
        else
        {
            newYvalue = OldYdata();
        }


        Vector3 newDataPoint = new Vector3(newX, newYvalue, 0f);
        dataPoints.Add(newDataPoint);

      
        if (dataPoints.Count > maxPoints)
        {
            dataPoints.RemoveAt(0);
        }
    }


    
    float NewYData()
    {
        Vector3 lastDataPoint = dataPoints[dataPoints.Count - 1];
        float newY = lastDataPoint.y + yChallengeVariable;
        return newY; 
    }

    float OldYdata()
    {
        Vector3 lastDataPoint = dataPoints[dataPoints.Count - 1];
        float newY = lastDataPoint.y;
        return newY;
    }

    float NewXData()
    {
      
        Vector3 lastDataPoint = dataPoints[dataPoints.Count - 1];
        float newX = lastDataPoint.x + xSkillVariable;

        return newX; 

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
