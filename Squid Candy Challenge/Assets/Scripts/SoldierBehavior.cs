using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class SoldierBehavior : MonoBehaviour
    {
        public Animator animator;
        public AudioClip shotSound;
        public AudioSource audioSource;

        public void OnPlayerFail()
        {
            StartCoroutine(ExecutionRoutine());
        }

        private IEnumerator ExecutionRoutine()
        {
            yield return new WaitForSeconds(1.0f);
            animator.enabled = true;
            yield return new WaitForSeconds(0.8f);
            EventManager.Instance.OnExecution();
            audioSource.PlayOneShot(shotSound, 0.8f);
        }
    }

}