using System;
using System.Collections.Generic;
using PathCreation.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private const int BallLayerMask = 1 << 6;
    private const int UpLayerMask = 1 << 7;
    private const int MidLayerMask = 1 << 8;
    private const int DownLayerMask = 1 << 9;
    
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Transform cameraRotator;
    [SerializeField]
    private bool lockCameraXZ;
    [SerializeField]
    private EggPartRotator up;
    [SerializeField]
    private EggPartRotator mid;
    [SerializeField]
    private EggPartRotator down;
    [SerializeField]
    private BallsContainer ballsForward;
    [SerializeField]
    private BallsContainer ballsBack;
    [SerializeField]
    private BallsContainer ballsLeft;
    [SerializeField]
    private BallsContainer ballsRight;

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

    public bool IsBallsMove => state is State.Forward or State.Back or State.Left or State.Right;

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
            isUpHit = Physics.Raycast(ray, 100, UpLayerMask);
            isMidHit = Physics.Raycast(ray, 100, MidLayerMask);
            isDownHit = Physics.Raycast(ray, 100, DownLayerMask);
            isBallHit = Physics.Raycast(ray, out var ballHit, 100, BallLayerMask);

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
                MoveBalls(ballsForward, delta, lastBallHit);
                break;
            case State.Back:
                MoveBalls(ballsBack, delta, lastBallHit);
                break;
            case State.Left:
                MoveBalls(ballsLeft, delta, lastBallHit);
                break;
            case State.Right:
                MoveBalls(ballsRight, delta, lastBallHit);
                break;
            case State.Up:
                MoveEggPart(up, delta);
                break;
            case State.Mid:
                MoveEggPart(mid, delta);
                break;
            case State.Down:
                MoveEggPart(down, delta);
                break;
            case State.Camera:
                MoveCamera(cameraRotator, delta);
                break;
            case State.None:         
                break;
            default:
                break;
        }
    }

    private void MoveBalls(BallsContainer balls, Vector3 inputDelta, Transform hit)
    {
        if (hit == null || lastMousePosition == Input.mousePosition)
            return;

        if (balls.CanMove)
            balls.Move(Ball.InputDeltaToDelta(inputDelta, hit));
            
        lastMousePosition = Input.mousePosition;
    }

    private void MoveEggPart(EggPartRotator part, Vector3 delta)
    {
        part.RotateY(-delta.x);
        lastMousePosition = Input.mousePosition;
    }

    private void MoveCamera(Transform cameraParent, Vector3 delta)
    {
        if (lockCameraXZ)
            delta = new Vector3(0f, delta.x, 0f);
        cameraParent.Rotate(delta);
        lastMousePosition = Input.mousePosition;
    }
}
