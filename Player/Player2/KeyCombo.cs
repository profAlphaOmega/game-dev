using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Player2 
{
    public class KeyCombo : MonoBehaviour
    {
        public string[] buttons;
        public int[] minFrameWindow = {0};
        public int[] maxFrameWindow = {0};
        private int iKeyCombo = 0; //moves along the array as buttons are pressed
        private int timeLastButtonPressed;
        private bool keyPressedThisFrame = false;
        public Vector2 input;
        public Vector2 lastInput;
    
        public KeyCombo(string[] b, int[] a, int[] p)
        {
            buttons = b;
            minFrameWindow = a;
            maxFrameWindow = p;
        }

        public void Start()
        {
            timeLastButtonPressed = Time.frameCount;        
        }

        public bool Check(List<InputManager.PlayerInput> i)
        {	
            if (ComboButtonCheck(i))
                {
                    if (
                            (iKeyCombo > 0) 
                            &&
                            (
                                (Time.frameCount > (timeLastButtonPressed + maxFrameWindow[iKeyCombo - 1])) 
                                || 
                                (Time.frameCount < (timeLastButtonPressed + minFrameWindow[iKeyCombo - 1]))
                            )
                        )
                        {iKeyCombo = 0;}
                    
                    iKeyCombo++;
                    timeLastButtonPressed = Time.frameCount;
                    // Debug.Log("CI: " + iKeyCombo);
                    if (iKeyCombo >= buttons.Length)
                    {
                        iKeyCombo = 0;
                        lastInput = input;
                        return true;
                    }

                    else {
                        lastInput = input;
                        return false;
                    } 
                }
                // else {
                //     iKeyCombo = 0;
                // }
            return false;	    
        }

        public bool ComboButtonCheck(List<InputManager.PlayerInput> i)
        {
            bool c = false;
            // InputManager.playerInputHistory.Last().ForEach(ia => {    
            i.ForEach(ia => {    
                if (ia.InputName == buttons[iKeyCombo])
                {
                    c = true;
                }
                else 
                {
                    iKeyCombo = 0;
                }
            });
            return c;
        }
    }
}