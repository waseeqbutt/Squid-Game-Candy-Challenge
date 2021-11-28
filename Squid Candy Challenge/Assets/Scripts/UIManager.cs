using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace vasik
{
    public class UIManager : MonoBehaviour, IGameState
    {
        public GameObject startPanel;
        public GameObject successPanel;
        public GameObject failedPanel;
        [Space]
        public GameObject bloodyImage;

        private void Start()
        {
            EventManager.Instance.OnPlayGameEvent += OnPlayGame;
            EventManager.Instance.OnSuccessEvent += OnSuccess;
            EventManager.Instance.OnLoseGameEvent += OnFail;
            EventManager.Instance.OnRetryGameEvent += OnRetry;
            EventManager.Instance.OnExecutingEvent += OnExecution;
        }

        public void OnFail()
        {
            Invoke("ShowFailPanelDelayed", 5f);
        }

        private void ShowFailPanelDelayed()
        {
            failedPanel.SetActive(true);
        }

        public void OnPlayGame()
        {
            startPanel.SetActive(false);
        }

        public void OnRetry()
        {
            
        }

        public void OnSuccess()
        {
            Invoke("ShowSuccessPanelDelayed", 3f);
        }

        private void ShowSuccessPanelDelayed()
        {
            successPanel.SetActive(true);
        }

        public void OnNextLevel()
        {
            
        }

        public void OnExecution()
        {
            bloodyImage.SetActive(true);
        }

        private void OnDisable()
        {
            EventManager.Instance.OnPlayGameEvent -= OnPlayGame;
            EventManager.Instance.OnSuccessEvent -= OnSuccess;
            EventManager.Instance.OnLoseGameEvent -= OnFail;
            EventManager.Instance.OnRetryGameEvent -= OnRetry;
            EventManager.Instance.OnExecutingEvent -= OnExecution;
        }
    }

}