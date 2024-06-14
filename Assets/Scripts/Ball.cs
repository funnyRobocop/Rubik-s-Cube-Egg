using System;
using PathCreation;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float STEP = 0.375f;

    [SerializeField]
    private EndOfPathInstruction endOfPathInstruction;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distanceTravelled;
    [SerializeField]
    private PathCreator pathCreator;

    private float goalDistanceTravelled;
    private int signMoving;

    void Awake()
    {
        goalDistanceTravelled = distanceTravelled;
        enabled = false;
    }

    public static int InputDeltaToDelta(Vector3 inputDelta, Transform tr)
    {
        int delta = 0;

        if (Math.Abs(tr.transform.localPosition.y) > 0.5f)
        {
            var xDelta = Mathf.RoundToInt(inputDelta.x);

            if (xDelta != 0)
            {
                if (tr.transform.localPosition.y > 0)
                    delta = (int)-Mathf.Sign(xDelta);
                else 
                    delta = (int)Mathf.Sign(xDelta);
            }
                
            Debug.LogFormat("go: {0} pos: {1} inputDelta: {2} RoundX: {3}", tr.name, tr.transform.localPosition, inputDelta, xDelta);   
        }
        else
        {
            var yDelta = Mathf.RoundToInt(inputDelta.y);
            
            if (yDelta != 0)
            {
                if (tr.transform.localPosition.z > 0)
                    delta = (int)-Mathf.Sign(yDelta);
                else 
                    delta = (int)Mathf.Sign(yDelta);
            }
                
            Debug.LogFormat("go: {0} pos: {1} inputDelta: {2} RoundY: {3}", tr.name, tr.transform.localPosition, inputDelta, yDelta);   
        }

        return delta;
    }

    void Update()
    {
        if (pathCreator == null || goalDistanceTravelled == distanceTravelled)
        {
            enabled = false;
            return;
        }

        if (Mathf.Abs(goalDistanceTravelled - distanceTravelled) > speed * Time.deltaTime)
        {
            distanceTravelled += speed * Time.deltaTime * signMoving;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }
        else
        {
            transform.position = pathCreator.path.GetPointAtDistance(goalDistanceTravelled, endOfPathInstruction);
            enabled = false;
        }
    }

    public void SetGoalPos(int delta)
    {
        signMoving = delta;
        goalDistanceTravelled += STEP * delta;
        enabled = true;
    }

    public void SetPathCreator(PathCreator pathCreator)
    {
        this.pathCreator = pathCreator;
    }
}
