using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class CollisionsChecker : MonoBehaviour
    {
        [SerializeField]
        private List<BallContainerBase> ballContainers;

        private List<Ball> ballList = new List<Ball>();

        public enum ContainerType { Up, Middle, Bottom, Forward, Back, Left, Right }


        private void Awake()
        {
            foreach (var item in ballContainers)
                item.OnRotationFinished += Run;
        }

        private void OnDestroy()
        {
            foreach (var item in ballContainers)
                item.OnRotationFinished -= Run;
        }

        public void Init(List<Ball> balls)
        {
            ballList.AddRange(balls);
            Run();
        }

        public void Run()
        {
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
        }
    }
}
