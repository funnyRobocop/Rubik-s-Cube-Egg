using System.Collections.Generic;
using System.Linq;
using RubiksCubeEgg;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_WEBGL
using YG;
#endif

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
        public AudioSource music;
        public GameObject adBtn;

        public GameObject chooseLvlItemPrefab;
        public GameObject chooseLvlItemParent;
        public List<ChooseLevelItem> levelItems = new();

        public Material[] eggMaterial;
        private Color choosedBackColor;
        public SpriteRenderer back;
        private Color choosedEggColor;
        public Button choosedBackColorBtn;
        public Button choosedEggColorBtn;        
        public List<Button> backColorBtn;
        public List<Button> eggColorBtn;
        
        public Button playBtn;
        public GameObject restartBtn;
        public GameObject trainBtn;
        public GameObject settingsBtn;
        public GameObject musicOn;
        public GameObject musicOff;

        public Vector3 initPlayBtnPos;
        public Vector3 initTrainBtnPos;

        public TextMeshProUGUI levelNumber;
        public TextMeshProUGUI difficult;

        public RubiksCubeEgg.Game.Props props;

        public GameObject adBtnWithText;
        public GameObject adBtnWithoutText;
        private int lastWidth;


        void Awake()
        {
            foreach (var btn in backColorBtn)
            {
                btn.onClick.AddListener(() =>
                {
                    choosedBackColorBtn = btn;
                    foreach (var item in backColorBtn)
                        item.GetComponentsInChildren<Image>(true).Last().enabled = false;
                    btn.GetComponentsInChildren<Image>(true).Last().enabled = true;
                    ChangeBackColor();
                    Main.Instance.Bg = backColorBtn.IndexOf(btn);
                    Main.Instance.SaveData();              
                });
            }

            foreach (var btn in eggColorBtn)
            {
                btn.onClick.AddListener(() =>
                {
                    choosedEggColorBtn = btn;
                    foreach (var item in eggColorBtn)
                        item.GetComponentsInChildren<Image>(true).Last().enabled = false;
                    btn.GetComponentsInChildren<Image>(true).Last().enabled = true;
                    ChangeEggColor();
                    Main.Instance.Egg = eggColorBtn.IndexOf(btn);
                    Main.Instance.SaveData();
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
#if UNITY_WEBGL
                YG2.InterstitialAdvShow();
#else
                AdsInterstitial.Instance.ShowInterstitial();
                AdsRewarded.Instance.RequestRewardedAd();
#endif
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
                if (winPanel.activeInHierarchy /*|| dialogPanel.activeInHierarchy*/)
                    return;

                FromLevelToMenu();
            }

            if (Screen.width != lastWidth)
                OnChangeScreenWidth();
        }

        public void OnChangeScreenWidth()
        {
            adBtnWithText.SetActive(Screen.width >= Screen.height);
            adBtnWithoutText.SetActive(Screen.width < Screen.height);
            lastWidth = Screen.width;
        }

        public void FromLevelToMenu()
        {
                curtain.SetActive(true);
                startPanel.SetActive(true);
                trainPanel.SetActive(false);
                settingsPanel.SetActive(false);
                chooseLvlPanel.SetActive(false);
                LoadMusic(Main.Instance.Music);
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

            chooseLvlItemParent.transform.position = Vector3.zero;
            music.Stop();
        }

        public void LoadSettings(int bg, int egg)
        {
            backColorBtn[bg].onClick.Invoke();
            eggColorBtn[egg].onClick.Invoke();
        }

        public void LoadMusic(bool isOn)
        {
            if (isOn)
                musicOff.GetComponentInChildren<Button>(true).onClick.Invoke();
            else
                musicOn.GetComponentInChildren<Button>(true).onClick.Invoke();
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

            ShowAllStartBtn(false);

#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }

        public void UpdateLevelView()
        {
            var level = Main.ChoosedLevel;
#if UNITY_WEBGL
            var isRussian = YG2.lang == "ru";
#else
            var isRussian = Application.systemLanguage == SystemLanguage.Russian;
#endif          

            string difficultText;
            if (level >= 14)
                difficultText = isRussian ? "сложно" : "hard";
            else 
                difficultText = isRussian ? "просто" : "easy";
            
            difficult.text = difficultText;
            levelNumber.text = (isRussian ? "уровень " : "level ") + level.ToString();
        }

        public void ChangeBackColor()
        {
            choosedBackColor = choosedBackColorBtn.GetComponent<Image>().color;
            Camera.main.backgroundColor = choosedBackColor;
            back.color = new Color(choosedBackColor.r, choosedBackColor.g, choosedBackColor.b, 255);
        }

        public void ChangeEggColor()
        {
            choosedEggColor = choosedEggColorBtn.GetComponent<Image>().color;
            foreach(var item in eggMaterial)
                item.color = new Color(choosedEggColor.r, choosedEggColor.g, choosedEggColor.b, 255);
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
            music.Stop();
        }

        public void ShowAllStartBtn(bool isOn)
        {
            restartBtn.SetActive(isOn);
            startPanel.GetComponent<Animator>().enabled = isOn;

            playBtn.transform.parent.position = new Vector3(playBtn.transform.parent.position.x, initPlayBtnPos.y, playBtn.transform.parent.position.z);
            trainBtn.transform.position = new Vector3(trainBtn.transform.position.x, initTrainBtnPos.y, trainBtn.transform.position.z);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}
