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

        public bool CanMove {get;set;}


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

        public void Move(int delta)
        {
            if (!CanMove)
                return;

            foreach (var item in ballList)
            {
                item.SetGoalPos(delta);
            }

            CanMove = false;
        }
    }
}