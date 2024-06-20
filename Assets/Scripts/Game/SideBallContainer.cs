using System.Collections.Generic;
using PathCreation;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class SideBallContainer : BallContainerBase
    {        

        [SerializeField]
        private List<Ball> ballList;
        
        private PathCreator pathCreator;

        public bool CanMove { get; set; }
        public Transform ThisTransform { get; private set; } 


        void Awake()
        {
            ThisTransform = GetComponent<Transform>();
            pathCreator = GetComponent<PathCreator>();
        }

        public override void Add(Ball ball)
        {
            if (!ballList.Contains(ball))
            {
                ballList.Add(ball);
                ball.PathCreator = pathCreator;
                ball.tag = tag;
            }
        }

        public override void Remove(Ball ball)
        {
            if (ballList.Contains(ball))
            {
                ballList.Remove(ball);
            }
        }

        public override void Clear()
        {
            ballList.Clear();
        }

        public void OnRotateStart()
        {
            foreach (var item in ballList)
            {
                item.ThisTransform.SetParent(ThisTransform);
            }
        }

        public void Rotate(int delta)
        {
            foreach (var item in ballList)
            {
                item.InitRotation(delta, this);
            }

            CanMove = false;
        }

        public void OnRotationFinish()
        {
            //Debug.LogFormat("{0} OnRotationFinish", gameObject.name);
            OnRotationFinished?.Invoke();
        }
    }
}