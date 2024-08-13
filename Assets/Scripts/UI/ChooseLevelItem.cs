using System;
using Palmmedia.ReportGenerator.Core;
using RubiksCubeEgg;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChooseLevelItem : MonoBehaviour
    {
        public Button playBtn;
        public TextMeshProUGUI levelText;
        public Image back;
        public Color passedColor;
        public Color scippedColor;
        public Color disabledColor;
        public Color defaultLevelTextColor;
        public State state = State.None;
        public int leveNumber;

        public enum State { Passed, Skipped, Disabled, None }

        public void Load(int levelNumber, UIHandler handler)
        {
            Debug.Log(levelNumber);
            playBtn.onClick.RemoveAllListeners();
            var currentLevel = Main.Instance.CurrentLevel;

            if (levelNumber > currentLevel)
                state = State.Disabled;
            else if (Main.Instance.SkippedLevelList.Contains(levelNumber))
                state = State.Skipped;
            else 
                state = State.Passed;

            this.levelText.text = levelNumber.ToString();

            if (state == State.Passed)
            {
                back.color = passedColor;
                levelText.color = defaultLevelTextColor;
                playBtn.onClick.AddListener(() => 
                { 
                    Main.Instance.ReloadLevel(levelNumber); 
                    handler.levelPanel.SetActive(true);
                    handler.settingsBtn.gameObject.SetActive(true);
                    handler.restartBtn.gameObject.SetActive(true);
                    handler.ShowAllStartBtn(true);
                    handler.chooseLvlPanel.SetActive(false);
                    handler.curtain.SetActive(false);
                });
                handler.UpdateLevelView();
            }
            else if (state == State.Disabled)
            {
                back.color = disabledColor;
                levelText.color = disabledColor;
            }
            else if (state == State.Skipped)
            {
                back.color = scippedColor;
                levelText.color = defaultLevelTextColor;
                playBtn.onClick.AddListener(() => 
                { 
                    Main.Instance.ReloadLevel(levelNumber); 
                    handler.levelPanel.SetActive(true);
                    handler.settingsBtn.gameObject.SetActive(true);
                    handler.restartBtn.gameObject.SetActive(true);
                    handler.ShowAllStartBtn(true);
                    handler.chooseLvlPanel.SetActive(false);
                    handler.curtain.SetActive(false);
                });
                handler.UpdateLevelView();
            }
        }
    }
}
