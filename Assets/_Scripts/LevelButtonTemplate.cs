using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace akistd
{
    public class LevelButtonTemplate : MonoBehaviour
    {
        [SerializeField]
        private LevelSO levelInfo;

        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private TextMeshProUGUI timer;

        [SerializeField]
        private Image thumb;

        public LevelSO LevelInfo { get => levelInfo; set => levelInfo = value; }

        private void Awake()
        {
            if (levelInfo != null)
            {
                title.text = "Tầng " + levelInfo.sceneCode.ToString();
                timer.text = levelInfo.timeComplete;
                thumb.sprite = levelInfo.thumbnail;
                Button button = this.GetComponent<Button>();

                button.onClick.AddListener(() => goToScene());

            }
        }

        private void goToScene() 
        {
            GameManager.Instance.LoadScene(levelInfo.sceneCode);
        }


    }
}
