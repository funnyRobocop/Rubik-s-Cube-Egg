using System;
using System.Collections.Generic;
using PathCreation.Examples;
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
    private bool lockXZ;
    [SerializeField]
    private EggPartRotator up;
    [SerializeField]
    private EggPartRotator mid;
    [SerializeField]
    private EggPartRotator down;
    [SerializeField]
    private List<PathFollower> ballListForward;
    [SerializeField]
    private List<PathFollower> ballListBack;
    [SerializeField]
    private List<PathFollower> ballListLeft;
    [SerializeField]
    private List<PathFollower> ballListRight;

    private Vector3 lastMousePosition = Vector3.negativeInfinity;
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

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            lastMousePosition = Input.mousePosition;
            
            foreach (var item in ballListForward)
                item.speed = 0f;
            foreach (var item in ballListBack)
                item.speed = 0f;
            foreach (var item in ballListLeft)
                item.speed = 0f;
            foreach (var item in ballListRight)
                item.speed = 0f;

            state = State.None;
            
            up.StopRotateY();
            mid.StopRotateY();            
            down.StopRotateY();   
            
            return;
        }
        
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var delta = Input.mousePosition - lastMousePosition;
        
        var isBallHit = Physics.Raycast(ray, out var ballHit, 100, BallLayerMask);
        var isUpHit = Physics.Raycast(ray, out var upHit, 100, UpLayerMask);
        var isMidHit = Physics.Raycast(ray, out var midHit, 100, MidLayerMask);
        var isDownHit = Physics.Raycast(ray, out var downHit, 100, DownLayerMask);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (isBallHit)
            {
                if (ballHit.transform.CompareTag("Forward"))
                    state = State.Forward;
                else if (ballHit.transform.CompareTag("Back"))
                    state = State.Back;
                else if (ballHit.transform.CompareTag("Left"))
                    state = State.Left;
                else if (ballHit.transform.CompareTag("Right"))
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

        switch (state)
        {
            case State.Forward:
                MoveBalls(ballListForward, delta);
                break;
            case State.Back:
                MoveBalls(ballListBack, delta);
                break;
            case State.Left:
                MoveBalls(ballListLeft, delta);
                break;
            case State.Right:
                MoveBalls(ballListRight, delta);
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

    private void MoveBalls(List<PathFollower> pathFollowerList, Vector3 rotation)
    {
        foreach (var item in pathFollowerList)
        {
            item.speed = -rotation.y  * 0.1f;
        }
        lastMousePosition = Input.mousePosition;
    }

    private void MoveEggPart(EggPartRotator part, Vector3 rotation)
    {
        part.enabled = true;
        part.RotateY(-rotation.x);
        lastMousePosition = Input.mousePosition;
    }

    private void MoveCamera(Transform cameraParent, Vector3 rotation)
    {
        if (lockXZ)
            rotation = new Vector3(0f, rotation.x, 0f);
        cameraParent.Rotate(rotation);
        lastMousePosition = Input.mousePosition;
    }
}
