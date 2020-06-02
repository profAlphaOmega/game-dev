using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Player2
{
    public class InputManager : MonoBehaviour 
    {
        public Main self;
        public List<PlayerInput> playerInputDownArray;
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
            self = GetComponent<Main>();
            if(SETTINGS.controllerDevice2 != "XBOX")
            {
                SETTINGS.SetKeyboardMapping();
            }
            playerInputDownLastArray = new List<PlayerInput>();
            playerInputHoldLastArray = new List<PlayerInput>();
            playerInputUpLastArray = new List<PlayerInput>();
        }
    public bool LastInputDown(string s)
    {
        bool c = false;
        playerInputDownLastArray.ForEach(pila => {
            if(pila.InputName == s) c = true;
        });
        if(self.playerState.inputDisabled) c = false;
        return c;
    }
    
    public bool LastInputHold(string s)
    {
        bool c = false;
        playerInputHoldLastArray.ForEach(pila => {
            if(pila.InputName == s) c = true;
        });
        if(self.playerState.inputDisabled) c = false;
        return c;
    }
    
    public bool LastInputUp(string s)
    {
        bool c = false;
        playerInputUpLastArray.ForEach(pila => {
            if(pila.InputName == s) c = true;
        });
        if(self.playerState.inputDisabled) c = false;
        return c;
    }


        public Vector2 CaptureInputRaw()
        {
            if (SETTINGS.controllerDevice2 == "XBOX")
            {
                playerInputRawArray = new List<PlayerInput>();
                PlayerInput A = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "A_xboxBtn2",
                    InputName = SETTINGS.A_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2A"),
                    InputHold = Input.GetButton("2A"),
                    InputUp = Input.GetButtonUp("2A"),
                    AltInputDown = Input.GetButtonDown("2A"),
                    AltInputHold = Input.GetButton("2A"),
                    AltInputUp = Input.GetButtonUp("2A"),
                };
                PlayerInput B = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "B_xboxBtn2",
                    InputName = SETTINGS.B_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2B"),
                    InputHold = Input.GetButton("2B"),
                    InputUp = Input.GetButtonUp("2B"),
                    AltInputDown = Input.GetButtonDown("2B"),
                    AltInputHold = Input.GetButton("2B"),
                    AltInputUp = Input.GetButtonUp("2B"),
                };
                PlayerInput X = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "X_xboxBtn2",
                    InputName = SETTINGS.X_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2X"),
                    InputHold = Input.GetButton("2X"),
                    InputUp = Input.GetButtonUp("2X"),
                    AltInputDown = Input.GetButtonDown("2X"),
                    AltInputHold = Input.GetButton("2X"),
                    AltInputUp = Input.GetButtonUp("2X"),
                };
                PlayerInput Y = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "Y_xboxBtn2",
                    InputName = SETTINGS.Y_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2Y"),
                    InputHold = Input.GetButton("2Y"),
                    InputUp = Input.GetButtonUp("2Y"),
                    AltInputDown = Input.GetButtonDown("2Y"),
                    AltInputHold = Input.GetButton("2Y"),
                    AltInputUp = Input.GetButtonUp("2Y"),
                };
                PlayerInput SELECT = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "SELECT_xboxBtn2",
                    InputName = SETTINGS.SELECT_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2SELECT"),
                    InputHold = Input.GetButton("2SELECT"),
                    InputUp = Input.GetButtonUp("2SELECT"),
                    AltInputDown = Input.GetButtonDown("2SELECT"),
                    AltInputHold = Input.GetButton("2SELECT"),
                    AltInputUp = Input.GetButtonUp("2SELECT"),
                };
                PlayerInput START = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "START_xboxBtn2",
                    InputName = SETTINGS.START_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2START"),
                    InputHold = Input.GetButton("2START"),
                    InputUp = Input.GetButtonUp("2START"),
                    AltInputDown = Input.GetButtonDown("2START"),
                    AltInputHold = Input.GetButton("2START"),
                    AltInputUp = Input.GetButtonUp("2START"),
                };
                PlayerInput LB = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LB_xboxBtn2",
                    InputName = SETTINGS.LB_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2LB"),
                    InputHold = Input.GetButton("2LB"),
                    InputUp = Input.GetButtonUp("2LB"),
                    AltInputDown = Input.GetButtonDown("2LB"),
                    AltInputHold = Input.GetButton("2LB"),
                    AltInputUp = Input.GetButtonUp("2LB"),
                };
                PlayerInput RB = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RB_xboxBtn2",
                    InputName = SETTINGS.RB_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2RB"),
                    InputHold = Input.GetButton("2RB"),
                    InputUp = Input.GetButtonUp("2RB"),
                    AltInputDown = Input.GetButtonDown("2RB"),
                    AltInputHold = Input.GetButton("2RB"),
                    AltInputUp = Input.GetButtonUp("2RB"),
                };
                PlayerInput LSIN = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LSIN_xboxBtn2",
                    InputName = SETTINGS.LSIN_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2LSIN"),
                    InputHold = Input.GetButton("2LSIN"),
                    InputUp = Input.GetButtonUp("2LSIN"),
                    AltInputDown = Input.GetButtonDown("2LSIN"),
                    AltInputHold = Input.GetButton("2LSIN"),
                    AltInputUp = Input.GetButtonUp("2LSIN"),
                };
                PlayerInput RSIN = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RSIN_xboxBtn2",
                    InputName = SETTINGS.RSIN_xboxBtn2,
                    InputAction = "default",
                    InputDown = Input.GetButtonDown("2RSIN"),
                    InputHold = Input.GetButton("2RSIN"),
                    InputUp = Input.GetButtonUp("2RSIN"),
                    AltInputDown = Input.GetButtonDown("2RSIN"),
                    AltInputHold = Input.GetButton("2RSIN"),
                    AltInputUp = Input.GetButtonUp("2RSIN"),
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

                playerInputRawArray.ForEach(pira => {
                    switch(pira.InputName)
                    {
                        case "OPTION0":
                            pira.InputName = INVENTORY_P2.option0;
                            break;
                        case "OPTION1":
                            pira.InputName = INVENTORY_P2.option1;
                            break;
                        case "OPTION2":
                            pira.InputName = INVENTORY_P2.option2;
                            break;
                        case "OPTION3":
                            pira.InputName = INVENTORY_P2.option3;
                            break;
                        case "OPTION4":
                            pira.InputName = INVENTORY_P2.option4;
                            break;
                        case "OPTION5":
                            pira.InputName = INVENTORY_P2.option5;
                            break;
                    }
                });

                // Disable Input 
                if(self.playerState.inputDisabled)
                {
                    playerInputRawArray.Clear();
                    input = Vector2.zero;
                }


                playerInputHistoryRaw.Add(playerInputRawArray);

                if(playerInputHistoryRaw.Count > _playerInputRawMax) {playerInputHistoryRaw.Remove(playerInputHistoryRaw.First());}

            }
            else
            {
                playerInputRawArray = new List<PlayerInput>();
                PlayerInput A = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "A_key",
                    InputName = SETTINGS.A_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.A_keycode),
                    InputHold = Input.GetKey(SETTINGS.A_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.A_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.A_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.A_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.A_keycode),
                };
                PlayerInput B = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "B_key",
                    InputName = SETTINGS.B_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.B_keycode),
                    InputHold = Input.GetKey(SETTINGS.B_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.B_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.B_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.B_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.B_keycode),
                };
                PlayerInput X = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "X_key",
                    InputName = SETTINGS.X_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.X_keycode),
                    InputHold = Input.GetKey(SETTINGS.X_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.X_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.X_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.X_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.X_keycode),
                };
                PlayerInput Y = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "Y_key",
                    InputName = SETTINGS.Y_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.Y_keycode),
                    InputHold = Input.GetKey(SETTINGS.Y_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.Y_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.Y_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.Y_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.Y_keycode),
                };
                PlayerInput SELECT = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "SELECT_key",
                    InputName = SETTINGS.SELECT_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.SELECT_keycode),
                    InputHold = Input.GetKey(SETTINGS.SELECT_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.SELECT_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.SELECT_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.SELECT_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.SELECT_keycode),
                };
                PlayerInput START = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "START_key",
                    InputName = SETTINGS.START_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.START_keycode),
                    InputHold = Input.GetKey(SETTINGS.START_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.START_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.START_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.START_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.START_keycode),
                };
                PlayerInput LB = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LB_key",
                    InputName = SETTINGS.LB_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.LB_keycode),
                    InputHold = Input.GetKey(SETTINGS.LB_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.LB_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.LB_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.LB_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.LB_keycode),
                };
                PlayerInput RB = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RB_key",
                    InputName = SETTINGS.RB_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.RB_keycode),
                    InputHold = Input.GetKey(SETTINGS.RB_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.RB_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.RB_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.RB_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.RB_keycode),
                };
                PlayerInput LSIN = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LSIN_xboxBtn2",
                    InputName = SETTINGS.LSIN_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.LSIN_keycode),
                    InputHold = Input.GetKey(SETTINGS.LSIN_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.LSIN_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.LSIN_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.LSIN_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.LSIN_keycode),
                };
                PlayerInput RSIN = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RSIN_key",
                    InputName = SETTINGS.RSIN_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.RSIN_keycode),
                    InputHold = Input.GetKey(SETTINGS.RSIN_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.RSIN_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.RSIN_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.RSIN_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.RSIN_keycode),
                };   
                PlayerInput LT = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LT_key",
                    InputName = SETTINGS.LT_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.LT_keycode),
                    InputHold = Input.GetKey(SETTINGS.LT_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.LT_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.LT_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.LT_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.LT_keycode),
                };
                PlayerInput RT = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RT_key",
                    InputName = SETTINGS.RT_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.RT_keycode),
                    InputHold = Input.GetKey(SETTINGS.RT_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.RT_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.RT_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.RT_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.RT_keycode),
                };
                PlayerInput UP = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "UP_key",
                    InputName = SETTINGS.UP_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.UP_keycode),
                    InputHold = Input.GetKey(SETTINGS.UP_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.UP_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.UP_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.UP_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.UP_keycode),
                };
                PlayerInput DOWN = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "DOWN_key",
                    InputName = SETTINGS.DOWN_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.DOWN_keycode),
                    InputHold = Input.GetKey(SETTINGS.DOWN_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.DOWN_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.DOWN_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.DOWN_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.DOWN_keycode),
                };
                PlayerInput LEFT = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "LEFT_key",
                    InputName = SETTINGS.LEFT_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.LEFT_keycode),
                    InputHold = Input.GetKey(SETTINGS.LEFT_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.LEFT_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.LEFT_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.LEFT_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.LEFT_keycode),
                };
                PlayerInput RIGHT = new PlayerInput(){
                    InputFrameCount = Time.frameCount,
                    InputType = "RIGHT_key",
                    InputName = SETTINGS.RIGHT_key[1],
                    InputAction = "default",
                    InputDown = Input.GetKeyDown(SETTINGS.RIGHT_keycode),
                    InputHold = Input.GetKey(SETTINGS.RIGHT_keycode),
                    InputUp = Input.GetKeyUp(SETTINGS.RIGHT_keycode),
                    AltInputDown = Input.GetKeyDown(SETTINGS.RIGHT_keycode),
                    AltInputHold = Input.GetKey(SETTINGS.RIGHT_keycode),
                    AltInputUp = Input.GetKeyUp(SETTINGS.RIGHT_keycode),
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
                            pira.InputName = INVENTORY_P2.option0;
                            break;
                        case "OPTION1":
                            pira.InputName = INVENTORY_P2.option1;
                            break;
                        case "OPTION2":
                            pira.InputName = INVENTORY_P2.option2;
                            break;
                        case "OPTION3":
                            pira.InputName = INVENTORY_P2.option3;
                            break;
                        case "OPTION4":
                            pira.InputName = INVENTORY_P2.option4;
                            break;
                        case "OPTION5":
                            pira.InputName = INVENTORY_P2.option5;
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

                // Disable Input 
                if(self.playerState.inputDisabled)
                {
                    playerInputRawArray.Clear();
                    input = Vector2.zero;
                }

                playerInputHistoryRaw.Add(playerInputRawArray);
                if(playerInputHistoryRaw.Count > _playerInputRawMax) {playerInputHistoryRaw.Remove(playerInputHistoryRaw.First());}
            }

            return input;
        }


        public void CaptureInputDown()
        {
            playerInputDownArray = new List<PlayerInput>();
            playerInputHistoryRaw.Last().ForEach(pinput => 
            {
                if(pinput.InputDown)
                {
                    pinput.InputAction = "down";
                    playerInputDownArray.Add(pinput);
                }
            });
            playerInputDownLastArray = playerInputDownArray;

            if(playerInputDownArray.Count > 0)
            {
                playerInputHistory.Add(playerInputDownArray);
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

    public List<PlayerInput> GetPlayerInputTriggerArrayRaw() 
        {

        PlayerInput _playerInputTriggerLeft = new PlayerInput();
        PlayerInput _playerInputTriggerRight = new PlayerInput();
        List<PlayerInput> playerInputArrayTriggersRaw = new List<PlayerInput>();
        
        if ((Input.GetAxisRaw("2RT") > 0))
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
                    InputType = "RT_xboxBtn2",
                    InputName = SETTINGS.RT_xboxBtn2,
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

        if ((Input.GetAxisRaw("2LT") > 0))
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
                    InputType = "LT_xboxBtn2",
                    InputName = SETTINGS.LT_xboxBtn2,
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

        if ((Input.GetAxisRaw("2RT") == 0))
            {
                if(_noRightTriggerInput)
                {
                    _rightTriggerButtonUp = false;
                    _playerInputTriggerRight = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = "RT_xboxBtn2",
                            InputName = SETTINGS.RT_xboxBtn2,
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
                            InputType = "RT_xboxBtn2",
                            InputName = SETTINGS.RT_xboxBtn2,
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

        if ((Input.GetAxisRaw("2LT") == 0))
            {
                if(_noLeftTriggerInput)
                {
                    _leftTriggerButtonUp = false;
                    _playerInputTriggerLeft = new PlayerInput(){
                            InputFrameCount = Time.frameCount,
                            InputType = "LT_xboxBtn2",
                            InputName = SETTINGS.LT_xboxBtn2,
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
                            InputType = "LT_xboxBtn2",
                            InputName = SETTINGS.LT_xboxBtn2,
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

            if ((Input.GetAxisRaw("2LSX") > 0.01) || (Input.GetAxis("2DPADX") > 0) || (Input.GetAxis("2RSX") > 0))
            {
                if ((Input.GetAxisRaw("2LSX") > 0.01))
                {
                    _InputTypeX = SETTINGS.LSX_xboxBtn2;
                }
                else if ((Input.GetAxis("2DPADX") > 0))
                {
                    _InputTypeX = SETTINGS.DPADX_xboxBtn2;
                }
                else
                {
                    _InputTypeX = SETTINGS.RSX_xboxBtn2;
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

            if ((Input.GetAxisRaw("2LSX") < -0.01) || (Input.GetAxis("2DPADX") < 0) || (Input.GetAxis("2RSX") < 0))  
            {
                if ((Input.GetAxisRaw("2LSX") < 0.01))
                {
                    _InputTypeX = SETTINGS.LSX_xboxBtn2;
                }
                else if ((Input.GetAxis("2DPADX") < 0))
                {
                    _InputTypeX = SETTINGS.DPADX_xboxBtn2;
                }
                else
                {
                    _InputTypeX = SETTINGS.RSX_xboxBtn2;
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

            if ((Input.GetAxisRaw("2LSX") == 0) && (Input.GetAxis("2DPADX") == 0) && (Input.GetAxis("2RSX") == 0))
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

            if ((Input.GetAxisRaw("2LSY") > 0.01) || (Input.GetAxis("2DPADY") > 0) || (Input.GetAxis("2RSY") > 0))
            {
                if ((Input.GetAxisRaw("2LSY") > 0.01))
                {
                    _InputTypeY = SETTINGS.LSY_xboxBtn2;
                }
                else if ((Input.GetAxis("2DPADY") > 0))
                {
                    _InputTypeY = SETTINGS.DPADY_xboxBtn2;
                }
                else
                {
                    _InputTypeY = SETTINGS.RSY_xboxBtn2;
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

            if ((Input.GetAxisRaw("2LSY") < -0.01) || (Input.GetAxis("2DPADY") < 0) || (Input.GetAxis("2RSY") < 0))
            {
                if ((Input.GetAxisRaw("2LSY") < 0.01))
                {
                    _InputTypeX = SETTINGS.LSY_xboxBtn2;
                }
                else if ((Input.GetAxis("2DPADY") < 0))
                {
                    _InputTypeX = SETTINGS.DPADY_xboxBtn2;
                }
                else
                {
                    _InputTypeX = SETTINGS.RSY_xboxBtn2;
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

            if ((Input.GetAxisRaw("2LSY") == 0) && (Input.GetAxis("2DPADY") == 0) && (Input.GetAxis("2RSY") == 0))
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
