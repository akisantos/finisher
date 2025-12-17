using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace akistd
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private UIData data;

        [SerializeField]
        private GameObject killEffectText;

        [SerializeField]
        private Material screenMat;


        public static UIManager Instance = null;

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



        }


        public enum ScreenType
        {
            IngameCanvas,
            PauseCanvas,
            WINCanvas,
            DETCanvas,
            OptionsCanvas,
            MainMenuCanvas,
            CreditsCanvas
        }

        #region Public func

        public void OpenCanvas(ScreenType type)
        {
            string getname = data.canvasesList.Find(e => e.name == type.ToString()).name;
            closeAllCanvas();
            GameObject.Find(getname).GetComponent<Canvas>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void OpenInGameCanvas(ScreenType type)
        {
            string getname = data.canvasesList.Find(e => e.name == type.ToString()).name;
            closeAllCanvas();
            GameObject.Find(getname).GetComponent<Canvas>().enabled = true;
        }

        public void TogglePauseCanvas(ScreenType type)
        {
            string getname = data.canvasesList.Find(e => e.name == type.ToString()).name;
       

            if (GameObject.Find(getname).GetComponent<Canvas>().enabled)
            {

                OpenInGameCanvas(ScreenType.IngameCanvas);
                GameObject.Find(getname).GetComponent<Canvas>().enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.Instance.PlayGame();
                GameManager.Instance.EnableInput();

            }
            else
            {
                closeAllCanvas();
                GameManager.Instance.PauseGame();
                GameManager.Instance.DisableInput();
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find(getname).GetComponent<Canvas>().enabled = true;
            }
            

        }

        public void closeAllCanvas()
        {
            foreach (var item in data.canvasesList)
            {
                if (GameObject.Find(item.name) != null)
                {
                    GameObject.Find(item.name).GetComponent<Canvas>().enabled = false;
                }

            }
        }

        public void changeTimer(string timestring)
        {
           
            if (GameObject.Find(ScreenType.IngameCanvas.ToString()).transform.Find("Timer").gameObject.GetComponent<TextMeshProUGUI>() != null)
            {
      
                TextMeshProUGUI timer = GameObject.Find(ScreenType.IngameCanvas.ToString()).transform.Find("Timer").gameObject.GetComponent<TextMeshProUGUI>();
                timer.text = timestring;
            }
        }

        public void changeInGameLevel(string timestring)
        {

            if (GameObject.Find(ScreenType.IngameCanvas.ToString()).transform.Find("Level").gameObject.GetComponent<TextMeshProUGUI>() != null)
            {

                TextMeshProUGUI timer = GameObject.Find(ScreenType.IngameCanvas.ToString()).transform.Find("Level").gameObject.GetComponent<TextMeshProUGUI>();
                timer.text = timestring;
            }
        }

        public void updateSettingsUI(float mainvol, float musicvol,float sfxvol, float sens )
        {
            GameObject.Find("AudioAllFuncGroup").GetComponentInChildren<Slider>().value = mainvol;
            GameObject.Find("AudioMusicFuncGroup").GetComponentInChildren<Slider>().value = musicvol;
            GameObject.Find("AudioSfxFuncGroup").GetComponentInChildren<Slider>().value = sfxvol;
            GameObject.Find("SensivityFunc").GetComponentInChildren<Slider>().value = sens;
        }

        public void TriggerKillText()
        {
            killEffectText = GameObject.Find(ScreenType.IngameCanvas.ToString()).transform.Find("KillEffectText").gameObject;
            killEffectText.GetComponent<TextMeshProUGUI>().text = RandomKillWord();
            LeanTween.scale(killEffectText, Vector3.one, 0.25f).setEaseOutElastic().setOnComplete(ResetKillText);
        }

        public void takeDamageEffect()
        {
            StopCoroutine("healEffect");
            StartCoroutine("takeDamangeEffect", 1f);
        }

        public void healingEffect()
        {
            StopCoroutine("takeDamangeEffect");
            StartCoroutine("healEffect", 1f);
        }

        public void SpeedUpEffect(float time)
        {
            StopAllCoroutines();
            StartCoroutine("speedUPEffect", time);
        }


        IEnumerator healEffect(float time)
        {
            screenMat.SetColor("_Color", new Color(0.3143463f, 1.642922f, 2.610441f));
            screenMat.SetFloat("_Intensity", 0.332f);
            yield return new WaitForSeconds(time);
            screenMat.SetFloat("_Intensity", 0);
        }

        IEnumerator takeDamangeEffect(float time)
        {
            screenMat.SetColor("_Color", new Color(1.216512f, 0f , 0.1058504f));
            screenMat.SetFloat("_Intensity", 0.332f);
            yield return new WaitForSeconds(time);
            screenMat.SetFloat("_Intensity", 0);
        }

        IEnumerator speedUPEffect(float time)
        {
            screenMat.SetColor("_Color", new Color(0.2276627f, 0.2944967f, 1.378986f));
            screenMat.SetFloat("_Intensity", 0.332f);
            yield return new WaitForSeconds(time);
            screenMat.SetFloat("_Intensity", 0);
        }




        #endregion

        #region private func

        private void ResetKillText()
        {
            LeanTween.scale(killEffectText, Vector3.zero, .25f).setDelay(0.55f).setEaseInOutSine();
            
        }

        private string RandomKillWord()
        {
            string[] words = { "UUUUẦY! ", "DEAD!", "😎 NHỨC NÁCH! ", "ĐÃ CÁI NƯ!", "🤣 DAMM! ", "😆 ĐƯỢC CỦA LÓ!", "😎 BULLEYES!", "Ò Ó O~" };

            return words[Random.Range(0, words.Length)];
        }

        #endregion
    }
}
