using System;
using PathCreation;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class Ball : MonoBehaviour
    {

        [SerializeField]
        private EndOfPathInstruction endOfPathInstruction;
        [SerializeField]
        private float distanceTravelled;
        [SerializeField]
        private PathCreator pathCreator;

        private float goalDistanceTravelled;
        private int direction;

        public Transform ThisTransform { get; private set; }

        void Awake()
        {
            ThisTransform = GetComponent<Transform>();
            goalDistanceTravelled = distanceTravelled;
            enabled = false;
        }

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

        private void Update()
        {
            if (Mathf.Abs(goalDistanceTravelled - distanceTravelled) > Consts.SideRotSpeed * Time.deltaTime)
            {
                distanceTravelled += Consts.SideRotSpeed * Time.deltaTime * direction;
                ThisTransform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            }
            else
            {
                distanceTravelled += Consts.SideRotSpeed * Time.deltaTime * direction;
                ThisTransform.position = pathCreator.path.GetPointAtDistance(goalDistanceTravelled, endOfPathInstruction);
                OnGoalDistanceTravelledReach();
            }
        }

        public void SetGoalPos(int delta)
        {
            direction = delta;
            goalDistanceTravelled += Consts.SideRotStep * delta;
            enabled = true;
        }

        public void SetPathCreator(PathCreator pathCreator)
        {
            this.pathCreator = pathCreator;
        }

        private void OnGoalDistanceTravelledReach()
        {
            enabled = false;
            ThisTransform.localRotation = Quaternion.identity;
        }
    }
}