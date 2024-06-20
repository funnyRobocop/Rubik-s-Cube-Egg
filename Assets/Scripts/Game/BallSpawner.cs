using System.Collections.Generic;
using PathCreation;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class BallSpawner : MonoBehaviour
    {

        [SerializeField]
        private Props props;
        
        [SerializeField]
        private List<PathCreator> pathList;
       

        public void Init(int levelNumber)
        {
            SpawnBalls(levelNumber);
        }

        public void SpawnBalls(int levelNumber)
        {
            var index = 0f;

            foreach (var ballData in props.levels[levelNumber - 1].ballList)
            {
                var pathCreator = pathList[(int)ballData.side];
                var newBallGO = Instantiate(props.GetBallPrefabByColor(ballData.color), pathCreator.transform.parent);
                var newBall = newBallGO.GetComponent<Ball>();
                newBall.Init(pathCreator, index * Consts.SideRotStep);
                newBall.name = string.Format("{0}_{1}", index, ballData.color.ToString());

                index++;
            }
        }
    }
}
