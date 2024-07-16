using System.Collections;
using System.Collections.Generic;
using RubiksCubeEgg;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{

    public class UIHandler : MonoBehaviour
    {
        public GameObject winPanel;
        public GameObject startPanel;
        
        public Button nextLevelBtn;
        public Button playBtn;
        public Button restartBtn;
        public Button trainBtn;
        public Button settingsBtn;
        public TextMeshPro levelNumber;
        public TextMeshPro difficult;

        public enum Difficult { easy, medium, hard, extreme, crazy }


        void Awake()
        {
            playBtn.onClick.AddListener(OnPlayClick);
            restartBtn.onClick.AddListener(OnRestartClick);
            trainBtn.onClick.AddListener(OnTrainClick);
            settingsBtn.onClick.AddListener(OnSettinigsClick);
            nextLevelBtn.onClick.AddListener(OnNextClick);

            winPanel.SetActive(false);
            startPanel.SetActive(true);

            UpdateLevelView();
        }

        void OnDestroy()
        {
            playBtn.onClick.RemoveListener(OnPlayClick);
            restartBtn.onClick.RemoveListener(OnRestartClick);
            trainBtn.onClick.RemoveListener(OnTrainClick);
            settingsBtn.onClick.RemoveListener(OnSettinigsClick);
            nextLevelBtn.onClick.RemoveListener(OnNextClick);
        }

        void OnNextClick()
        {
            Main.Instance.CurrentLevel++;
            Main.Instance.Restart();
            startPanel.SetActive(false);
            winPanel.SetActive(false);

            UpdateLevelView();
        }

        void OnPlayClick()
        {
            Main.Instance.Restart();
            startPanel.SetActive(false);
            winPanel.SetActive(false);

            UpdateLevelView();
        }

        void OnRestartClick()
        {
            Main.Instance.Restart();
            startPanel.SetActive(false);
            winPanel.SetActive(false);

            UpdateLevelView();
        }

        void OnTrainClick()
        {
            Main.Instance.Restart();
            startPanel.SetActive(false);
            winPanel.SetActive(false);
        }

        void OnSettinigsClick()
        {
        }

        public void ShowWin()
        {
            startPanel.SetActive(false);
            winPanel.SetActive(true);
            
            UpdateLevelView();
        }

        public void UpdateLevelView()
        {
            var level = Main.Instance.CurrentLevel;

            string difficultText = "Super Extremely Hard";

            if (level <= 80)
                difficultText = "Very Super Hard";
            else if (level <= 60)
                difficultText = "Very Very Hard";
            else if (level <= 40)
                difficultText = "Very Hard";
            else if (level <= 20)
                difficultText = "Hard";
            else if (level <= 5)
                difficultText = "Easy";
            
            difficult.text = difficultText;
            levelNumber.text = level.ToString();
        }
    }
}
