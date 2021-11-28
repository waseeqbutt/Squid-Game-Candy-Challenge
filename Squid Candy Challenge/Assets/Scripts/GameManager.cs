using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace vasik
{
    public class GameManager : MonoBehaviour, IGameState
    {
        public int currentLevel = 1;
        public Level[] level;

        [Space]
        public GameObject cameraObj;
        public GameObject inputHandlerObj;
        public GameObject soundManagerObj;

        [Space]
        public GameObject baseObj;
        public GameObject soldierObj;
        public GameObject crackedCandy;
        public GameObject confettiPrefab;


        private void Start()
        {
            EventManager.Instance.OnPlayGameEvent += OnPlayGame;
            EventManager.Instance.OnSuccessEvent += OnSuccess;
            EventManager.Instance.OnLoseGameEvent += OnFail;
            EventManager.Instance.OnRetryGameEvent += OnRetry;
            EventManager.Instance.OnNextLevelEvent += OnNextLevel;

     //       PlayerPrefs.SetInt("LevelUnlocked", 1);
            currentLevel = PlayerPrefs.GetInt("LevelUnlocked") == 0 ? 1 : PlayerPrefs.GetInt("LevelUnlocked");
            level[currentLevel - 1].gameObject.SetActive(true);
        }

        public Level GetCurrentLevel()
        {
            return level[0];
        }

        public void OnPlayGame()
        {
            inputHandlerObj.SendMessage("Initiate");
        }

        public void OnSuccess()
        {
            StartCoroutine(SuccessSequence());
        }

        public void OnFail()
        {
            StartCoroutine(FailSequence());
        }

        public void OnRetry()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void OnNextLevel()
        {
            SaveLevel();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        private void SaveLevel()
        {
            currentLevel++;

            if (PlayerPrefs.GetInt("LevelUnlocked") >= 4)
            {
                PlayerPrefs.SetInt("LevelUnlocked", 1);
            }
            else
                PlayerPrefs.SetInt("LevelUnlocked", currentLevel);
        }

        public void OnExecution()
        {

        }

        private IEnumerator SuccessSequence()
        {
            confettiPrefab.SetActive(true);
            soundManagerObj.SendMessage("PlayAudioOneShot", "Success");
            soundManagerObj.SendMessage("StopAudioLoop");
            level[currentLevel - 1].candySafeZone.SetActive(false);
            yield return new WaitForSeconds(1f);
            level[currentLevel - 1].candyBase.transform.parent = baseObj.transform;
            baseObj.GetComponent<Animator>().Play("Move");
        }

        private IEnumerator FailSequence()
        {
            crackedCandy.SetActive(true);
            soundManagerObj.SendMessage("PlayAudioOneShot", "Crack");
            yield return new WaitForSeconds(1.5f);
            cameraObj.GetComponent<Animator>().enabled = true;
            soldierObj.SendMessage("OnPlayerFail");
            soundManagerObj.SendMessage("PlayAudioOneShot", "Scream");
        }

        private void OnDisable()
        {
            EventManager.Instance.OnPlayGameEvent -= OnPlayGame;
            EventManager.Instance.OnSuccessEvent -= OnSuccess;
            EventManager.Instance.OnLoseGameEvent -= OnFail;
            EventManager.Instance.OnRetryGameEvent -= OnRetry;
            EventManager.Instance.OnNextLevelEvent -= OnNextLevel;
        }
    }

}