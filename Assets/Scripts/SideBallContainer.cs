using System;
using System.Collections.Generic;
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

        private Transform thisTransform;

        public bool CanMove {get;set;}

        void Awake()
        {
            thisTransform = GetComponent<Transform>();
        }

        public override void Add(Ball ball)
        {
            base.Add(ball);
            if (!ballList.Contains(ball))
            {
                ballList.Add(ball);
                ball.SetPathCreator(pathCreator);
                ball.tag = tag;
            }
        }

        public override void Remove(Ball ball)
        {
            base.Remove(ball);
            if (ballList.Contains(ball))
                ballList.Remove(ball);
        }

        public void OnRotateStart()
        {
            foreach (var item in ballList)
            {
                item.ThisTransform.SetParent(thisTransform);
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