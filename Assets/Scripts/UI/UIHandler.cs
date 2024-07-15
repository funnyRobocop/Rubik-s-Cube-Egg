using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{

    public class UIHandler : MonoBehaviour
    {
        public Button nextLevelBtn;
        public GameObject winPanel;



        void Awake()
        {
            nextLevelBtn.onClick.AddListener(OnNextClick);
            winPanel.SetActive(false);
        }

        void OnNextClick()
        {
            
        }

        public void ShowWin()
        {
            winPanel.SetActive(true);
            winPanel.GetComponent<Animator>().SetTrigger("Start");
        }
    }
}
