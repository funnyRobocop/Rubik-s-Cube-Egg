using UnityEngine;

public class SegmentBallContainer : MonoBehaviour
{

    private Transform thisTransform;
    private bool aligning;

    public float speed;

    void Awake()
    {
        thisTransform = GetComponent<Transform>();
    }


    void Update()
    {
        if (!aligning)
            return;

        RotateYTo(CalculateGoalY);
    }

    
    public void Rotate(Vector3 delta)
    {
        aligning = false;
        thisTransform.Rotate(new Vector3(0f, -delta.x, 0f));
    }
    public void Stop()
    {
        aligning = true;
    }
    
    private void RotateYTo(float goal)
    {
        float deltaY = goal - thisTransform.localRotation.eulerAngles.y;
        thisTransform.Rotate(new Vector3(0f, deltaY * Time.deltaTime * speed, 0f));
    }


    private float CalculateGoalY => Mathf.Round(thisTransform.localRotation.eulerAngles.y /Consts.SegmentRotStep) * Consts.SegmentRotStep;
}
