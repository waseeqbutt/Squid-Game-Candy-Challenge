using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class UIAnimator : MonoBehaviour
    {
        public Animator targetUI;
        
        public void SetAnimationValue(float value)
        {
            targetUI.SetFloat("value", value);
        }
    }

}