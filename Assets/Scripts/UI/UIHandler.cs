using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public GameObject levelPanel;
        public GameObject trainPanel;
        public GameObject settingsPanel;
        public SpriteRenderer backSprite;
        public Material[] eggMaterial;

        private Color choosedBackColor;
        private Color choosedEggColor;

        
        public Button[] backColorBtn;
        public Button[] eggColorBtn;
        
        public Button nextLevelBtn;
        public Button playBtn;
        public Button restartBtn;
        public Button trainBtn;
        public Button settingsBtn;
        public TextMeshProUGUI levelNumber;
        public TextMeshProUGUI difficult;


        void Awake()
        {
            playBtn.onClick.AddListener(OnPlayClick);
            restartBtn.onClick.AddListener(OnRestartClick);
            trainBtn.onClick.AddListener(OnTrainClick);
            settingsBtn.onClick.AddListener(OnSettinigsClick);
            nextLevelBtn.onClick.AddListener(OnNextClick);

            winPanel.SetActive(false);
            startPanel.SetActive(true);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);

            for (int i = 0; i < backColorBtn.Length; i++)
            {
                Button item = backColorBtn[i];
                item.onClick.AddListener(() => SetBackColor(i));
            }

            foreach (var item in eggColorBtn)
                item.onClick.AddListener(SetEggColor);

            UpdateLevelView();
        }

        void OnDestroy()
        {
            playBtn.onClick.RemoveListener(OnPlayClick);
            restartBtn.onClick.RemoveListener(OnRestartClick);
            trainBtn.onClick.RemoveListener(OnTrainClick);
            settingsBtn.onClick.RemoveListener(OnSettinigsClick);
            nextLevelBtn.onClick.RemoveListener(OnNextClick);
            
            foreach (var item in backColorBtn)
                item.onClick.RemoveAllListeners();              
            foreach (var item in eggColorBtn)
                item.onClick.RemoveAllListeners(); 
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

            trainPanel.SetActive(true);
            trainPanel.transform.GetChild(0).gameObject.SetActive(true);//curtain
            trainPanel.transform.GetChild(1).gameObject.SetActive(true);
            trainPanel.transform.GetChild(2).gameObject.SetActive(false);
            trainPanel.transform.GetChild(3).gameObject.SetActive(false);
            trainPanel.transform.GetChild(4).gameObject.SetActive(false);
        }

        void OnSettinigsClick()
        {
            settingsPanel.SetActive(true);
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

            string difficultText = "Extreme";

            if (level <= 80)
                difficultText = "Extreme";
            else if (level <= 60)
                difficultText = "Super";
            else if (level <= 40)
                difficultText = "Very";
            else if (level <= 20)
                difficultText = "Hard";
            else if (level <= 5)
                difficultText = "Easy";
            
            difficult.text = difficultText;
            levelNumber.text = "level " + level.ToString();
        }

        public void SetBackColor(int i)
        {
            choosedBackColor = backColorBtn[i].image.color;
            backSprite.color = choosedBackColor;
        }

        public void SetEggColor()
        {
            foreach(var item in eggMaterial)
                item.color = choosedEggColor;
        }
    }
}
