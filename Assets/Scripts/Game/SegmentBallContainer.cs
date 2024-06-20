using System.Collections.Generic;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class SegmentBallContainer : BallContainerBase
    {
        
        [SerializeField]
        private List<Ball> ballList;

        private Transform thisTransform;


        void Awake()
        {
            thisTransform = GetComponent<Transform>();
            enabled = false;
        }

        void Update()
        {
            AlignRotation(CalculateGoalY);

            if (Mathf.Abs(CalculateGoalY - thisTransform.localRotation.eulerAngles.y) < 0.1f)
            {
                OnRotationFinish();
            }
        }

        public override void Add(Ball ball)
        {
            if (!ballList.Contains(ball))
            {
                ballList.Add(ball);
                ball.transform.SetParent(thisTransform);
            }
        }

        public override void Remove(Ball ball)
        {
            if (ballList.Contains(ball))
                ballList.Remove(ball);
        }

        public override void Clear()
        {
            ballList.Clear();
        }

        public void OnRotateStart()
        {
            foreach (var item in ballList)
            {
                item.ThisTransform.SetParent(thisTransform);
            }
        }
        
        public void Rotate(Vector3 delta)
        {
            enabled = false;
            thisTransform.Rotate(new Vector3(0f, -delta.x * Time.deltaTime * Consts.SegmentRotSpeed, 0f));
        }

        public void Stop()
        {
            enabled = true;
        }
        
        private void AlignRotation(float goalY)
        {
            var deltaY = goalY - thisTransform.localRotation.eulerAngles.y;
            thisTransform.Rotate(new Vector3(0f, deltaY * Time.deltaTime * Consts.SegmentRotAligningSpeed, 0f));
        }

        private void OnRotationFinish()
        {
            enabled = false;
            OnRotationFinished?.Invoke();
        }

        private float CalculateGoalY => Mathf.Round(thisTransform.localRotation.eulerAngles.y / Consts.SegmentRotStep) * Consts.SegmentRotStep;        
    }
}
