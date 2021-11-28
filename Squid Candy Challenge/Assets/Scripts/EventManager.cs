using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace vasik
{
    public class EventManager : MonoBehaviour, IGameState
    {
        public static EventManager Instance;

        public event Action OnPlayGameEvent;
        public event Action OnSuccessEvent;
        public event Action OnLoseGameEvent;
        public event Action OnRetryGameEvent;
        public event Action OnNextLevelEvent;
        public event Action OnExecutingEvent;

        void Awake()
        {
            Instance = this;
        }

        public void OnPlayGame()
        {
            if (OnPlayGameEvent != null)
                OnPlayGameEvent.Invoke();
        }

        public void OnSuccess()
        {
            if(OnSuccessEvent != null)
                OnSuccessEvent.Invoke();
        }

        public void OnFail()
        {
            if (OnLoseGameEvent != null)
                OnLoseGameEvent.Invoke();
        }

        public void OnRetry()
        {
            if (OnRetryGameEvent != null)
                OnRetryGameEvent.Invoke();
        }

        public void OnNextLevel()
        {
            if (OnNextLevelEvent != null)
                OnNextLevelEvent.Invoke();
        }

        public void OnExecution()
        {
            if (OnExecutingEvent != null)
                OnExecutingEvent.Invoke();
        }
    }

}