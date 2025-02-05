using RubiksCubeEgg;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ChooseLevelItem : MonoBehaviour
    {
        public Button playBtn;
        public TextMeshProUGUI levelText;

        public GameObject opened;
        public GameObject passed;
        public GameObject skipped;
        public GameObject disabled;
        
        public State state = State.None;
        public int leveNumber;

        public enum State { Opened, Passed, Skipped, Disabled, None }

        public void Load(int levelNumber, UIHandler handler)
        {
            //Debug.Log("level item " + levelNumber);
            
            var currentLevel = Main.Instance.CurrentLevel;
            levelText.text = levelNumber.ToString();

            if (levelNumber == currentLevel)
                state = State.Opened;
            else if (levelNumber > currentLevel)
                state = State.Disabled;
            else if (Main.Instance.SkippedLevelList.Contains(levelNumber))
                state = State.Skipped;
            else 
                state = State.Passed;

            //state = State.Passed; // test

            playBtn.onClick.RemoveAllListeners();
            disabled.SetActive(false);
            opened.SetActive(false);
            passed.SetActive(false);
            skipped.SetActive(false);

            if (state == State.Passed)
            {
                passed.SetActive(true);
                levelText.gameObject.SetActive(true);

                playBtn.onClick.AddListener(() => 
                { 
                    Main.ChoosedLevel = levelNumber;
                    handler.RestartLevel();
                });
            }
            else if (state == State.Disabled)
            {
                disabled.SetActive(true);
                levelText.gameObject.SetActive(false);
            }
            else
            {
                skipped.SetActive(true);
                levelText.gameObject.SetActive(true);

                playBtn.onClick.AddListener(() => 
                {
                    Main.ChoosedLevel = levelNumber;
                    handler.RestartLevel();
                });
            }
        }
    }
}
