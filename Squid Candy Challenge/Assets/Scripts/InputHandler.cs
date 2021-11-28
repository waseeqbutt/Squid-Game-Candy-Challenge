using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class InputHandler : MonoBehaviour
    {
        public bool IsClicked { get; set; }
        public bool IsClickedOut { get; set; }
        public Vector3 CursorPosition { get; set; }

        // Update is called once per frame
        void Update()
        {
            CursorPosition = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                IsClicked = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                IsClickedOut = true;
                IsClicked = false;
            }
        }
    }

}