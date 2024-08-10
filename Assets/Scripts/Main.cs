using System.Collections.Generic;
using UI;
using UnityEngine;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {      

        public int CurrentLevel = 0;
        public int ChoosedLevel = 0;
        public List<int> SkippedLevelList = new();

        public static Main Instance;

        [SerializeField]
        private Game.BallSpawner ballSpawner;
        [SerializeField]
        private Game.CollisionsChecker collisionsChecker;
        [SerializeField]
        private UIHandler uIHandler;

        public bool IsRun;

        void Awake()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            collisionsChecker.OnWin += Win;
            Instance = this;            
        }
        
        void Start()
        {
            LoadLevel(0);
        }

        private void OnDestroy()
        {
            collisionsChecker.OnWin -= Win;
        }

        public void LoadLevel(int level)
        {
            ChoosedLevel = level;
            IsRun = ChoosedLevel > 0;
            var spawnedBalls = ballSpawner.SpawnBalls(level);
            collisionsChecker.Init(spawnedBalls);
            uIHandler.chooseLvlPanel.SetActive(false);
        }

        private void Win()
        {
            Debug.Log("Win");
            uIHandler.ShowWin();
            IsRun = false;
        }
    }
}
