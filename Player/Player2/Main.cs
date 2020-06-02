using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Player2
{
    [RequireComponent(typeof(Player.Controller2D))]
    public class Main : MonoBehaviour
    {
        Player.Controller2D controller;
        Player.Attack attack;
        public PlayerState playerState;
        public Player.SunFlare sunFlare;
        Animator Animator;
        BoxCollider2D Collider;
        GameObject SunFlare;
        public InputManager InputManager;

        public Vector2 velocity;
        public Vector2 input;
        Vector2 StandingSize = new Vector2(0.23f, 0.47f);
        Vector2 StandingOffset = new Vector2(0, 0);
        Vector2 CrouchingSize = new Vector2(0.22f, 0.15f);
        Vector2 CrouchingOffset = new Vector2(0, -0.16f);

        private float moveSpeed = 3;
        public bool facingRight = true;
        public float dashVelocity = 10;
        private float velocityXSmoothing;
        private float crouchSmoothing;
        private float gravity = 0;
        private float shortGravity = 0;
        
        public float jumpHeight = 2.4f;
        public float timeToJumpApex = .50f;
        public float shortJumpHeight = 1.0f;
        public float shortTimeToJumpApex = .35f;
        public int jumpsMax = 100;
        public int jumps;
        private float accelerationTimeAirborne = .05f;
        private float accelerationTimeGrounded = .05f;
        private float jumpVelocity;
        private float shortJumpVelocity;
        private int iJumpFrames = 0;
        private int iJumpFramesDiff = 0;

        public class PlayerState
        {
            public bool backdash;
            public bool frontdash;
            public bool jump;
            public bool grounded;
            public bool falling;
            public bool crouch;
            public bool zip;
            public int lookDirection;
            public bool inputDisabled;

            public void Reset()
            {
                backdash =  false;
                frontdash = false;
                jump = false;
                grounded = false;
                falling = false;
                crouch = false;
                zip = false;
                lookDirection = 0;
            }
            public void Set(string n, bool v)
            {
                switch (n)
                {
                    default:
                        break;
                    case "jump":
                        jump = v;
                        break;
                    case "backdash":
                        backdash = v;
                        break;
                    case "frontdash":
                        frontdash = v;
                        break;
                    case "grounded":
                        grounded = v;
                        break;
                    case "falling":
                        falling = v;
                        break;
                    case "crouch":
                        crouch = v;
                        break;
                    case "zip":
                        zip = v;
                        break;
                }
            }
        }
        

        private KeyCombo DashRight = new KeyCombo(new string[] {"RIGHT","RIGHT"}, new int[] {5}, new int[] {10});
        private KeyCombo DashLeft = new KeyCombo(new string[] {"LEFT","LEFT"}, new int[] {5}, new int[] {10});
        private KeyCombo DashUp = new KeyCombo(new string[] {"UP","UP"}, new int[] {5}, new int[] {10});
        private KeyCombo DashDown = new KeyCombo(new string[] {"DOWN","DOWN"}, new int[] {5}, new int[] {10});
        private KeyCombo GLaunch = new KeyCombo(new string[] {"DOWN","UP"}, new int[] {5}, new int[] {10});

        public float wallSlideSpeedMax = 1; 
        public Vector2 wallJumpClimb = new Vector2(7.5f,16f);
        public Vector2 wallJumpOff = new Vector2(8.5f,7f);
        public Vector2 wallJumpLeap = new Vector2(18f,17f);
        public int wallDirX;
        public float wallStickTime = 50.0f;
        public float timeToWallUnstick = 0f;
        public bool wallSliding = false;
        bool holdL = false;
        bool holdR = false;
        


        void Start()
        {
            controller = GetComponent<Player.Controller2D>();
            attack = GetComponent<Player.Attack>();
            Animator = GetComponent<Animator>();
            Collider = GetComponent<BoxCollider2D>();
            InputManager = GetComponent<InputManager>();
            gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2); // Calculate gravity
            jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex; // Calculate jump velocity
            shortGravity = -(2 * shortJumpHeight) / Mathf.Pow(shortTimeToJumpApex, 2);
            shortJumpVelocity = Mathf.Abs(shortGravity) * shortTimeToJumpApex;
            
            jumps = jumpsMax; // Set max jumps
            playerState = new PlayerState();
            playerState.Reset();
        }

        void Update()
        {
            // InputManager
            input = InputManager.CaptureInputRaw();
            InputManager.CaptureInputDown();
            InputManager.CaptureInputHold();
            InputManager.CaptureInputUp();
            
            PlayerStateLook();
            // Vertical Collision Check
            CollisionCheck();
            
            // if (Input.GetButtonUp("SunFlare"))
            // {
            //     Animator.SetBool("sunflare", false);
            // }
            if(InputManager.playerInputHistory.Count > 0)
            {
                KeyCombo();
            }
            // Jump Check
            Jump();
            FastFall();
            // Dash Check
            Zip();
            LeftDash();
            RightDash();
            Crouch();
            Attack();
            s_SunFlare();
            // BackDash();
            // FrontDash();
            
            // Moving
            Move(input, moveSpeed, ref velocity);

            // Falling Check, might be able to set somewhere else
            if (Mathf.Sign(velocity.y) == -1 && !playerState.grounded)
            {
                playerState.Set("falling", true);
            }

            // Set Speed Parameter for Animator
            if (!Animator.GetBool("Backdash"))
            {
                Animator.SetFloat("Speed", Mathf.Abs(velocity.x));
            }
        }

        void FastFall()
        {
            if(playerState.falling && InputManager.LastInputDown("DOWN"))
            {
                velocity.y -= 6;
            }
        }


        void Attack()
        {
            if(InputManager.LastInputDown("ATTACK"))
            {
                Debug.Log("Attack");
                attack.Enable("SWORDATTACK");
            }
        }
        void s_SunFlare()
        {
            if (InputManager.LastInputDown("SUNFLARE"))
            {
                Animator.Play("SunFlare");
            }
            if (InputManager.LastInputHold("SUNFLARE"))
            {
                sunFlare.Emit(playerState.lookDirection);
            }
        }

        void Crouch()
        {   
            if (InputManager.LastInputHold("DOWN") && playerState.grounded)
            {
                Collider.size = CrouchingSize;
                Collider.offset = CrouchingOffset;
                Animator.SetBool("crouch", true); 
                playerState.Set("crouch", true);
                input.x = 0; 
            }
            // if (!Input.GetButton("Option") && transform.localScale.y != 1f)
            if (!InputManager.LastInputHold("DOWN") && Collider.size.y < 0.47f)
            {
                Animator.SetBool("crouch", false);
                playerState.Set("crouch", false); 
                Collider.size = StandingSize;
                Collider.offset = StandingOffset;
            }
        }

        void Zip()
        {
            // if (Input.GetAxisRaw("LTrigger") > 0 && !holdR)
            if (InputManager.LastInputHold("DASHLEFT") && !holdR)
            {
                holdL = true;
                // if (Input.GetAxisRaw("RTrigger") > 0)
                if (InputManager.LastInputHold("DASHRIGHT"))
                {
                    // float backdashDirection = (facingRight) ? -1 : 1;
                    // velocity.x = dashVelocity * backdashDirection;
                    Animator.SetBool("zip", true);
                    playerState.Set("zip", true);
                    velocity.x = -dashVelocity;
                }
            }
            else {
                holdL = false;
                // Animator.SetBool("zip", false);
            }
            
            // if (Input.GetAxisRaw("RTrigger") > 0 && !holdL)
            if (InputManager.LastInputHold("DASHRIGHT") && !holdL)
            {
                holdR = true;
                // if (Input.GetAxisRaw("LTrigger") > 0)
                if (InputManager.LastInputHold("DASHLEFT"))
                {
                    // float backdashDirection = (facingRight) ? 1 : -1;
                    // velocity.x = dashVelocity * backdashDirection;
                    Animator.SetBool("zip", true);
                    playerState.Set("zip", true);
                    velocity.x = dashVelocity;
                }
            }
            else {
                holdR = false;
            }
            // if (Input.GetAxisRaw("RTrigger") == 0 || Input.GetAxisRaw("RTrigger") == 0)
            if (InputManager.LastInputUp("DASHLEFT") || InputManager.LastInputUp("DASHRIGHT"))
            {
                Animator.SetBool("zip", false);
                playerState.Set("zip", false);
                
            }
        }

        void LeftDash()
        {
            // if(InputManager.LastInputDown("ltrigger") || InputManager.LastInputDown("backdash"))
            if(InputManager.LastInputDown("DASHLEFT"))
            {
                velocity.x = -dashVelocity;
                Animator.Play("BackdashAlucard",-1,0f);
                playerState.Set("backdash", true);
            }
    
            if(InputManager.LastInputUp("DASHLEFT")) playerState.Set("backdash", false);
            
        }
        void RightDash()
        {
            // if(InputManager.LastInputDown("rtrigger") || InputManager.LastInputDown("FrontDash"))
            if(InputManager.LastInputDown("DASHRIGHT"))
            {
                velocity.x = dashVelocity;
            }
        }
        // void BackDash()
        // {
        //     if(InputManager.LastInputDown("ltrigger") || InputManager.LastInputDown("backdash"))
        //     {
        //         float backdashDirection = (facingRight) ? -1 : 1;
        //         velocity.x = dashVelocity * backdashDirection;
        // 		Animator.Play("BackdashAlucard",-1,0f);
        //         playerState.Set("backdash", true);
        //     }
    
        //     if(Input.GetAxisRaw("LTrigger") == 0) playerState.Set("backdash", false);
    
        void PlayerStateLook()
        {
            // if (facingRight)
            // {
            //     playerState.lookDirection = 0;
            // }
            // else
            // {
            //     playerState.lookDirection = 180;
            // }
            if(InputManager.LastInputHold("UP")) {playerState.lookDirection = 90;} 
            if(InputManager.LastInputHold("RIGHT")) {playerState.lookDirection = 0;} 
            if(InputManager.LastInputHold("DOWN")) {playerState.lookDirection = 270;} 
            if(InputManager.LastInputHold("LEFT")) {playerState.lookDirection = 180;} 
        }
        
        void Flip(Vector2 input)
        {
            if (
                (facingRight && input.x < 0 && !Animator.GetBool("Backdash"))
                ||
                (!facingRight && input.x > 0 && !Animator.GetBool("Backdash"))
                )
                {
                    facingRight = !facingRight;

                    // Multiply the player's x local scale by -1.
                    Vector2 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
        }

        void Jump()
        {
            if(InputManager.LastInputDown("JUMP"))
            {
                // playerState.Set("jump", true);
                playerState.Set("jump", true);
                // velocity.y = shortJumpVelocity;
                //Animator.Play("JumpAlucard",-1,0f);
                
                if (wallSliding)
                {
                    if (wallDirX == input.x)
                    {
                        velocity.x = -wallDirX * wallJumpClimb.x;
                        velocity.y = wallJumpClimb.y;
                        jumps += 1;
                    }
                    if (input.x == 0)
                    {
                        velocity.x = -wallDirX * wallJumpOff.x;
                        velocity.y = wallJumpOff.y;
                        jumps += 1;
                    }
                    if (wallDirX == -input.x)
                    {
                        velocity.x = -wallDirX * wallJumpClimb.x;
                        velocity.y = wallJumpClimb.y;
                        jumps += 1;
                    }
                }
            }
            if(InputManager.LastInputUp("JUMP") && (iJumpFramesDiff < 4) && jumps > 0 && !wallSliding)
                {
                    // if(jumps < jumpsMax) ? Animator.Play("Double Jump") : Animator.Play("Jump");
                    velocity.y = shortJumpVelocity;
                    iJumpFrames = 0;
                    playerState.Set("jump", false);
                    jumps -= 1;
                    if(!playerState.zip)
                    {
                        Animator.Play("JumpAlucard",-1,0f);
                    }
                }

            if (InputManager.LastInputHold("JUMP") && playerState.jump && jumps > 0 && !wallSliding)
            {
                if(iJumpFrames == 0) iJumpFrames = Time.frameCount;
                iJumpFramesDiff = Time.frameCount - iJumpFrames;

                if(iJumpFramesDiff >= 4)
                {
                    // if(jumps < jumpsMax) ? Animator.Play("Double Jump") : Animator.Play("Jump");
                    // Debug.Log("longJump");
                    // velocity.y += 6;
                    iJumpFrames = 0;
                    velocity.y = jumpVelocity;
                    // Debug.Log("LongJump Velocity: " + jumpVelocity);
                    playerState.Set("jump", false);
                    jumps -= 1;
                    if(!playerState.zip)
                    {
                        Animator.Play("JumpAlucard",-1,0f);
                    }
                }  
            }

        }
        public void KeyCombo()
        {
            if(InputManager.playerInputDownArray.Count > 0)
            {
                // var i = InputManager.playerInputLastArray;
                if (DashRight.Check(InputManager.playerInputDownLastArray))
                {
                    velocity.x = 100;
                }
                if (DashLeft.Check(InputManager.playerInputDownLastArray))
                {
                    velocity.x = -100;
                }
                if (DashUp.Check(InputManager.playerInputDownLastArray))
                {
                    velocity.y = 20;
                }
                if (DashDown.Check(InputManager.playerInputDownLastArray))
                {
                    velocity.y = -20;
                }
                if (GLaunch.Check(InputManager.playerInputDownLastArray))
                {
                    velocity.y = Mathf.Abs(gravity) * timeToJumpApex * 2; // Calculate jump velocity
                }
            }
        }

        void Move(Vector2 input, float moveSpeed, ref Vector2 velocity)
        {
            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            Animator.SetFloat ("inputX", input.x);
            Animator.SetFloat ("inputY", input.y);
            Animator.SetFloat ("velocityX", velocity.x);
            Animator.SetFloat ("velocityY", velocity.y);
            Flip(input);
        }

        void CollisionCheck()
        {
            if (controller.collisions.left)
            {
                wallDirX = -1;
            } 
            if (controller.collisions.right)
            {
                wallDirX = 1;
            }
            
            if (controller.collisions.above)
            {
                velocity.y = 0;
            } 
            if (controller.collisions.below)
            {
                velocity.y = 0; // Don't accumulate gravity
                jumps = jumpsMax;
                Animator.SetBool("grounded", true);
                playerState.Set("grounded", true);
                playerState.Set("falling", false);   
            }

            if (!controller.collisions.below)
            {
                Animator.SetBool("grounded", false); 
                playerState.Set("grounded", false);    
            }

            WallSliding();
        }


        void WallSliding()
        {
        wallSliding = false;
            
        if (( (controller.collisions.left && !facingRight) || (controller.collisions.right && facingRight) ) && !controller.collisions.below && playerState.falling)
            {
                
                wallSliding = true;

                if (velocity.y < -wallSlideSpeedMax)
                {
                    velocity.y = -wallSlideSpeedMax;
                }
                if (timeToWallUnstick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;

                    if (input.x != wallDirX && input.x != 0)
                    {
                        timeToWallUnstick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnstick = wallStickTime;
                    }
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
        }


            void OnGUI () 
            {
                GUI.Box(new Rect(10,10,100,20), "FPS: " + (1/Time.deltaTime));
                GUI.Box(new Rect(125,10,100,20), InputManager.playerInputHistory.Count.ToString());
                if (InputManager.playerInputHistory.Count > 0)
                {
                    if (InputManager.playerInputHistory.Last().Count > 1)
                    {
                        for (int i=0; i <= InputManager.playerInputHistory.Last().Count - 1; i++)
                        {
                            GUI.Box(new Rect(10,30 + i*25,100,25), InputManager.playerInputHistory.Last()[i].InputName);     
                        }
                    }
                    else
                    {
                        GUI.Box(new Rect(10,30,100,25), InputManager.playerInputHistory.Last()[0].InputName);
                    }
                }
            }
    }
}