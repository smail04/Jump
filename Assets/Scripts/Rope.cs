using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    public InputPanel inputPanel;
    public float maxLength;
    public Vector3 endPosition;
    private LineRenderer line;
    private bool visible;
    public bool Visible { 
        get => visible; 
        set 
        { 
            visible = value; 
            line.enabled = visible; 
        } 
    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        Visible = false;
    }

    public void SetPositions(Vector2 start, Vector2 end)
    {
        line.SetPosition(0, start);
        if (Vector3.Distance(start, end) > maxLength)
        {
            Ray ray = new Ray(start, (end - start).normalized);
            endPosition = ray.GetPoint(maxLength);
        }
        else
            endPosition = end;

        line.SetPosition(1, endPosition);
    }

    public void SetPositions()
    {
        SetPositions(inputPanel.startPosition, inputPanel.currentPosition);
    }
}
