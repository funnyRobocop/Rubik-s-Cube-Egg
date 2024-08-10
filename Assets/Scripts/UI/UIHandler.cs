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
                
                levelPanel.SetActive(false);
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

        public void OnDisabledLevelItemClick()
        {
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

            Handheld.Vibrate();
        }

        public void UpdateLevelView()
        {
            var level = Main.Instance.ChoosedLevel + 1;

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
    }
}
