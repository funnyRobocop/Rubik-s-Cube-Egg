using System.Runtime.CompilerServices;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class InputController : MonoBehaviour
    {
        
        [SerializeField]
        private CameraController cameraController;
        [SerializeField]
        private SegmentBallContainer upContainer;
        [SerializeField]
        private SegmentBallContainer middleContainer;
        [SerializeField]
        private SegmentBallContainer bottomContainer;
        [SerializeField]
        private SideBallContainer forwardContainer;
        [SerializeField]
        private SideBallContainer backContainer;
        [SerializeField]
        private SideBallContainer leftContainer;
        [SerializeField]
        private SideBallContainer rightContainer;

        private Vector3 lastMousePosition = Vector3.negativeInfinity;
        private Transform lastBallHit;
        private State state;

        private enum State
        {
            None,
            Forward,
            Back,
            Left,
            Right,
            Up,
            Middle,
            Bottom,
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

                if (state == State.Up || state == State.Middle|| state == State.Bottom)
                {
                    upContainer.Stop();
                    middleContainer.Stop();            
                    bottomContainer.Stop();
                }

                forwardContainer.CanMove = true;
                backContainer.CanMove = true;
                leftContainer.CanMove = true;
                rightContainer.CanMove = true;

                lastBallHit = null;
                
                state = State.None;

                return;
            }            

            if (Input.GetMouseButtonDown(0))
            {
                var ray = cameraController.Camera.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray, 100f, Consts.UpLayerMask | Consts.MiddleLayerMask | Consts.BottomLayerMask | Consts.BallLayerMask | Consts.CameraLayerMask);
                Transform closestHit = null;
                float minDistance = float.MaxValue;
                foreach (var item in hits)
                {
                    if (item.distance < minDistance)
                    {
                        minDistance = item.distance;
                        closestHit = item.transform;

                    } 
                }

                if (closestHit != null)
                {
                    if (closestHit.gameObject.layer == Consts.BallLayer)
                    {
                        lastBallHit = closestHit.parent;

                        if (lastBallHit.CompareTag(Consts.ForwardTag))
                            state = State.Forward;
                        else if (lastBallHit.CompareTag(Consts.BackTag))
                            state = State.Back;
                        else if (lastBallHit.CompareTag(Consts.LeftTag))
                            state = State.Left;
                        else if (lastBallHit.CompareTag(Consts.RightTag))
                            state = State.Right;
                    }
                    else if (closestHit.gameObject.layer == Consts.UpLayer)
                    {
                        state = State.Up;
                    }
                    else if (closestHit.gameObject.layer == Consts.MiddleLayer)
                    {
                        state = State.Middle;
                    }
                    else if (closestHit.gameObject.layer == Consts.BottomLayer)
                    {
                        state = State.Bottom;
                    }
                    else
                    {
                        state = State.Camera;
                    }
                }                
            }

            var delta = Input.mousePosition - lastMousePosition;
            switch (state)
            {
                case State.Forward:
                    RotateSideContainer(forwardContainer, delta, lastBallHit);
                    break;
                case State.Back:
                    RotateSideContainer(backContainer, delta, lastBallHit);
                    break;
                case State.Left:
                    RotateSideContainer(leftContainer, delta, lastBallHit);
                    break;
                case State.Right:
                    RotateSideContainer(rightContainer, delta, lastBallHit);
                    break;
                case State.Up:
                    RotateSegmentContainer(upContainer, delta);
                    break;
                case State.Middle:
                    RotateSegmentContainer(middleContainer, delta);
                    break;
                case State.Bottom:
                    RotateSegmentContainer(bottomContainer, delta);
                    break;
                case State.Camera:
                    RotateCamera(delta);
                    break;
                case State.None:         
                    break;
                default:
                    break;
            }
        }

        private void RotateSideContainer(SideBallContainer container, Vector3 inputDelta, Transform hit)
        {
            if (hit == null || lastMousePosition == Input.mousePosition || !container.CanMove)
                return;

            container.OnRotateStart();
            container.Rotate(Ball.InputDeltaToDirection(inputDelta, hit));
            lastMousePosition = Input.mousePosition;             
        }

        private void RotateSegmentContainer(SegmentBallContainer container, Vector3 inputDelta)
        {
            if (lastMousePosition == Input.mousePosition)
                return;

            container.OnRotateStart();
            container.Rotate(inputDelta);
            lastMousePosition = Input.mousePosition;
        }

        private void RotateCamera( Vector3 inputDelta)
        {
            cameraController.Rotate(inputDelta);
            lastMousePosition = Input.mousePosition;
        }
    }
}
