using System;
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
                //playBtn.onClick.AddListener(Main.Instance.Restart);
            }
            else if (state == State.Disabled)
            {
                back.color = disabledColor;
                levelText.color = disabledColor;
                playBtn.onClick.AddListener(handler.OnDisabledLevelItemClick);
            }
            else if (state == State.Skipped)
            {
                back.color = scippedColor;
                levelText.color = defaultLevelTextColor;
                //playBtn.onClick.AddListener(Main.Instance.Restart);
            }

        }
    }
}
