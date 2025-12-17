using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

namespace akistd
{
    public class GameManager : MonoBehaviour
    {
        //GameManager, tôi sẽ đảm nhiệm việc gì?
        //Gọi các hàm trong trò chơi để chuẩn bị.

        // Tính + lưu trữ điểm.

        //Load, reload màn chơi.
        public enum GameState
        {
            Starting,
            Playing,
            Win,
            Lose,
            Pause,
            NotInGame
        }

        private GameState gameState;

        private List<LevelSO> levelList = new List<LevelSO>();

        [SerializeField]
        private LevelSO currentLevel;

        [SerializeField]
        private GameObject buttonTemplate;

        private float levelCompleteTime;
        private bool isIngame=false;

        private akistd.FirstPerson.PlayerInput playerInput;

        public GameState CurrentGameState { get => gameState;private set => gameState = value; }

        public static GameManager Instance = null;

        private void Awake()
        {

            if (Instance == null)
            {
                Instance = this;
            }
           
            else if (Instance != this)
            {
                Destroy(gameObject);
            }



            DontDestroyOnLoad(gameObject);

/*            CookLevelData();*/
            


        }

        private void Start()
        {
            CookLevelData();
            ServerCurrentScene();
            AudioManager.Instance.clearEffect();
            Debug.Log("Level" + currentLevel.sceneCode);
            
        }

        private void Update()
        {
            GameLevelInGame();
        }

        #region Public Funcs
        public void GameWin()
        {
            PauseGame();
            UpdateLevelInfo();
            DisableInput();
            isIngame = false;
            playerInput.InputActions.UI.Esc.Disable();
            UIManager.Instance.OpenCanvas(UIManager.ScreenType.WINCanvas);


        }

        private void UpdateLevelInfo()
        {

            if (currentLevel.timeComplete == "NaN")
            {
                currentLevel.timeComplete = refractorTimeString(levelCompleteTime);
            }
            else if (refractorTimeFloat(currentLevel.timeComplete) > levelCompleteTime && levelCompleteTime > 0)
            {
                currentLevel.timeComplete = refractorTimeString(levelCompleteTime);
            }
            else
            {
                currentLevel.timeComplete = "NaN";
            }

            if (currentLevel.sceneCode + 1 < levelList.Count)
            {
                levelList[currentLevel.sceneCode + 1].unlocked = true;
          
            }
            levelList[currentLevel.sceneCode].timeComplete = currentLevel.timeComplete;
            levelList[currentLevel.sceneCode].unlocked = currentLevel.unlocked;

            LevelDataSave();

        }

        private void LevelDataSave()
        {
            List<LevelData> activatedLevel = new List<LevelData>();

            foreach (var item in levelList)
            {
                LevelData lv = new LevelData(item);

                activatedLevel.Add(lv);
                
            }

            SaveLoad.SaveData(activatedLevel);
        }

        public void GameLose()
        {
            Debug.Log("Loe?");
            isIngame = false;
            Time.timeScale = 0f;
            Canvas loseCanvas = GameObject.Find("DETCanvas").GetComponent<Canvas>();
            Cursor.lockState = CursorLockMode.None;
            if (playerInput != null)
            {
                playerInput.PlayerActions.Disable();
            }
            loseCanvas.enabled = true;
        }
        
        public void GameRetry()
        {
            LoadScene(currentLevel.sceneCode);
            AudioManager.Instance.clearEffect();
            Time.timeScale = 1f;
        }

