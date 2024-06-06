using UnityEngine;

public class EggPartRotator : MonoBehaviour
{

    private const float STEP_OF_ROTATION = 90f;

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

    
    public void RotateY(float delta)
    {
        aligning = false;
        thisTransform.Rotate(new Vector3(0f, delta, 0f));
    }
    public void StopRotateY()
    {
        aligning = true;
    }
    
    private void RotateYTo(float goal)
    {
        float deltaY = goal - thisTransform.localRotation.eulerAngles.y;
        thisTransform.Rotate(new Vector3(0f, deltaY * Time.deltaTime * speed, 0f));
    }


    private float CalculateGoalY => Mathf.Round(thisTransform.localRotation.eulerAngles.y / STEP_OF_ROTATION) * STEP_OF_ROTATION;
}
