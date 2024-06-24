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

        private void Awake()
        {
            Application.targetFrameRate = 60;
            collisionsChecker.OnWin += Win;
        }

        private void OnDestroy()
        {
            collisionsChecker.OnWin -= Win;
        }
        
        private void Start()
        {
            var spawnedBalls = ballSpawner.SpawnBalls(CurrentLevel);
            collisionsChecker.Init(spawnedBalls);
        }

        private void Win()
        {
            Debug.Log("Win");
        }
    }
}
