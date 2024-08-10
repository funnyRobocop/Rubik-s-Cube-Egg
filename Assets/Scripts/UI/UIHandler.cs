using System.Collections.Generic;
using RubiksCubeEgg;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{

    public class UIHandler : MonoBehaviour
    {
        public GameObject curtain;

        public GameObject levelPanel;
        public GameObject startPanel;
        public GameObject trainPanel;
        public GameObject settingsPanel;
        public GameObject winPanel;
        public GameObject chooseLvlPanel;
        public GameObject chooseLvlItemPrefab;
        public GameObject chooseLvlItemParent;
        public List<ChooseLevelItem> levelItems = new();

        public SpriteRenderer backSprite;
        public Material[] eggMaterial;
        private Color choosedBackColor;
        private Button choosedBackColorBtn;
        private Color choosedEggColor;
        private Button choosedEggColorBtn;        
        public Button[] backColorBtn;
        public Button[] eggColorBtn;
        
        public Button nextLevelBtn;
        public Button playBtn;
        public Button restartBtn;
        public Button trainBtn;
        public Button settingsBtn;
        public TextMeshProUGUI levelNumber;
        public TextMeshProUGUI difficult;
        public Button leftLevelBtn;
        public Button rightLevelBtn;
        public Button updateBtn;

        public RubiksCubeEgg.Game.Props props;


        void Awake()
        {
            playBtn.onClick.AddListener(OnPlayClick);
            restartBtn.onClick.AddListener(OnRestartClick);
            trainBtn.onClick.AddListener(OnTrainClick);
            settingsBtn.onClick.AddListener(OnSettinigsClick);
            nextLevelBtn.onClick.AddListener(OnNextClick);

            curtain.SetActive(true);
            startPanel.SetActive(true);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            chooseLvlPanel.SetActive(false);
            winPanel.SetActive(false);

            playBtn.transform.parent.gameObject.SetActive(true);
            restartBtn.transform.parent.gameObject.SetActive(false);

            foreach (var btn in backColorBtn)
            {
                btn.onClick.AddListener(() =>
                {
                    choosedBackColorBtn = btn;
                    ChangeBackColor();                 
                });
            }

            foreach (var btn in eggColorBtn)
            {
                btn.onClick.AddListener(() =>
                {
                    choosedEggColorBtn = btn;
                    ChangeEggColor();                 
                });
            }
            UpdateLevelView();
        }

        public void LoadChooseLevelPanel()
        {
            if (levelItems.Count > 0)
            {
                for (int item = 0; item < levelItems.Count; item++)
                {
                    var levelItem = levelItems[item];
                    levelItem.Load(item, this);
                    return;
                }
            }

            for (int i = 0; i < props.levels.Count; i++)
            {
                var item = props.levels[i];
                var level = Instantiate(chooseLvlItemPrefab, Vector3.zero, Quaternion.identity);
                level.transform.SetParent(chooseLvlItemParent.transform);
                level.transform.localScale = Vector3.one;
                var chooseLevelItem = level.GetComponent<ChooseLevelItem>();

                chooseLevelItem.Load(i+1,this);
                levelItems.Add(chooseLevelItem);
            }
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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                curtain.SetActive(true);
                winPanel.SetActive(false);
                startPanel.SetActive(true);
                levelPanel.SetActive(false);
                trainPanel.SetActive(false);
                settingsPanel.SetActive(false);
                chooseLvlPanel.SetActive(false);

                playBtn.transform.parent.gameObject.SetActive(false);
                restartBtn.transform.parent.gameObject.SetActive(true);
            }
        }

        void OnNextClick()
        {
            Main.Instance.CurrentLevel++;
            
            if (Main.Instance.CurrentLevel > props.levels.Count - 1)
                Main.Instance.CurrentLevel = props.levels.Count - 1;
            
            UpdateLevelView();
            Main.Instance.Restart();

            curtain.SetActive(false);
            winPanel.SetActive(false);
            startPanel.SetActive(false);
            levelPanel.SetActive(true);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            chooseLvlPanel.SetActive(false);
            
            playBtn.transform.parent.gameObject.SetActive(true);
            restartBtn.transform.parent.gameObject.SetActive(false);

            UpdateLevelView();
        }

        void OnPlayClick()
        {
            Main.Instance.Restart();
            
            curtain.SetActive(false);
            winPanel.SetActive(false);
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            //chooseLvlPanel.SetActive(true);
            
            playBtn.transform.parent.gameObject.SetActive(false);
            restartBtn.transform.parent.gameObject.SetActive(false);

            UpdateLevelView();

            Handheld.Vibrate();
        }

        void OnRestartClick()
        {
            Main.Instance.Restart();

            curtain.SetActive(true);
            winPanel.SetActive(false);
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            chooseLvlPanel.SetActive(true);

            playBtn.transform.parent.gameObject.SetActive(false);
            restartBtn.transform.parent.gameObject.SetActive(false);

            UpdateLevelView();

            Handheld.Vibrate();
        }

        void OnTrainClick()
        {
            Main.Instance.Restart();

            winPanel.SetActive(false);
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            trainPanel.SetActive(true);
            settingsPanel.SetActive(false);
            chooseLvlPanel.SetActive(false);

            playBtn.transform.parent.gameObject.SetActive(false);
            restartBtn.transform.parent.gameObject.SetActive(false);

            trainPanel.transform.GetChild(0).gameObject.SetActive(true);//curtain
            trainPanel.transform.GetChild(1).gameObject.SetActive(true);
            trainPanel.transform.GetChild(2).gameObject.SetActive(false);
            trainPanel.transform.GetChild(3).gameObject.SetActive(false);
            trainPanel.transform.GetChild(4).gameObject.SetActive(false);

            Handheld.Vibrate();
        }

        public void OnDisabledLevelItemClick()
        {

        }

        void OnSettinigsClick()
        {
            curtain.SetActive(true);
            winPanel.SetActive(false);
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(true);
            chooseLvlPanel.SetActive(false);

            playBtn.transform.parent.gameObject.SetActive(false);
            restartBtn.transform.parent.gameObject.SetActive(false);
        }

        public void ShowWin()
        {
            curtain.SetActive(true);
            winPanel.SetActive(true);
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            chooseLvlPanel.SetActive(false);

            playBtn.transform.parent.gameObject.SetActive(false);
            restartBtn.transform.parent.gameObject.SetActive(false);
            
            UpdateLevelView();

            Handheld.Vibrate();
        }

        public void UpdateLevelView()
        {
            var level = Main.Instance.ChoosedLevel + 1;

            var difficultText = "Extreme";

            if (level >= 80)
                difficultText = "Extreme";
            else if (level >= 60)
                difficultText = "Super";
            else if (level >= 40)
                difficultText = "Very";
            else if (level >= 20)
                difficultText = "Hard";
            else 
                difficultText = "Easy";
            
            difficult.text = difficultText;
            levelNumber.text = "level " + level.ToString();

            LoadChooseLevelPanel();
        }

        public void ChangeBackColor()
        {
            choosedBackColor = choosedBackColorBtn.GetComponent<Image>().color;
            Camera.main.backgroundColor = choosedBackColor;
        }

        public void ChangeEggColor()
        {
            choosedEggColor = choosedEggColorBtn.GetComponent<Image>().color;
            foreach(var item in eggMaterial)
                item.color = choosedEggColor;
        }
    }
}
