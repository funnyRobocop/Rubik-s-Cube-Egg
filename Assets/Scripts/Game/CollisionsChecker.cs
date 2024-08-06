using System.Collections.Generic;
using UnityEngine;
using System;


namespace RubiksCubeEgg.Game
{
    public class CollisionsChecker : MonoBehaviour
    {

        public event Action OnWin;

        [SerializeField]
        private List<BallContainerBase> ballContainers;

        private List<Ball> ballList = new();
        private bool isRun;

        public enum ContainerType { Up, Middle, Bottom, Forward, Back, Left, Right }


        private void Awake()
        {
            foreach (var item in ballContainers)
            {
                item.OnRotationStarted += Stop;
                item.OnRotationFinished += Run;
            }
        }

        private void OnDestroy()
        {
            foreach (var item in ballContainers)
            {
                item.OnRotationStarted -= Stop;
                item.OnRotationFinished -= Run;
            }
        }

        public void Init(List<Ball> balls)
        {
            ballList.AddRange(balls);
            Run();
        }

        private void Run()
        {
            /*if (isRun)
                return;*/

            isRun = true;

            foreach (var item in ballContainers)
                item.Clear();

            foreach (var ball in ballList)
            {
                if (ball.ThisTransform.position.y > 0.25f)
                {
                    ballContainers[(int)ContainerType.Up].Add(ball);
                }
                else if (ball.ThisTransform.position.y < -0.25f)
                {
                    ballContainers[(int)ContainerType.Bottom].Add(ball);
                }
                else
                {
                    ballContainers[(int)ContainerType.Middle].Add(ball);
                }

                if (ball.ThisTransform.position.x < -0.55f)
                {
                    ballContainers[(int)ContainerType.Forward].Add(ball);
                }
                else if (ball.ThisTransform.position.x > 0.55f)
                {
                    ballContainers[(int)ContainerType.Back].Add(ball);
                }
                else if (ball.ThisTransform.position.z > 0.55f)
                {
                    ballContainers[(int)ContainerType.Left].Add(ball);
                }
                else
                {
                    ballContainers[(int)ContainerType.Right].Add(ball);
                }
            }

            Debug.Log("Check is finished");

            if (CheckWinCondition() && OnWin!= null)
                OnWin();
        }

        private void Stop()
        {
            isRun = false;
        }

        private bool CheckWinCondition()
        {
            return CheckColorIsEqual(ballContainers[(int)ContainerType.Forward].Balls) &&
                CheckColorIsEqual(ballContainers[(int)ContainerType.Back].Balls) &&
                CheckColorIsEqual(ballContainers[(int)ContainerType.Left].Balls) &&
                CheckColorIsEqual(ballContainers[(int)ContainerType.Right].Balls);
        }

        private bool CheckColorIsEqual(List<Ball> ballList)
        {
            if (ballList.Count != Consts.SideBallCount)
            {
                Debug.LogError("elements count not correct" + ballList.Count) ;
                return false;
            }

            var color = ballList[0].Color;
            foreach (var item in ballList)
            {
                if (item.Color != color)
                return false;
            }

            return true;
        }
    }
}
