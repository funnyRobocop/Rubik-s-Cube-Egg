using System;
using System.Collections.Generic;
using UI;
using UnityEngine;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {

        public int CurrentLevel = 1;
        public static int ChoosedLevel;
        public int Bg;
        public int Egg;
        public bool Music
        {
            get;
            set;
        }
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
            LoadData();
        }

        private void OnDestroy()
        {
            collisionsChecker.OnWin -= Win;
        }
        
        private void LoadData()
        {
            dataLoader.Load();
            CurrentLevel = dataLoader.PlayerData.level;
            SkippedLevelList = new List<int>(dataLoader.PlayerData.skipped);
            Bg = dataLoader.PlayerData.bg;
            Egg = dataLoader.PlayerData.egg;
            Music = dataLoader.PlayerData.music;

            if (CurrentLevel < 1)
                CurrentLevel = 1;

            LoadLevel(ChoosedLevel);
            CheckRewardAd();

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
                        SkippedLevelList.Remove(ChoosedLevel);
                
                uIHandler.ShowWin();
                SaveData();
#if UNITY_WEBGL
                YG2.SetLeaderboard("level", CurrentLevel - 1 - SkippedLevelList.Count);
#else
                Debug.Log("Todo SetLeaderboard");
#endif   
            }
            
            IsRun = false;
        }

        public void SaveData()
        {
            dataLoader.Save(CurrentLevel, Bg, Egg, Music, SkippedLevelList ?? new List<int>());
        }

        public void CheckRewardAd()
        {
            if (SkippedLevelList.Contains(ChoosedLevel) || ChoosedLevel < CurrentLevel)
                uIHandler.adBtn.SetActive(false);
            else
                uIHandler.adBtn.SetActive(true);
        }

        public void RewardAdvShow()
        {
            if (SkippedLevelList.Contains(ChoosedLevel) || ChoosedLevel < CurrentLevel)
                return;
            
#if UNITY_WEBGL
            YG2.RewardedAdvShow("skip", () =>
            {
                if (!SkippedLevelList.Contains(ChoosedLevel))
                    SkippedLevelList.Add(ChoosedLevel);

                ChoosedLevel = 0;
                CurrentLevel++;
                
                SaveData();

                IsRun = false;

                uIHandler.chooseLvlPanel.SetActive(true);
                uIHandler.LoadChooseLevelPanel();
                uIHandler.startPanel.SetActive(false);
                uIHandler.levelPanel.SetActive(false);
                uIHandler.ShowAllStartBtn(false);
            });
#else
                Debug.Log("Todo RewardedAdvShow");
#endif   
        } 
    }
}
