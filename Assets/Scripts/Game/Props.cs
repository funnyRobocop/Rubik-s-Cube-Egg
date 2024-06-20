using System.Collections.Generic;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Props", order = 1)]
    public class Props : ScriptableObject
    {
        public List<Level> levels;
        /// <summary>
        /// Blue, Green, Grey, Violet
        /// </summary>
        public List<GameObject> ballPrefabByColorList;

        public enum Side { Forwad, Back, Left, Right }
        public enum Color { Blue, Green, Grey, Violet }

        public GameObject GetBallPrefabByColor(Color color)
        {
            return ballPrefabByColorList[(int) color];
        }
        
        [System.Serializable]
        public class Level
        {
            public List<Ball> ballList;
            
            [System.Serializable]
            public class Ball
            {
                public Side side;
                public Color color;
            };
        }
    }
}
