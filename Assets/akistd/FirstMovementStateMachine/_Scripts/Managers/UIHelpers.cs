using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace akistd
{
    public class UIHelpers : MonoBehaviour
    {
        [SerializeField]
        private WeaponData UISoundEffect;

        private float currentSensData = 0.02f;
        private float currentMainAudioData= 1f;
        private float currentMusicAudioData= 1f;
        private float currentSfxAudioData = 1f;

        public float CurrentSensData { get => currentSensData; }
        public float CurrentMainAudioData { get => currentMainAudioData;  }
        public float CurrentMusicAudioData { get => currentMusicAudioData;  }
        public float CurrentSfxAudioData { get => currentSfxAudioData;  }


        public void GoHomeScene()
        {
            PlayUISound();
            GameManager.Instance.GoHomeScene();
            Time.timeScale = 1f;
        }

        public void Play()
        {
            PlayUISound();
            GameManager.Instance.LoadNextScene();
        }

        public void ContinueGame()
        {
            PlayUISound();
            GameManager.Instance.ContinueAfterPause();
        }

        public void Exit()
        {
            PlayUISound();
            Application.Quit(); 
        }

        //MainMenu
        public void LevelBackToMainMenu()
        {
            UIManager.Instance.closeAllCanvas();
            Canvas lvlCanvas = GameObject.Find("LevelCanvas").GetComponent<Canvas>();
            Canvas mainCanvas = GameObject.Find("MainMenuCanvas").GetComponent<Canvas>();
 
            lvlCanvas.enabled = false;

            mainCanvas.enabled = true;
            PlayUISound();
        }

        public void MainMenuToLevel()
        {
            Canvas lvlCanvas = GameObject.Find("LevelCanvas").GetComponent<Canvas>();
            Canvas mainCanvas = GameObject.Find("MainMenuCanvas").GetComponent<Canvas>();

            lvlCanvas.enabled = true;

            mainCanvas.enabled = false;
            PlayUISound();
            GameManager.Instance.setUpLevelOptionsScene();
        }

        public void Retry()
        {
            PlayUISound();
            GameManager.Instance.GameRetry();

        }

        public void OpenOptionScreen()
        {
            UIManager.Instance.OpenCanvas(UIManager.ScreenType.OptionsCanvas);

        }
        public void OpenPauseScreen()
        {
            UIManager.Instance.OpenCanvas(UIManager.ScreenType.PauseCanvas);

        }

        public void OpenCreditScreen()
        {
            UIManager.Instance.OpenCanvas(UIManager.ScreenType.CreditsCanvas);

        }

        public void NextLevel()
        {
            PlayUISound();
            GameManager.Instance.LoadNextScene();

        }

        private void PlayUISound()
        {
            AudioManager.Instance.Play(UISoundEffect.swordSFX.audioList[0]);

        }

        

        public void goToFirstLevel()
        {
            GameManager.Instance.LoadScene(2);
        }

        public void changeMainVolume(float value)
        {
            currentMainAudioData = value;
            AudioManager.Instance.changeMainVolume(value);
        }

        public void changeSFXVolume(float value)
        {
            currentSfxAudioData = value; 
            AudioManager.Instance.changeSFXVolume(value);
        }

        public void changeMusicVolume(float value)
        {
            currentMusicAudioData = value;
            AudioManager.Instance.changeMusicVolume(value);
        }

        public void changeSens(float value)
        {
     
            currentSensData = value;

            GameManager.Instance.ChangeSens(currentSensData);
        }

        public void updatePlayerHealth()
        {
            GameObject ingameCanvas = GameObject.Find("IngameCanvas");
            if (ingameCanvas != null)
            {
                PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<akistd.FirstPerson.Player>().PlayerHealth;
                Slider healthbar = ingameCanvas.transform.Find("HealthBar").gameObject.GetComponent<Slider>();
                float value = healthbar.value;
                healthbar.value = Mathf.Lerp(value, playerHealth.CurrentHealth,0.2f);
                TextMeshProUGUI healthText = ingameCanvas.transform.Find("HealthBar").GetComponentInChildren<TextMeshProUGUI>();
                healthText.text = playerHealth.CurrentHealth + "/100";
            }
        }

        private void Update()
        {
            updatePlayerHealth();
        }

    }
}
