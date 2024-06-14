using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class InputController : MonoBehaviour
    {
        
        [SerializeField]
        private new Camera camera;
        [SerializeField]
        private Transform cameraRotator;
        [SerializeField]
        private bool lockCameraXZ;

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
                
                state = State.None;

                upContainer.Stop();
                middleContainer.Stop();            
                bottomContainer.Stop();
                forwardContainer.CanMove = true;
                backContainer.CanMove = true;
                leftContainer.CanMove = true;
                rightContainer.CanMove = true;

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
                isMidHit = Physics.Raycast(ray, 100, Consts.MiddleLayerMask);
                isDownHit = Physics.Raycast(ray, 100, Consts.BottomLayerMask);
                isBallHit = Physics.Raycast(ray, out var ballHit, 100, Consts.BallLayerMask);

                if (isBallHit)
                {
                    lastBallHit = ballHit.transform.parent;

                    if (lastBallHit.CompareTag(Consts.ForwardTag))
                        state = State.Forward;
                    else if (lastBallHit.CompareTag(Consts.BackTag))
                        state = State.Back;
                    else if (lastBallHit.CompareTag(Consts.LeftTag))
                        state = State.Left;
                    else if (lastBallHit.CompareTag(Consts.RightTag))
                        state = State.Right;
                }
                else if (isUpHit)
                {
                    state = State.Up;
                }
                else if (isMidHit)
                {
                    state = State.Middle;
                }
                else if (isDownHit)
                {
                    state = State.Bottom;
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
                    RotateCamera(cameraRotator, delta);
                    break;
                case State.None:         
                    break;
                default:
                    break;
            }
        }

        private void RotateSideContainer(SideBallContainer container, Vector3 inputDelta, Transform hit)
        {
            if (hit == null || lastMousePosition == Input.mousePosition)
                return;

            if (container.CanMove)
                container.Move(Ball.InputDeltaToDirection(inputDelta, hit));
                
            lastMousePosition = Input.mousePosition;
        }

        private void RotateSegmentContainer(SegmentBallContainer container, Vector3 inputDelta)
        {
            container.Rotate(inputDelta);
            lastMousePosition = Input.mousePosition;
        }

        private void RotateCamera(Transform cameraParent, Vector3 inputDelta)
        {
            if (lockCameraXZ)
                inputDelta = new Vector3(0f, inputDelta.x, 0f);

            cameraParent.Rotate(inputDelta);
            lastMousePosition = Input.mousePosition;
        }
    }
}
