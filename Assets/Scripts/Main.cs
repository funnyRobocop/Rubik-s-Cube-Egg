using System.Runtime.InteropServices;
using UI;
using UnityEngine;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {      

        public int CurrentLevel = 0;

        public static Main Instance;

        [SerializeField]
        private Game.BallSpawner ballSpawner;
        [SerializeField]
        private Game.CollisionsChecker collisionsChecker;

        public UIHandler uIHandler;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            collisionsChecker.OnWin += Win;
            Instance = this;
        }

        private void OnDestroy()
        {
            collisionsChecker.OnWin -= Win;
        }
        
        private void Start()
        {
            Restart();
        }

        public void Restart()
        {
            var spawnedBalls = ballSpawner.SpawnBalls(CurrentLevel);
            collisionsChecker.Init(spawnedBalls);
        }

        private void Win()
        {
            Debug.Log("Win");
            uIHandler.ShowWin();
        }
    }
}
