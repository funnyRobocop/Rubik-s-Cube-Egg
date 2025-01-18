using System.Collections.Generic;
using UI;
using UnityEngine;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {      

        public static int CurrentLevel = 1;
        public static int ChoosedLevel;
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
            LoadLevel(ChoosedLevel);
        }

        private void OnDestroy()
        {
            collisionsChecker.OnWin -= Win;
        }

        public void LoadLevel(int level)
        {
            IsRun = level > 0;
            var spawnedBalls = ballSpawner.SpawnBalls(level);
            collisionsChecker.Init(spawnedBalls);
        }

        private void Win()
        {
            Debug.Log("Win");

            if (IsRun)
            {
                if (ChoosedLevel == CurrentLevel)
                    CurrentLevel++;
                else
                    if (SkippedLevelList.Contains(ChoosedLevel))
                        SkippedLevelList.Remove(ChoosedLevel); //todo проверить
                
                uIHandler.ShowWin();
            }
            
            IsRun = false;
        }
    }
}
