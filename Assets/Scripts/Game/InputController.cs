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
        [SerializeField]
        private GameObject uiCurtain;


        private State state;
        private Vector3 lastMousePosition = Vector3.negativeInfinity;
        private Transform lastBallHit;
        private int lastBallHitFrameCounter;

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
            if (!IsMouseOverGameWindow)
            {   
                lastMousePosition = Vector3.negativeInfinity;
                return;
            }

            if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            {             

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

                if (uiCurtain.activeInHierarchy)
                    return;

                Transform closestHit = null;
                var minDistance = float.MaxValue;
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
                        lastBallHitFrameCounter = 0;
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
                    RotateSideContainer(forwardContainer, delta);
                    break;
                case State.Back:
                    RotateSideContainer(backContainer, delta);
                    break;
                case State.Left:
                    RotateSideContainer(leftContainer, delta);
                    break;
                case State.Right:
                    RotateSideContainer(rightContainer, delta);
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
            }
            
            lastMousePosition = Input.mousePosition;   
        }

        private void RotateSideContainer(SideBallContainer container, Vector3 inputDelta)
        {
            if (lastBallHit == null || lastMousePosition == Input.mousePosition || !container.CanMove)
                return;

            lastBallHitFrameCounter++;
            if (lastBallHitFrameCounter < 10)
                return;

            container.OnRotateStart();
            container.Rotate(Ball.InputDeltaToDirection(inputDelta, lastBallHit));
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
            if (!IsMouseOverGameWindow)
            return;
            cameraController.Rotate(inputDelta);
            lastMousePosition = Input.mousePosition;
        }

        private bool IsMouseOverGameWindow
        {
            get
            {
                Vector3 mp = Input.mousePosition;
                return !( 0>mp.x || 0>mp.y || Screen.width<mp.x || Screen.height<mp.y );
            }
        }
    }
}
