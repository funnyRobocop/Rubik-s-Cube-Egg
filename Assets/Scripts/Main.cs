using UnityEngine;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {      

        public int CurrentLevel = 0;
        public int CurrentLevelView => CurrentLevel + 1;

        [SerializeField]
        private Game.BallSpawner ballSpawner;
        [SerializeField]
        private Game.CollisionsChecker collisionsChecker;

        
        void Start()
        {
            Application.targetFrameRate = 60;
            
            var spawnedBalls = ballSpawner.SpawnBalls(CurrentLevel);
            collisionsChecker.Init(spawnedBalls);
        }
    }
}
