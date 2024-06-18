using System;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class SideBallContainer : BallContainerBase
    {        

        [SerializeField]
        private List<Ball> ballList;
        [SerializeField]
        private PathCreator pathCreator;

        public bool CanMove { get; set; }
        public Transform ThisTransform { get; private set; } 


        void Awake()
        {
            ThisTransform = GetComponent<Transform>();
        }

        public override void Add(Ball ball)
        {
            base.Add(ball);
            if (ThisTransform.GetComponentsInChildren<Ball>().FirstOrDefault(f => f == ball) == null)
            {
                if (!ballList.Contains(ball))
                    ballList.Add(ball);
                ball.SetPathCreator(pathCreator);
                ball.tag = tag;
                ball.SideBallContainer = this;
            }
        }

        public override void Remove(Ball ball)
        {
            base.Remove(ball);
            if (ballList.Contains(ball))
            {
                ballList.Remove(ball);
                ball.SideBallContainer = null;
            }
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
                item.SetGoalPos(delta);
            }

            CanMove = false;
        } 
    }
}