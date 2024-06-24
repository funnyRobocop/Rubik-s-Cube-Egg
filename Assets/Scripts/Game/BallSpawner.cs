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
      
        int index;

        public List<Ball> SpawnBalls(int levelNumber)
        {
            index = 0;
            var result = new List<Ball>();
            SpawnBalls(props.levels[levelNumber].forwardBallList, Props.Side.Forwad, ref result);
            SpawnBalls(props.levels[levelNumber].backBallList, Props.Side.Back, ref result);
            SpawnBalls(props.levels[levelNumber].leftBallList, Props.Side.Left, ref result);
            SpawnBalls(props.levels[levelNumber].rightBallList, Props.Side.Right, ref result);

            if (index != 32)
                Debug.LogError("There must be 32 balls! Check level " + levelNumber);

            return result;
        }

        private void SpawnBalls(List<Props.Color> colors, Props.Side side, ref List<Ball> balls)
        {
            foreach (var color in colors)
            {
                var pathCreator = pathList[(int)side];
                var newBallGO = Instantiate(props.GetBallPrefabByColor(color), pathCreator.transform.parent);
                var newBall = newBallGO.GetComponent<Ball>();
                newBall.Init(pathCreator, index * Consts.SideRotStep);
                newBall.name = string.Format("{0}_{1}", index, color.ToString());
                balls.Add(newBall);

                index++;
            }
        }
    }
}
