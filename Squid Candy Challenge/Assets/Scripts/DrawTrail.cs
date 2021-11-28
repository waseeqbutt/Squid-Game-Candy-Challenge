using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class DrawTrail : MonoBehaviour
    {
        public enum State { Idle, InProgress, Completed, Failed, Paused }
        public State state;
        public InputHandler inputHandler;
        public Camera mainCamera;
        public Transform pointerObj;
        public TrailRenderer pointerTrail;
        public Collider pointerCollider;
        public ParticleSystem pointerParticle;
        public int remainingHits = 15;

        private Ray ray;
        private RaycastHit hit;
        private bool isSwiped = true;

        // Start is called before the first frame update
        private void Start()
        {
            pointerTrail.enabled = false;
            state = State.Paused;
        }

        public void Initiate()
        {
            state = State.Idle;
        }

        // Update is called once per frame
        void Update()
        {
            if(inputHandler.IsClicked == true && (state == State.InProgress || state == State.Idle))
            {
                OnClickedIn();
            }

            if(inputHandler.IsClickedOut == true && state == State.InProgress)
            {
                OnClickedOut();
            }
        }

        private void OnClickedIn()
        {
            if (GetRaycastHit())
            {
                if (hit.collider.CompareTag("Pointer"))
                {
                    OnRaycastHitPointer();
                }
                else
                if (isSwiped == true)
                {
                    if (hit.collider.CompareTag("Target"))
                    {
                        OnRaycastHitTarget();
                    }
                    else
                    {
                        OnFailedRaycastHit();
                    }
                }
            }
        }

        private void OnClickedOut()
        {
            inputHandler.IsClickedOut = false;
            pointerCollider.enabled = true;
            pointerParticle.enableEmission = false;
            isSwiped = false;
            state = State.Idle;
        }

        private bool GetRaycastHit()
        {
            ray = mainCamera.ScreenPointToRay(inputHandler.CursorPosition);
            return Physics.Raycast(ray, out hit, 200f);
        }

        private void OnRaycastHitPointer()
        {
            isSwiped = true;
            pointerCollider.enabled = false;
        }

        private void OnRaycastHitTarget()
        {
            pointerObj.position = hit.point;
            pointerTrail.enabled = true;
            pointerParticle.enableEmission = true;
            state = State.InProgress;

            if (IsFinalPosition(hit.point) == true)
            {
                OnCompleted();
                return;
            }
        }

        private void OnFailedRaycastHit()
        {
            if (remainingHits <= 0)
            {
                OnFailed();
            }

            remainingHits--;

            Debug.Log("<color=red> Wrong hit </color>");
        }

        private bool IsFinalPosition(Vector3 hitPoint)
        {
            if(pointerTrail.positionCount > 50 && (pointerTrail.GetPosition(0) - hitPoint).magnitude < 0.1f)
            {
                return true;
            }
            else
                return false;
        }

        private void OnCompleted()
        {
            Debug.Log("<color=green> Completed </color>");
            state = State.Completed;
            pointerObj.gameObject.SetActive(false);
            EventManager.Instance.OnSuccess();
        }

        private void OnFailed()
        {
            state = State.Failed;
            EventManager.Instance.OnFail();
        }
    }

}