        public void ContinueAfterPause()
        {
            UIManager.Instance.OpenInGameCanvas(UIManager.ScreenType.IngameCanvas);
            isIngame = true;
            PlayGame();
            playerInput.PlayerActions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void TogglePauseMenu()
        {
            UIManager.Instance.TogglePauseCanvas(UIManager.ScreenType.PauseCanvas);

        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            isIngame = false;
        }

        public void PlayGame()
        {
            Time.timeScale = 1f;
        }

        public void DisableInput()
        {
            playerInput.PlayerActions.Disable();
        }

        public void EnableInput()
        {
            playerInput.PlayerActions.Enable();
        }

        public void setUpLevelOptionsScene()
        {
            GameObject parentPanel = GameObject.Find("LevelListPanel");
            
            foreach (Transform child in parentPanel.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            LevelDataLoad();
            foreach (var item in levelList)
            {
                if (item.sceneType == LevelSO.SceneType.Level)
                {
                    buttonTemplate.GetComponent<LevelButtonTemplate>().LevelInfo = item;
                    GameObject lvlGO = Instantiate(buttonTemplate, parentPanel.transform, false);
                    if (!item.unlocked)
                    {
                        lvlGO.GetComponent<Button>().interactable = false;
                    }
                }

            }


        }


        #endregion

        #region private Func

        public void LoadScene(int levelCode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.LoadScene(levelCode);
            currentLevel = levelList[levelCode];
            SceneManager.sceneLoaded += OnSceneLoaded;
            
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);

            ServerCurrentScene();
            PlayGame();
        }

        public void LoadNextScene()
        {

            SceneManager.LoadScene(currentLevel.sceneCode + 1);
            currentLevel = levelList[currentLevel.sceneCode + 1];
            ServerCurrentScene();

        }

        public void GoHomeScene()
        {
            LevelDataSave();
            LoadScene(0);

        }

        public void ChangeSens(float sens)
        {
            if (GameObject.Find("PlayerCamera") != null)
            {
                GameObject playerCam = GameObject.Find("PlayerCamera");
                playerCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sens;
                playerCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sens;
                
            }

            SaveGameSettings();
        }

        public void SaveGameSettings()
        {
            float generalVolume, musicVolume, sfxVolume;
            float sens;


            UIHelpers uihelper = GameObject.Find("UIHelpers").GetComponent<UIHelpers>();
            


            generalVolume = uihelper.CurrentMainAudioData;
            musicVolume = uihelper.CurrentMusicAudioData;
            sfxVolume = uihelper.CurrentSfxAudioData;
            sens = uihelper.CurrentSensData;

            string allvolume = generalVolume+";";
            string musicvolume = musicVolume + ";";
            string sfxvolume = sfxVolume + ";";
            

            string settings = allvolume + musicvolume + sfxvolume+ sens;
            SaveLoad.SaveGameSettings(settings);

            
        }

        public void SaveGameSettingsDefault()
        {
            float generalVolume, musicVolume, sfxVolume;
            float sens;



            generalVolume = 0f;
            musicVolume = 0f;
            sfxVolume = 0f;
            sens = 0.05f;

            string allvolume = generalVolume + ";";
            string musicvolume = musicVolume + ";";
            string sfxvolume = sfxVolume + ";";


            string settings = allvolume + musicvolume + sfxvolume + sens;
            SaveLoad.SaveGameSettingsFirstTime(settings);


        }

        public void LoadGameSettings()
        {
            string rawSettings = SaveLoad.LoadGameSettings();
            string[] settings = rawSettings.Split(';');
            Debug.Log(rawSettings);
            UIHelpers uihelper = GameObject.Find("UIHelpers").GetComponent<UIHelpers>();

            uihelper.changeMainVolume(float.Parse(settings[0]));
            uihelper.changeMusicVolume(float.Parse(settings[1]));
            uihelper.changeSFXVolume(float.Parse(settings[2]));
            uihelper.changeSens(float.Parse(settings[3]));

            

            UIManager.Instance.updateSettingsUI(float.Parse(settings[0]), float.Parse(settings[1]), float.Parse(settings[2]), float.Parse(settings[3]));

        }

        private void CookLevelData()
        {

            var list = Resources.LoadAll<LevelSO>("Data/Level");

            foreach (var item in list)
            {
                levelList.Add(item);
            }

            levelList.Sort((e,e2) => e.sceneCode.CompareTo(e2.sceneCode));
            currentLevel = levelList[SceneManager.GetActiveScene().buildIndex];

          
        }


        private void ServerCurrentScene()
        {
            if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
            {
                
                Debug.Log("First Time Opening");
                PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

                SaveGameSettingsDefault();
                LoadGameSettings();

            }
            else
            {
                Debug.Log("NOT First Time Opening");
                LoadGameSettings();
            }

            //LoadGameSettings();
            //LevelDataSave();
            if (currentLevel.sceneType == LevelSO.SceneType.MainMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                AudioManager.Instance.PlayMusic(currentLevel.bgm);
                LevelDataLoad();
            }
            else if (currentLevel.sceneType == LevelSO.SceneType.Level)
            {
                AudioManager.Instance.PlayMusic(currentLevel.bgm);
                serveLevelScene();
            }

            
        }

        private void LevelDataLoad()
        {
            LevelDataList levelDataList = SaveLoad.LoadData();

            if (levelDataList != null)
            {
                foreach (var item in levelDataList.lvDataList)
                {
                    Debug.LogError("Bo lang nuoc oiiii" + item.sceneCode + " "+ item.unlocked);
                    levelList.Find(e => e.sceneCode == item.sceneCode).unlocked = item.unlocked;
                    levelList.Find(e => e.sceneCode == item.sceneCode).timeComplete = item.timeComplete;
                }
            }
            else
            {
                Debug.LogError("Bo lang nuoc oiiii");
                LevelDataSave();
            }
        }


        private void serveLevelScene()
        {
            LoadPlayerComponent();
            Cursor.lockState = CursorLockMode.Locked;
            playerInput.InputActions.UI.Esc.started += PauseGameToggle;
            GameLevelStart();
        }

        private void LoadPlayerComponent()
        {
            if (GameObject.Find("Player") != null)
            {
                GameObject player = GameObject.Find("Player");
                playerInput = player.GetComponent<FirstPerson.PlayerInput>();


            }

            if (GameObject.Find("PlayerCamera") != null)
            {
                GameObject playerCam = GameObject.Find("PlayerCamera");
                playerCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.2f;
                playerCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.2f;
            }
        }

        private void PauseGameToggle(InputAction.CallbackContext context)
        {
            TogglePauseMenu();
        }

        private void GameLevelStart()
        {
            isIngame = true;
            levelCompleteTime = 0;
            
        }

        private void GameLevelInGame()
        {
            if (isIngame)
            {
                levelCompleteTime += Time.deltaTime;
                UIManager.Instance.changeTimer(refractorTimeString(levelCompleteTime));
                UIManager.Instance.changeInGameLevel("Tầng " + currentLevel.sceneCode);
            }
        }

        private string refractorTimeString(float levelCompleteTime)
        {
            string minutes = Mathf.Floor(levelCompleteTime / 60).ToString("00");
            string seconds = (levelCompleteTime % 60).ToString("00");
            return string.Format("{0}:{1}", minutes, seconds);
        }

        private float refractorTimeFloat(string levelCompleteTime)
        {
            string min = levelCompleteTime.Split(":")[0];
            string sec = levelCompleteTime.Split(":")[1];
            float minutes = Mathf.Floor(float.Parse(min) * 60);
            float seconds = Mathf.Floor(float.Parse(sec));
            seconds += minutes;
            return seconds;
        }

        #endregion
    }
}

