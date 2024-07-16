using System.Collections;
using System.Collections.Generic;
using RubiksCubeEgg;
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




        void Awake()
        {
            playBtn.onClick.AddListener(OnPlayClick);
            restartBtn.onClick.AddListener(OnRestartClick);
            trainBtn.onClick.AddListener(OnTrainClick);
            settingsBtn.onClick.AddListener(OnSettinigsClick);
            nextLevelBtn.onClick.AddListener(OnNextClick);

            winPanel.SetActive(false);
            startPanel.SetActive(true);
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
        }

        void OnPlayClick()
        {
            Main.Instance.Restart();
            startPanel.SetActive(false);
            winPanel.SetActive(false);
        }

        void OnRestartClick()
        {
            Main.Instance.Restart();
            startPanel.SetActive(false);
            winPanel.SetActive(false);
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
        }
    }
}
