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
        public GameObject dialogPanel;

        public GameObject chooseLvlItemPrefab;
        public GameObject chooseLvlItemParent;
        public List<ChooseLevelItem> levelItems = new();

        public Material[] eggMaterial;
        private Color choosedBackColor;
        private Color choosedEggColor;
        private Button choosedBackColorBtn;
        private Button choosedEggColorBtn;        
        public Button[] backColorBtn;
        public Button[] eggColorBtn;
        
        public Button playBtn;
        public GameObject restartBtn;
        public GameObject trainBtn;
        public GameObject settingsBtn;

        public Vector3 initPlayBtnPos;
        public Vector3 initTrainBtnPos;

        public TextMeshProUGUI levelNumber;
        public TextMeshProUGUI difficult;

        public RubiksCubeEgg.Game.Props props;


        void Awake()
        {
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

            initPlayBtnPos = playBtn.transform.parent.position;
            initTrainBtnPos = trainBtn.transform.position;
        }

        void Start()
        {            
            if (Main.ChoosedLevel > 0)
            {
                curtain.SetActive(false);
                startPanel.SetActive(false);                
                levelPanel.SetActive(true);
                ShowAllStartBtn(true);
            }
        }

        void OnDestroy()
        {            
            foreach (var item in backColorBtn)
                item.onClick.RemoveAllListeners();              
            foreach (var item in eggColorBtn)
                item.onClick.RemoveAllListeners(); 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (winPanel.activeInHierarchy || dialogPanel.activeInHierarchy)
                    return;

                curtain.SetActive(true);
                startPanel.SetActive(true);
                
                //levelPanel.SetActive(false);
                trainPanel.SetActive(false);
                settingsPanel.SetActive(false);
                chooseLvlPanel.SetActive(false);
            }
        }

        public void LoadChooseLevelPanel()
        {
            if (levelItems.Count > 0)
            {
                for (int item = 0; item < levelItems.Count; item++)
                {
                    var levelItem = levelItems[item];
                    levelItem.Load(item+1, this);
                }
                return;
            }

            for (int i = 0; i < props.levels.Count - 1; i++)
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

        public void ShowWin()
        {
            curtain.SetActive(true);
            winPanel.SetActive(true);

            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            trainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            chooseLvlPanel.SetActive(false);
            dialogPanel.SetActive(false);

            ShowAllStartBtn(false);

            Handheld.Vibrate();
        }

        public void UpdateLevelView()
        {
            var level = Main.ChoosedLevel;

            string difficultText;
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

        public void OnPlayBtnClick()
        {
            curtain.SetActive(false);
            startPanel.SetActive(false);

            if (!Main.Instance.IsRun)
            {
                LoadChooseLevelPanel();
                chooseLvlPanel.SetActive(true);
            }
        }

        public void ShowAllStartBtn(bool isOn)
        {
            restartBtn.SetActive(isOn);
            startPanel.GetComponent<Animator>().enabled = isOn;

            playBtn.transform.parent.position = new Vector3(playBtn.transform.parent.position.x, initPlayBtnPos.y, playBtn.transform.parent.position.z);
            trainBtn.transform.position = new Vector3(trainBtn.transform.position.x, initTrainBtnPos.y, trainBtn.transform.position.z);
        }
    }
}
