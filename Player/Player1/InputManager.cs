using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Player1 
{
    public class InputManager : MonoBehaviour
    {
        public Main self;
        public delegate void CaptureInputRaw();
        public CaptureInputRaw captureInputRaw;
        public PlayerXboxMap xboxMap;
        public PlayerKeyboardMap keyboardMap;
        public Inventory inventory;

        public List<PlayerInput> playerInputArray;
        public List<PlayerInput> playerInputDownLastArray;
        public List<List<PlayerInput>> playerInputHistory = new List<List<PlayerInput>>();
        
        public List<PlayerInput> playerInputHoldArray;
        public List<PlayerInput> playerInputHoldLastArray;
        public List<List<PlayerInput>> playerInputHoldHistory = new List<List<PlayerInput>>();
        
        public List<PlayerInput> playerInputUpArray;
        public List<PlayerInput> playerInputUpLastArray;
        public List<List<PlayerInput>> playerInputUpHistory = new List<List<PlayerInput>>();
        
        public Vector2 input;
        public List<List<PlayerInput>> playerInputHistoryRaw = new List<List<PlayerInput>>(); 
        public List<PlayerInput> playerInputRawArray;
        
        private int _playerInputRawMax = 500;
        private bool _previousLeft = false;
        private bool _previousRight = false;
        private bool _previousDown = false;
        private bool _previousUp = false;
        private bool _noHorizontalInput = true;
        private bool _noVerticalInput = true;
        private bool _rightButtonDown = false;
        private bool _rightButtonHeld = false;
        private bool _rightButtonUp = false;
        private bool _leftButtonDown = false;
        private bool _leftButtonHeld = false;
        private bool _leftButtonUp = false;
        private bool _upButtonDown = false;
        private bool _upButtonHeld = false;
        private bool _upButtonUp = false;
        private bool _downButtonDown = false;
        private bool _downButtonHeld = false;
        private bool _downButtonUp = false;
        private bool _noRightTriggerInput = true;
        private bool _noLeftTriggerInput = true;
        private bool _previousTriggerRight = false;
        private bool _previousTriggerLeft = false;
        private bool _leftTriggerButtonDown = false;
        private bool _leftTriggerButtonHeld = false;
        private bool _leftTriggerButtonUp = false;
        private bool _rightTriggerButtonDown = false;
        private bool _rightTriggerButtonHeld = false;
        private bool _rightTriggerButtonUp = false;



        public class PlayerInput
        {
            public int InputFrameCount {get; set;}
            public string InputType {get; set;}
            public string InputName {get; set;}
            public string InputAction {get; set;}
            public bool InputDown {get; set;}
            public bool InputHold {get; set;}
            public bool InputUp {get; set;}
            public bool AltInputDown {get; set;}
            public bool AltInputHold {get; set;}
            public bool AltInputUp {get; set;}

            public PlayerInput() 
            {
                InputFrameCount = Time.frameCount;
                InputType = "default";
                InputName = "default";
                InputAction = "default";
                InputDown = false;
                InputHold = false;
                InputUp = false;
                AltInputDown = false;
                AltInputHold = false;
                AltInputUp = false;
            }
        }

        public void Start()
        {

            // To not have to run the if xbox or keyboard check on every frame,
            // you should, in Start(), detect which is in your settings, than pass a delegate to the Update().
            // Then run the delegate. You will have to breakout the current setup into two functions
            // 
            // How to handle players switching bindings on the fly.
            // You need to just have a binding to the change event (you can't just set the value)
            // Then change the delegate
            self = GetComponent<Main>();
            if(SETTINGS.controllerDevice != "XBOX")
            {
                keyboardMap.SetKeyboardMapping();
                captureInputRaw = new CaptureInputRaw(CaptureInputRawKeyboard);
            }
            else
            {
                captureInputRaw = new CaptureInputRaw(CaptureInputRawXbox);
            }
            playerInputDownLastArray = new List<PlayerInput>();
            playerInputUpLastArray = new List<PlayerInput>();
            playerInputHoldLastArray = new List<PlayerInput>();
            // playerInputHistory = new List<List<PlayerInput>>();
        }
        
        public bool LastInputDown(params string[] s)
        {
            bool c = false;
            if(self.state.inputDisabled) return c;
            foreach (string lida in s)
            {
                playerInputDownLastArray.ForEach(pila => {
                    if(pila.InputName == lida) c = true;
                });
                if(c) break;
            }
            return c;
        }
        
        public bool LastInputHold(params string[] s)
        {
            bool c = false;
            if(self.state.inputDisabled) return c;
            foreach (string liha in s)
            {
                playerInputHoldLastArray.ForEach(pihla => {
                    if(pihla.InputName == liha) c = true;
                });
                if(c) break;
            }
            return c;
        }
        
        public bool LastInputUp(params string[] s)
        {
            bool c = false;
            if(self.state.inputDisabled) return c;
            foreach (string liua in s)
            {
                playerInputUpLastArray.ForEach(piula => {
                    if(piula.InputName == liua) c = true;
                });
                if(c) break;
            }
            return c;
        }

        // public void CaptureInputRaw()
        // {
            // if (SETTINGS.controllerDevice == "XBOX")
            // {
        public void CaptureInputRawXbox()
        {
            playerInputRawArray = new List<PlayerInput>();
            PlayerInput A = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "A_xboxBtn",
                InputName = xboxMap.A_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("A"),
                InputHold = Input.GetButton("A"),
                InputUp = Input.GetButtonUp("A"),
                AltInputDown = Input.GetButtonDown("A"),
                AltInputHold = Input.GetButton("A"),
                AltInputUp = Input.GetButtonUp("A"),
            };
            PlayerInput B = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "B_xboxBtn",
                InputName = xboxMap.B_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("B"),
                InputHold = Input.GetButton("B"),
                InputUp = Input.GetButtonUp("B"),
                AltInputDown = Input.GetButtonDown("B"),
                AltInputHold = Input.GetButton("B"),
                AltInputUp = Input.GetButtonUp("B"),
            };
            PlayerInput X = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "X_xboxBtn",
                InputName = xboxMap.X_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("X"),
                InputHold = Input.GetButton("X"),
                InputUp = Input.GetButtonUp("X"),
                AltInputDown = Input.GetButtonDown("X"),
                AltInputHold = Input.GetButton("X"),
                AltInputUp = Input.GetButtonUp("X"),
            };
            PlayerInput Y = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "Y_xboxBtn",
                InputName = xboxMap.Y_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("Y"),
                InputHold = Input.GetButton("Y"),
                InputUp = Input.GetButtonUp("Y"),
                AltInputDown = Input.GetButtonDown("Y"),
                AltInputHold = Input.GetButton("Y"),
                AltInputUp = Input.GetButtonUp("Y"),
            };
            PlayerInput SELECT = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "SELECT_xboxBtn",
                InputName = xboxMap.SELECT_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("SELECT"),
                InputHold = Input.GetButton("SELECT"),
                InputUp = Input.GetButtonUp("SELECT"),
                AltInputDown = Input.GetButtonDown("SELECT"),
                AltInputHold = Input.GetButton("SELECT"),
                AltInputUp = Input.GetButtonUp("SELECT"),
            };
            PlayerInput START = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "START_xboxBtn",
                InputName = xboxMap.START_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("START"),
                InputHold = Input.GetButton("START"),
                InputUp = Input.GetButtonUp("START"),
                AltInputDown = Input.GetButtonDown("START"),
                AltInputHold = Input.GetButton("START"),
                AltInputUp = Input.GetButtonUp("START"),
            };
            PlayerInput LB = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "LB_xboxBtn",
                InputName = xboxMap.LB_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("LB"),
                InputHold = Input.GetButton("LB"),
                InputUp = Input.GetButtonUp("LB"),
                AltInputDown = Input.GetButtonDown("LB"),
                AltInputHold = Input.GetButton("LB"),
                AltInputUp = Input.GetButtonUp("LB"),
            };
            PlayerInput RB = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "RB_xboxBtn",
                InputName = xboxMap.RB_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("RB"),
                InputHold = Input.GetButton("RB"),
                InputUp = Input.GetButtonUp("RB"),
                AltInputDown = Input.GetButtonDown("RB"),
                AltInputHold = Input.GetButton("RB"),
                AltInputUp = Input.GetButtonUp("RB"),
            };
            PlayerInput LSIN = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "LSIN_xboxBtn",
                InputName = xboxMap.LSIN_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("LSIN"),
                InputHold = Input.GetButton("LSIN"),
                InputUp = Input.GetButtonUp("LSIN"),
                AltInputDown = Input.GetButtonDown("LSIN"),
                AltInputHold = Input.GetButton("LSIN"),
                AltInputUp = Input.GetButtonUp("LSIN"),
            };
            PlayerInput RSIN = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "RSIN_xboxBtn",
                InputName = xboxMap.RSIN_xboxBtn,
                InputAction = "default",
                InputDown = Input.GetButtonDown("RSIN"),
                InputHold = Input.GetButton("RSIN"),
                InputUp = Input.GetButtonUp("RSIN"),
                AltInputDown = Input.GetButtonDown("RSIN"),
                AltInputHold = Input.GetButton("RSIN"),
                AltInputUp = Input.GetButtonUp("RSIN"),
            };

            List<PlayerInput> playerInputArrayTriggerRaw = GetPlayerInputTriggerArrayRaw();
            playerInputArrayTriggerRaw.ForEach(piatr => 
            {
                playerInputRawArray.Add(piatr);
            });
            
            input = new Vector2(0,0);
            List<PlayerInput> playerInputArrayDirectionRaw = GetPlayerInputDirectionArrayRaw();
            playerInputArrayDirectionRaw.ForEach(piadr => 
            {
            playerInputRawArray.Add(piadr);

            if ((piadr.InputName == "UP") && (piadr.InputHold))
            {
                input.y = 1.0f;
            } 
            if ((piadr.InputName == "DOWN") && (piadr.InputHold))
            {
                input.y = -1.0f;
            } 
            if ((piadr.InputName == "RIGHT") && (piadr.InputHold))
            {
                input.x = 1.0f;
                
            } 
            if ((piadr.InputName == "LEFT") && (piadr.InputHold))
            {
                input.x = -1.0f;
            } 
            });
        
            playerInputRawArray.Add(A);
            playerInputRawArray.Add(B);
            playerInputRawArray.Add(X);
            playerInputRawArray.Add(Y);
            playerInputRawArray.Add(SELECT);
            playerInputRawArray.Add(START);
            playerInputRawArray.Add(LB);
            playerInputRawArray.Add(RB);
            playerInputRawArray.Add(LSIN);
            playerInputRawArray.Add(RSIN);

            // Take the SO for actionbindings and set it to the correct OPTION Mappings
            playerInputRawArray.ForEach(pira => {
                switch(pira.InputName)
                {
                    case "OPTION0":
                        pira.InputName = inventory.actionBindings.option0;
                        break;
                    case "OPTION1":
                        pira.InputName = inventory.actionBindings.option1;
                        break;
                    case "OPTION2":
                        pira.InputName = inventory.actionBindings.option2;
                        break;
                    case "OPTION3":
                        pira.InputName = inventory.actionBindings.option3;
                        break;
                    case "OPTION4":
                        pira.InputName = inventory.actionBindings.option4;
                        break;
                    case "OPTION5":
                        pira.InputName = inventory.actionBindings.option5;
                        break;
                }
            });

            // Disable Input 
            if(self.state.inputDisabled)
            {
                playerInputRawArray.Clear();
                input = Vector2.zero;
            }
            
            playerInputHistoryRaw.Add(playerInputRawArray);

            if(playerInputHistoryRaw.Count > _playerInputRawMax) {playerInputHistoryRaw.Remove(playerInputHistoryRaw.First());}

        }
            // else
        public void CaptureInputRawKeyboard()
        {
            playerInputRawArray = new List<PlayerInput>();
            PlayerInput A = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "A_key",
                InputName = keyboardMap.A_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.A_keycode),
                InputHold = Input.GetKey(keyboardMap.A_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.A_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.A_keycode),
                AltInputHold = Input.GetKey(keyboardMap.A_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.A_keycode),
            };
            PlayerInput B = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "B_key",
                InputName = keyboardMap.B_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.B_keycode),
                InputHold = Input.GetKey(keyboardMap.B_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.B_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.B_keycode),
                AltInputHold = Input.GetKey(keyboardMap.B_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.B_keycode),
            };
            PlayerInput X = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "X_key",
                InputName = keyboardMap.X_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.X_keycode),
                InputHold = Input.GetKey(keyboardMap.X_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.X_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.X_keycode),
                AltInputHold = Input.GetKey(keyboardMap.X_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.X_keycode),
            };
            PlayerInput Y = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "Y_key",
                InputName = keyboardMap.Y_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.Y_keycode),
                InputHold = Input.GetKey(keyboardMap.Y_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.Y_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.Y_keycode),
                AltInputHold = Input.GetKey(keyboardMap.Y_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.Y_keycode),
            };
            PlayerInput SELECT = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "SELECT_key",
                InputName = keyboardMap.SELECT_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.SELECT_keycode),
                InputHold = Input.GetKey(keyboardMap.SELECT_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.SELECT_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.SELECT_keycode),
                AltInputHold = Input.GetKey(keyboardMap.SELECT_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.SELECT_keycode),
            };
            PlayerInput START = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "START_key",
                InputName = keyboardMap.START_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.START_keycode),
                InputHold = Input.GetKey(keyboardMap.START_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.START_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.START_keycode),
                AltInputHold = Input.GetKey(keyboardMap.START_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.START_keycode),
            };
            PlayerInput LB = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "LB_key",
                InputName = keyboardMap.LB_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.LB_keycode),
                InputHold = Input.GetKey(keyboardMap.LB_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.LB_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.LB_keycode),
                AltInputHold = Input.GetKey(keyboardMap.LB_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.LB_keycode),
            };
            PlayerInput RB = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "RB_key",
                InputName = keyboardMap.RB_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.RB_keycode),
                InputHold = Input.GetKey(keyboardMap.RB_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.RB_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.RB_keycode),
                AltInputHold = Input.GetKey(keyboardMap.RB_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.RB_keycode),
            };
            PlayerInput LSIN = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "LSIN_xboxBtn",
                InputName = keyboardMap.LSIN_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.LSIN_keycode),
                InputHold = Input.GetKey(keyboardMap.LSIN_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.LSIN_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.LSIN_keycode),
                AltInputHold = Input.GetKey(keyboardMap.LSIN_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.LSIN_keycode),
            };
            PlayerInput RSIN = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "RSIN_key",
                InputName = keyboardMap.RSIN_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.RSIN_keycode),
                InputHold = Input.GetKey(keyboardMap.RSIN_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.RSIN_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.RSIN_keycode),
                AltInputHold = Input.GetKey(keyboardMap.RSIN_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.RSIN_keycode),
            };   
            PlayerInput LT = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "LT_key",
                InputName = keyboardMap.LT_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.LT_keycode),
                InputHold = Input.GetKey(keyboardMap.LT_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.LT_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.LT_keycode),
                AltInputHold = Input.GetKey(keyboardMap.LT_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.LT_keycode),
            };
            PlayerInput RT = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "RT_key",
                InputName = keyboardMap.RT_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.RT_keycode),
                InputHold = Input.GetKey(keyboardMap.RT_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.RT_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.RT_keycode),
                AltInputHold = Input.GetKey(keyboardMap.RT_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.RT_keycode),
            };
            PlayerInput UP = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "UP_key",
                InputName = keyboardMap.UP_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.UP_keycode),
                InputHold = Input.GetKey(keyboardMap.UP_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.UP_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.UP_keycode),
                AltInputHold = Input.GetKey(keyboardMap.UP_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.UP_keycode),
            };
            PlayerInput DOWN = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "DOWN_key",
                InputName = keyboardMap.DOWN_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.DOWN_keycode),
                InputHold = Input.GetKey(keyboardMap.DOWN_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.DOWN_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.DOWN_keycode),
                AltInputHold = Input.GetKey(keyboardMap.DOWN_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.DOWN_keycode),
            };
            PlayerInput LEFT = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "LEFT_key",
                InputName = keyboardMap.LEFT_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.LEFT_keycode),
                InputHold = Input.GetKey(keyboardMap.LEFT_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.LEFT_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.LEFT_keycode),
                AltInputHold = Input.GetKey(keyboardMap.LEFT_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.LEFT_keycode),
            };
            PlayerInput RIGHT = new PlayerInput(){
                InputFrameCount = Time.frameCount,
                InputType = "RIGHT_key",
                InputName = keyboardMap.RIGHT_key[1],
                InputAction = "default",
                InputDown = Input.GetKeyDown(keyboardMap.RIGHT_keycode),
                InputHold = Input.GetKey(keyboardMap.RIGHT_keycode),
                InputUp = Input.GetKeyUp(keyboardMap.RIGHT_keycode),
                AltInputDown = Input.GetKeyDown(keyboardMap.RIGHT_keycode),
                AltInputHold = Input.GetKey(keyboardMap.RIGHT_keycode),
                AltInputUp = Input.GetKeyUp(keyboardMap.RIGHT_keycode),
            };

            playerInputRawArray.Add(A);
            playerInputRawArray.Add(B);
            playerInputRawArray.Add(X);
            playerInputRawArray.Add(Y);
            playerInputRawArray.Add(SELECT);
            playerInputRawArray.Add(START);
            playerInputRawArray.Add(LB);
            playerInputRawArray.Add(RB);
            playerInputRawArray.Add(LSIN);
            playerInputRawArray.Add(RSIN);
            playerInputRawArray.Add(LT);
            playerInputRawArray.Add(RT);
            playerInputRawArray.Add(UP);
            playerInputRawArray.Add(DOWN);
            playerInputRawArray.Add(LEFT);
            playerInputRawArray.Add(RIGHT);
            
            playerInputRawArray.ForEach(pira => {
                switch(pira.InputName)
                {
                    case "OPTION0":
                        pira.InputName = inventory.actionBindings.option0;
                        break;
                    case "OPTION1":
                        pira.InputName = inventory.actionBindings.option1;
                        break;
                    case "OPTION2":
                        pira.InputName = inventory.actionBindings.option2;
                        break;
                    case "OPTION3":
                        pira.InputName = inventory.actionBindings.option3;
                        break;
                    case "OPTION4":
                        pira.InputName = inventory.actionBindings.option4;
                        break;
                    case "OPTION5":
                        pira.InputName = inventory.actionBindings.option5;
                        break;
                }
            });
            
            input = new Vector2(0,0);
            if (UP.InputHold)
            {
                input.y = 1.0f;
            } 
            if (DOWN.InputHold)
            {
                input.y = -1.0f;
            } 
            if (LEFT.InputHold)
            {
                input.x = -1.0f;
            } 
            if (RIGHT.InputHold)
            {
                input.x = 1.0f;
            } 

            if(self.state.inputDisabled)
            {
                playerInputRawArray.Clear();
                input = Vector2.zero;
            } 

            playerInputHistoryRaw.Add(playerInputRawArray);
            if(playerInputHistoryRaw.Count > _playerInputRawMax) {playerInputHistoryRaw.Remove(playerInputHistoryRaw.First());}
        }
        // }


        public void CaptureInputDown()
        {
            playerInputArray = new List<PlayerInput>();
            playerInputHistoryRaw.Last().ForEach(pinput => 
            {
                if(pinput.InputDown)
                {
                    pinput.InputAction = "down";
                    playerInputArray.Add(pinput);
                }
            });
            playerInputDownLastArray = playerInputArray;

            if(playerInputArray.Count > 0)
            {
                playerInputHistory.Add(playerInputArray);
                if(playerInputHistory.Count > _playerInputRawMax) 
                {
                    playerInputHistory.Remove(playerInputHistory.First());
                }
            }
        }

        public void CaptureInputHold()
        {
            playerInputHoldArray = new List<PlayerInput>();
            playerInputHistoryRaw.Last().ForEach(pinput => 
            {
                    if(pinput.InputHold)
                    {
                        pinput.InputAction = "hold";
                        playerInputHoldArray.Add(pinput);
                    }
            });
            playerInputHoldLastArray = playerInputHoldArray;

            if(playerInputHoldArray.Count > 0)
            {
                playerInputHoldHistory.Add(playerInputHoldArray);
                if(playerInputHoldHistory.Count > _playerInputRawMax) 
                {
                    playerInputHoldHistory.Remove(playerInputHoldHistory.First());
                }
            }
        }

        public void CaptureInputUp()
        {
            playerInputUpArray = new List<PlayerInput>();
            playerInputHistoryRaw.Last().ForEach(pinput => 
            {
                    if(pinput.InputUp)
                    {
                        pinput.InputAction = "up";
                        playerInputUpArray.Add(pinput);
                    }
            });

            playerInputUpLastArray = playerInputUpArray;

            if(playerInputUpArray.Count > 0)
            {
                playerInputUpHistory.Add(playerInputUpArray);
                if(playerInputUpHistory.Count > _playerInputRawMax) 
                {
                    playerInputUpHistory.Remove(playerInputUpHistory.First());
                }
            }
        }

        public void Capture()
        {
            captureInputRaw();
            CaptureInputDown();
            CaptureInputHold();
            CaptureInputUp();
        }

    public List<PlayerInput> GetPlayerInputTriggerArrayRaw() 
    {

        PlayerInput _playerInputTriggerLeft = new PlayerInput();
        PlayerInput _playerInputTriggerRight = new PlayerInput();
        List<PlayerInput> playerInputArrayTriggersRaw = new List<PlayerInput>();
        
        // if ((Input.GetAxisRaw("RTrigger") > 0))
        if ((Input.GetAxisRaw("RT") > 0))
            {
                // _leftTriggerButtonDown = false;
                // _leftTriggerButtonHeld = false;
                // _leftTriggerButtonUp = false;

                if (_rightTriggerButtonHeld)
                    {
                        _rightTriggerButtonDown = false;
                        _rightTriggerButtonHeld = true;
                        _rightTriggerButtonUp = false;
                        // _previousRight = true;
                    }
                else
                {
                    _rightTriggerButtonDown = true;
                    _rightTriggerButtonHeld = true;
                    _rightTriggerButtonUp = false;
                    // _previousRight = true;
                }
                _previousTriggerRight = true;
                
                _playerInputTriggerRight = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RT_xboxBtn",
                    InputName = xboxMap.RT_xboxBtn,
                    InputAction = "rtrigger",
                    InputDown = _rightTriggerButtonDown,
                    InputHold = true,
                    InputUp = false,
                    AltInputDown = _rightTriggerButtonDown,
                    AltInputHold = true,
                    AltInputUp = false
                };
                playerInputArrayTriggersRaw.Add(_playerInputTriggerRight);
            }

        // if ((Input.GetAxisRaw("LTrigger") > 0))
        if ((Input.GetAxisRaw("LT") > 0))
            {
                // _rightTriggerButtonDown = false;
                // _rightTriggerButtonHeld = false;
                // _rightTriggerButtonUp = false;

                if (_leftTriggerButtonHeld)
                    {
                        _leftTriggerButtonDown = false;
                        _leftTriggerButtonHeld = true;
                        _leftTriggerButtonUp = false;
                        // _previousRight = true;
                    }
                else
                {
                    _leftTriggerButtonDown = true;
                    _leftTriggerButtonHeld = true;
                    _leftTriggerButtonUp = false;
                    // _previousRight = true;
                }
                _previousTriggerLeft = true;
                
                _playerInputTriggerLeft = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LT_xboxBtn",
                    InputName = xboxMap.LT_xboxBtn,
                    InputAction = "ltrigger",
                    InputDown = _leftTriggerButtonDown,
                    InputHold = true,
                    InputUp = false,
                    AltInputDown = _leftTriggerButtonDown,
                    AltInputHold = true,
                    AltInputUp = false
                };
                playerInputArrayTriggersRaw.Add(_playerInputTriggerLeft);
            }

        // if ((Input.GetAxisRaw("RTrigger") == 0))
        if ((Input.GetAxisRaw("RT") == 0))
            {
                
                if(_noRightTriggerInput)
                {
                    _rightTriggerButtonUp = false;
                    _playerInputTriggerRight = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = "RT_xboxBtn",
                            InputName = xboxMap.RT_xboxBtn,
                            InputAction = "rtrigger",
                            InputDown = false,
                            InputHold = false,
                            InputUp = false,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = false
                        };
                    playerInputArrayTriggersRaw.Add(_playerInputTriggerRight);
                }
                else
                {
                    if(_previousTriggerRight) 
                    {
                        _previousTriggerRight = false;
                        _rightButtonUp = true;
                        _playerInputTriggerRight = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = "RT_xboxBtn",
                            InputName = xboxMap.RT_xboxBtn,
                            InputAction = "rtrigger",
                            InputDown = false,
                            InputHold = false,
                            InputUp = true,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = true
                        };
                        playerInputArrayTriggersRaw.Add(_playerInputTriggerRight);
                    }
                    _noRightTriggerInput = true;
                }
                _rightTriggerButtonDown = false;
                _rightTriggerButtonHeld = false;                           
            }
            else 
            {
                _noRightTriggerInput = false;
            }

        // if ((Input.GetAxisRaw("LTrigger") == 0))
        if ((Input.GetAxisRaw("LT") == 0))
            {
                if(_noLeftTriggerInput)
                {
                    _leftTriggerButtonUp = false;
                    _playerInputTriggerLeft = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = "LT_xboxBtn",
                            InputName = xboxMap.LT_xboxBtn,
                            InputAction = "ltrigger",
                            InputDown = false,
                            InputHold = false,
                            InputUp = false,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = false
                        };
                    playerInputArrayTriggersRaw.Add(_playerInputTriggerLeft);
                }
                else
                {
                    if(_previousTriggerLeft) 
                    {
                        _previousTriggerLeft = false;
                        _leftButtonUp = true;
                        _playerInputTriggerLeft = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = "LT_xboxBtn",
                            InputName = xboxMap.LT_xboxBtn,
                            InputAction = "ltrigger",
                            InputDown = false,
                            InputHold = false,
                            InputUp = true,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = true
                        };
                        playerInputArrayTriggersRaw.Add(_playerInputTriggerLeft);
                    }
                    _noLeftTriggerInput = true;
                }
                _leftTriggerButtonDown = false;
                _leftTriggerButtonHeld = false;            
            }
            else 
            {
                _noLeftTriggerInput = false;
            }
        return playerInputArrayTriggersRaw;
    }

        public List<PlayerInput> GetPlayerInputDirectionArrayRaw() 
        {       
            PlayerInput _playerInputDirectionUp = new PlayerInput();
            PlayerInput _playerInputDirectionRight = new PlayerInput();
            PlayerInput _playerInputDirectionDown = new PlayerInput();
            PlayerInput _playerInputDirectionLeft = new PlayerInput();
            List<PlayerInput> playerInputArrayDirectionRaw = new List<PlayerInput>();
            
            string _InputTypeX = "default";
            string _InputTypeY = "default";

            if ((Input.GetAxisRaw("LSX") > 0.01) || (Input.GetAxis("DPADX") > 0) || (Input.GetAxis("RSX") > 0))
            {
                if ((Input.GetAxisRaw("LSX") > 0.01))
                {
                    _InputTypeX = xboxMap.LSX_xboxBtn;
                }
                else if ((Input.GetAxis("DPADX") > 0))
                {
                    _InputTypeX = xboxMap.DPADX_xboxBtn;
                }
                else
                {
                    _InputTypeX = xboxMap.RSX_xboxBtn;
                }
                
                
                _leftButtonDown = false;
                _leftButtonHeld = false;
                _leftButtonUp = false;

                if (_rightButtonHeld)
                    {
                        _rightButtonDown = false;
                        _rightButtonHeld = true;
                        _rightButtonUp = false;
                        // _previousRight = true;
                    }
                else
                {
                    _rightButtonDown = true;
                    _rightButtonHeld = true;
                    _rightButtonUp = false;
                    // _previousRight = true;
                }
                _previousRight = true;
                
                _playerInputDirectionRight = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = _InputTypeX,
                    InputName = "RIGHT",
                    InputAction = "RIGHT",
                    InputDown = _rightButtonDown,
                    InputHold = true,
                    InputUp = false,
                    AltInputDown = _rightButtonDown,
                    AltInputHold = true,
                    AltInputUp = false
                };
                playerInputArrayDirectionRaw.Add(_playerInputDirectionRight);
            }

            if ((Input.GetAxisRaw("LSX") < -0.01) || (Input.GetAxis("DPADX") < 0) || (Input.GetAxis("RSX") < 0)) 
            {
                if ((Input.GetAxisRaw("LSX") < 0.01))
                {
                    _InputTypeX = xboxMap.LSX_xboxBtn;
                }
                else if ((Input.GetAxis("DPADX") < 0))
                {
                    _InputTypeX = xboxMap.DPADX_xboxBtn;
                }
                else
                {
                    _InputTypeX = xboxMap.RSX_xboxBtn;
                }
                
                _rightButtonDown = false;
                _rightButtonHeld = false;
                _rightButtonUp = false;


                if(_leftButtonHeld)
                {
                    _leftButtonDown = false;
                    _leftButtonHeld = true;
                    _leftButtonUp = false;
                    // _previousLeft = true;
                }
                else{
                    _leftButtonDown = true;
                    _leftButtonHeld = true;
                    _leftButtonUp = false;
                    // _previousLeft = true;
                }
                _previousLeft = true;
                
                _playerInputDirectionLeft = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = _InputTypeX,
                    InputName = "LEFT",
                    InputAction = "LEFT",
                    InputDown = _leftButtonDown,
                    InputHold = true,
                    InputUp = false,
                    AltInputDown = _leftButtonDown,
                    AltInputHold = true,
                    AltInputUp = false
                };
                playerInputArrayDirectionRaw.Add(_playerInputDirectionLeft);
            }

            if ((Input.GetAxisRaw("LSX") == 0) && (Input.GetAxis("DPADX") == 0) && (Input.GetAxis("RSX") == 0))
            {
                if(_noHorizontalInput)
                {
                    _rightButtonUp = false;
                    _leftButtonUp = false;
                    _playerInputDirectionRight = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = _InputTypeX,
                            InputName = "RIGHT",
                            InputAction = "RIGHT",
                            InputDown = false,
                            InputHold = false,
                            InputUp = false,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = false
                        };
                    _playerInputDirectionLeft = new PlayerInput(){
                        InputFrameCount = Time.frameCount,
                        InputType = _InputTypeX,
                        InputName = "LEFT",
                        InputAction = "LEFT",
                        InputDown = false,
                        InputHold = false,
                        InputUp = false,
                        AltInputDown = false,
                        AltInputHold = false,
                        AltInputUp = false
                    };
                    playerInputArrayDirectionRaw.Add(_playerInputDirectionRight);
                    playerInputArrayDirectionRaw.Add(_playerInputDirectionLeft);
                }
                else
                {
                    if(_previousRight) 
                    {
                        _previousRight = false;
                        _rightButtonUp = true;
                        _playerInputDirectionRight = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = _InputTypeX,
                            InputName = "RIGHT",
                            InputAction = "RIGHT",
                            InputDown = false,
                            InputHold = false,
                            InputUp = true,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = true
                        };
                        playerInputArrayDirectionRaw.Add(_playerInputDirectionRight);
                    }
                    if(_previousLeft) 
                    {
                        _leftButtonUp = true;
                        _previousLeft = false;
                        _playerInputDirectionLeft = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = _InputTypeX,
                            InputName = "LEFT",
                            InputAction = "LEFT",
                            InputDown = false,
                            InputHold = false,
                            InputUp = true,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = true
                        };
                        playerInputArrayDirectionRaw.Add(_playerInputDirectionLeft);
                        }
                    _noHorizontalInput = true;
                }
                _rightButtonDown = false;
                _rightButtonHeld = false;            
                _leftButtonDown = false;
                _leftButtonHeld = false;            
            }
            else 
            {
                _noHorizontalInput = false;
            }
            
            if ((Input.GetAxisRaw("LSY") > 0.01) || (Input.GetAxis("DPADY") > 0) || (Input.GetAxis("RSY") > 0))
            {
                if ((Input.GetAxisRaw("LSY") > 0.01))
                {
                    _InputTypeY = xboxMap.LSY_xboxBtn;
                }
                else if ((Input.GetAxis("DPADY") > 0))
                {
                    _InputTypeY = xboxMap.DPADY_xboxBtn;
                }
                else
                {
                    _InputTypeY = xboxMap.RSY_xboxBtn;
                }

                _downButtonDown = false;
                _downButtonHeld = false;
                _downButtonUp = false;

                if (_upButtonHeld)
                    {
                        _upButtonDown = false;
                        _upButtonHeld = true;
                        _upButtonUp = false;
                        _previousUp = true;
                    }
                else
                {
                    _upButtonDown = true;
                    _upButtonHeld = true;
                    _upButtonUp = false;
                    _previousUp = true;
                }
                _playerInputDirectionUp = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = _InputTypeY,
                    InputName = "UP",
                    InputAction = "UP",
                    InputDown = _upButtonDown,
                    InputHold = true,
                    InputUp = false,
                    AltInputDown = _upButtonDown,
                    AltInputHold = true,
                    AltInputUp = false
                };
                playerInputArrayDirectionRaw.Add(_playerInputDirectionUp);
            }

            if ((Input.GetAxisRaw("LSY") < -0.01) || (Input.GetAxis("DPADY") < 0) || (Input.GetAxis("RSY") < 0))
            {
                if ((Input.GetAxisRaw("LSY") < 0.01))
                {
                    _InputTypeX = xboxMap.LSY_xboxBtn;
                }
                else if ((Input.GetAxis("DPADY") < 0))
                {
                    _InputTypeX = xboxMap.DPADY_xboxBtn;
                }
                else
                {
                    _InputTypeX = xboxMap.RSY_xboxBtn;
                }

                
                _upButtonDown = false;
                _upButtonHeld = false;
                _upButtonUp = false;

                if(_downButtonHeld)
                {
                    _downButtonDown = false;
                    _downButtonHeld = true;
                    _downButtonUp = false;
                    _previousDown = true;
                }
                else{
                    _downButtonDown = true;
                    _downButtonHeld = true;
                    _downButtonUp = false;
                    _previousDown = true;
                }
                _playerInputDirectionDown = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = _InputTypeY,
                    InputName = "DOWN",
                    InputAction = "DOWN",
                    InputDown = _downButtonDown,
                    InputHold = true,
                    InputUp = false,
                    AltInputDown = _downButtonDown,
                    AltInputHold = true,
                    AltInputUp = false
                };
                playerInputArrayDirectionRaw.Add(_playerInputDirectionDown);
            }

            if ((Input.GetAxisRaw("LSY") == 0) && (Input.GetAxis("DPADY") == 0) && (Input.GetAxis("RSY") == 0))
            {
                if(_noVerticalInput)
                {
                    _upButtonUp = false;
                    _downButtonUp = false;

                    _playerInputDirectionUp = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = _InputTypeY,
                            InputName = "UP",
                            InputAction = "UP",
                            InputDown = false,
                            InputHold = false,
                            InputUp = false,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = false
                        };
                    _playerInputDirectionDown = new PlayerInput(){
                        InputFrameCount = Time.frameCount,
                        InputType = _InputTypeY,
                        InputName = "DOWN",
                        InputAction = "DOWN",
                        InputDown = false,
                        InputHold = false,
                        InputUp = false,
                        AltInputDown = false,
                        AltInputHold = false,
                        AltInputUp = false
                    };
                    playerInputArrayDirectionRaw.Add(_playerInputDirectionUp);
                    playerInputArrayDirectionRaw.Add(_playerInputDirectionDown);
                }
                else
                {
                    if(_previousUp) 
                    {
                        _upButtonUp = true;
                        _previousUp = false;
                        _playerInputDirectionUp = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = _InputTypeY,
                            InputName = "UP",
                            InputAction = "UP",
                            InputDown = false,
                            InputHold = false,
                            InputUp = true,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = true
                        };
                        playerInputArrayDirectionRaw.Add(_playerInputDirectionUp);
                    }
                    if(_previousDown) 
                    {
                        _downButtonUp = true;
                        _previousDown = false;
                        _playerInputDirectionDown = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = _InputTypeY,
                            InputName = "DOWN",
                            InputAction = "DOWN",
                            InputDown = false,
                            InputHold = false,
                            InputUp = true,
                            AltInputDown = false,
                            AltInputHold = false,
                            AltInputUp = true
                        };
                        playerInputArrayDirectionRaw.Add(_playerInputDirectionDown);
                    }
                    _noVerticalInput = true;
                }
                _upButtonDown = false;
                _upButtonHeld = false;            
                _downButtonDown = false;
                _downButtonHeld = false;            
            }
            else 
            {
                _noVerticalInput = false;
            }
        
            return playerInputArrayDirectionRaw;
        }
    }
}