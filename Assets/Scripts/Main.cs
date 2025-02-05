using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using YG;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {

        public int CurrentLevel = 1;
        public static int ChoosedLevel;
        public int Bg;
        public int Egg;
        public bool Music { get; set; } = true;

        public List<int> SkippedLevelList = new();

        public static Main Instance;

        private DataLoader dataLoader;
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
            dataLoader = FindFirstObjectByType<DataLoader>();   
        }

        void Start()
        {
#if UNITY_WEBGL
            //if (YandexGame.SDKEnabled)
            //    LoadData();

            //YandexGame.GetDataEvent += LoadData;
#else
            LoadData();
#endif 
        }

        private void OnDestroy()
        {
            collisionsChecker.OnWin -= Win;
#if UNITY_WEBGL
            //YandexGame.GetDataEvent -= LoadData;
#endif 
        }
        
        private void LoadData()
        {
            dataLoader.Load();
            CurrentLevel = dataLoader.PlayerData.level;
            Bg = dataLoader.PlayerData.bg;
            Egg = dataLoader.PlayerData.egg;
            Music = dataLoader.PlayerData.music;

            if (CurrentLevel < 1)
                CurrentLevel = 1;

            LoadLevel(ChoosedLevel);

            uIHandler.LoadSettings(Bg, Egg);

            if (ChoosedLevel <= 0)
                uIHandler.LoadMusic(Music);
        }

        public void LoadLevel(int level)
        {
            IsRun = level > 0;
            var spawnedBalls = ballSpawner.SpawnBalls(level);
            collisionsChecker.Init(spawnedBalls);
        }

        private void Win()
        {
            if (IsRun)
            {
                if (ChoosedLevel == CurrentLevel)
                    CurrentLevel++;
                else
                    if (SkippedLevelList.Contains(ChoosedLevel))
                        SkippedLevelList.Remove(ChoosedLevel); //todo проверить
                
                uIHandler.ShowWin();
                SaveData();
            }
            
            IsRun = false;
        }

        public void SaveData()
        {
            dataLoader.Save(CurrentLevel, Bg, Egg, Music);
        }
    }
}
