using UnityEngine;

public class InputController : MonoBehaviour
{
    
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Transform cameraRotator;
    [SerializeField]
    private bool lockCameraXZ;
    [SerializeField]
    private SegmentBallContainer up;
    [SerializeField]
    private SegmentBallContainer mid;
    [SerializeField]
    private SegmentBallContainer down;
    [SerializeField]
    private SideBallContainer ballsForward;
    [SerializeField]
    private SideBallContainer ballsBack;
    [SerializeField]
    private SideBallContainer ballsLeft;
    [SerializeField]
    private SideBallContainer ballsRight;

    private Vector3 lastMousePosition = Vector3.negativeInfinity;
    private Transform lastBallHit;
    private State state = State.None;

    private enum State
    {
        None,
        Forward,
        Back,
        Left,
        Right,
        Up,
        Mid,
        Down,
        Camera
    }

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            lastMousePosition = Input.mousePosition;
            
            state = State.None;

            up.StopRotateY();
            mid.StopRotateY();            
            down.StopRotateY();
            ballsForward.CanMove = true;
            ballsBack.CanMove = true;
            ballsLeft.CanMove = true;
            ballsRight.CanMove = true;

            lastBallHit = null;
            
            return;
        }
        
        bool isUpHit;
        bool isMidHit;
        bool isDownHit;
        bool isBallHit;
        
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            isUpHit = Physics.Raycast(ray, 100, Consts.UpLayerMask);
            isMidHit = Physics.Raycast(ray, 100, Consts.MidLayerMask);
            isDownHit = Physics.Raycast(ray, 100, Consts.DownLayerMask);
            isBallHit = Physics.Raycast(ray, out var ballHit, 100, Consts.BallLayerMask);

            if (isBallHit)
            {
                lastBallHit = ballHit.transform.parent;

                if (lastBallHit.CompareTag("Forward"))
                    state = State.Forward;
                else if (lastBallHit.CompareTag("Back"))
                    state = State.Back;
                else if (lastBallHit.CompareTag("Left"))
                    state = State.Left;
                else if (lastBallHit.CompareTag("Right"))
                    state = State.Right;
            }
            else if (isUpHit)
            {
                state = State.Up;
            }
            else if (isMidHit)
            {
                state = State.Mid;
            }
            else if (isDownHit)
            {
                state = State.Down;
            }
            else
            {
                state = State.Camera;
            }
        }

        var delta = Input.mousePosition - lastMousePosition;
        switch (state)
        {
            case State.Forward:
                RotateBalls(ballsForward, delta, lastBallHit);
                break;
            case State.Back:
                RotateBalls(ballsBack, delta, lastBallHit);
                break;
            case State.Left:
                RotateBalls(ballsLeft, delta, lastBallHit);
                break;
            case State.Right:
                RotateBalls(ballsRight, delta, lastBallHit);
                break;
            case State.Up:
                RotateEggPart(up, delta);
                break;
            case State.Mid:
                RotateEggPart(mid, delta);
                break;
            case State.Down:
                RotateEggPart(down, delta);
                break;
            case State.Camera:
                RotateCamera(cameraRotator, delta);
                break;
            case State.None:         
                break;
            default:
                break;
        }
    }

    private void RotateBalls(SideBallContainer container, Vector3 inputDelta, Transform hit)
    {
        if (hit == null || lastMousePosition == Input.mousePosition)
            return;

        if (container.CanMove)
            container.Move(Ball.InputDeltaToDirection(inputDelta, hit));
            
        lastMousePosition = Input.mousePosition;
    }

    private void RotateEggPart(SegmentBallContainer container, Vector3 inputDelta)
    {
        container.Rotate(inputDelta);
        lastMousePosition = Input.mousePosition;
    }

    private void RotateCamera(Transform cameraParent, Vector3 delta)
    {
        if (lockCameraXZ)
            delta = new Vector3(0f, delta.x, 0f);
        cameraParent.Rotate(delta);
        lastMousePosition = Input.mousePosition;
    }
}
