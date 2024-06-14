using System;
using System.Collections.Generic;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class SegmentBallContainer : BallContainerBase
    {

        public event Action OnAligningFinished;
        
        [SerializeField]
        private List<Ball> ballList;

        private Transform thisTransform;
        private bool needAligning;


        void Awake()
        {
            thisTransform = GetComponent<Transform>();
        }

        void Update()
        {
            if (!needAligning)
                return;

            AlignRotation(CalculateGoalY);

            if (Mathf.Abs(CalculateGoalY - thisTransform.localRotation.eulerAngles.y) < float.Epsilon)
            {
                OnAligningFinished?.Invoke();
            }
        }

        public override void Add(Ball ball)
        {
            base.Add(ball);
            if (!ballList.Contains(ball))
            {
                ballList.Add(ball);
                ball.transform.SetParent(thisTransform);
            }
        }

        public override void Remove(Ball ball)
        {
            base.Remove(ball);
            if (ballList.Contains(ball))
                ballList.Remove(ball);
        }
        
        public void Rotate(Vector3 delta)
        {
            needAligning = false;
            thisTransform.Rotate(new Vector3(0f, -delta.x, 0f));
        }

        public void Stop()
        {
            needAligning = true;
        }
        
        private void AlignRotation(float goalY)
        {
            var deltaY = goalY - thisTransform.localRotation.eulerAngles.y;
            thisTransform.Rotate(new Vector3(0f, deltaY * Time.deltaTime * Consts.SegmentRotSpeed, 0f));
        }

        private float CalculateGoalY => Mathf.Round(thisTransform.localRotation.eulerAngles.y / Consts.SegmentRotStep) * Consts.SegmentRotStep;        
    }
}
