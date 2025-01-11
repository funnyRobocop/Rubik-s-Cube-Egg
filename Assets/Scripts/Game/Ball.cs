using System;
using PathCreation;
using UnityEngine;
using System;


namespace RubiksCubeEgg.Game
{
    public class Ball : MonoBehaviour
    {

        private float distanceTravelled;
        private float goalDistanceTravelled;
        private int direction;
        private float initDistanceTravelled;

        public SideBallContainer SideBallContainer { get; set; }
        public Transform ThisTransform { get; private set; }
        public PathCreator PathCreator { get; set; }
        public Color Color { get; private set; }
        Props props;


        public static int InputDeltaToDirection(Vector3 inputDelta, Transform tr)
        {
            int delta = 0;

            if (Math.Abs(tr.localPosition.y) > 0.5f)
            {
                var xDelta = Mathf.RoundToInt(inputDelta.x);

                if (xDelta != 0f)
                    delta = tr.localPosition.y > 0f ? (int)-Mathf.Sign(xDelta) : (int)Mathf.Sign(xDelta);
                    
                //Debug.LogFormat("go: {0} pos: {1} inputDelta: {2} RoundX: {3}", tr.name, tr.transform.localPosition, inputDelta, xDelta);   
            }
            else
            {
                var yDelta = Mathf.RoundToInt(inputDelta.y);
                
                if (yDelta != 0f)
                    delta = tr.localPosition.z > 0f ? (int)-Mathf.Sign(yDelta) : (int)Mathf.Sign(yDelta);
                    
                //Debug.LogFormat("go: {0} pos: {1} inputDelta: {2} RoundY: {3}", tr.name, tr.transform.localPosition, inputDelta, yDelta);   
            }

            return delta;
        }

        private void Awake()
        {
            ThisTransform = GetComponent<Transform>();
            enabled = false;
            initDistanceTravelled = distanceTravelled;
        }

        private void Update()
        {
            if (Mathf.Abs(goalDistanceTravelled - distanceTravelled) > Consts.SideRotSpeed * Time.deltaTime)
            {
                distanceTravelled += Consts.SideRotSpeed * Time.deltaTime * direction;
                ThisTransform.position = PathCreator.path.GetPointAtDistance(distanceTravelled);
            }
            else
            {
                distanceTravelled = goalDistanceTravelled;
                ThisTransform.position = PathCreator.path.GetPointAtDistance(goalDistanceTravelled);
                OnGoalDistanceTravelledReach();
            }
        }

        public void Init(PathCreator pathCreator, float distanceTravelled, Color color)
        {
            this.distanceTravelled = distanceTravelled;
            PathCreator = pathCreator;
            Color = color;
            goalDistanceTravelled = distanceTravelled;
            ThisTransform.SetLocalPositionAndRotation(PathCreator.path.GetPointAtDistance(distanceTravelled), Quaternion.identity);
        }

        public void ChangeColor(Color color)
        {
            distanceTravelled = initDistanceTravelled;
            Color = color;
            //props.GetBallPrefabByColor(0);
        }

        public void InitRotation(int delta, SideBallContainer sideBallContainer)
        {
            this.SideBallContainer = sideBallContainer;
            direction = delta;
            goalDistanceTravelled += Consts.SideRotStep * delta;
            enabled = true;
        }

        private void OnGoalDistanceTravelledReach()
        {
            enabled = false;
            //ThisTransform.localRotation = Quaternion.identity;

            if (SideBallContainer != null)
                SideBallContainer.OnRotationFinish();
        }
    }
}