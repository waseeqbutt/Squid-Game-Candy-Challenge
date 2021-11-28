using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class SoundManager : MonoBehaviour
    {
        public AudioClip clickSoundFx;
        public AudioClip successSoundFx;
        public AudioClip failSoundFx;
        public AudioClip shotSoundFx;
        public AudioClip crackSoundFx;
        public AudioClip screamSoundFx;

        public List<SoundType> audioList;

        private Dictionary<string, AudioClip> audioDictionary;
        private AudioSource audioSource;

        [System.Serializable]
        public struct SoundType
        {
            public string audioKey;
            public AudioClip audioClipValue;
        }

        void Awake()
        {
            audioDictionary = new Dictionary<string, AudioClip>();

            foreach(var data in audioList)
            {
                audioDictionary[data.audioKey] = data.audioClipValue;
            }
        }

        private void Start()
        {
            EventManager.Instance.OnPlayGameEvent += OnPlayGame;
            EventManager.Instance.OnSuccessEvent += OnSuccess;
            EventManager.Instance.OnLoseGameEvent += OnFail;

            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudioOneShot(string audioName)
        {
            audioSource.PlayOneShot(audioDictionary[audioName], 0.8f);
        }

        public void StopAudioLoop()
        {
            audioSource.clip = null;
            audioSource.loop = false;
        }

        public void OnPlayGame()
        {
            
        }

        public void OnSuccess()
        {
            
        }

        public void OnFail()
        {
            
        }

        private void OnDisable()
        {
            EventManager.Instance.OnPlayGameEvent -= OnPlayGame;
            EventManager.Instance.OnSuccessEvent -= OnSuccess;
            EventManager.Instance.OnLoseGameEvent -= OnFail;
        }
    }

}