using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Playables;

namespace Player1
{
    [RequireComponent(typeof(Controller2D))]
    public class Main : MonoBehaviour
    {
        public Vector2 velocity;
        public Controller2D controller;
        public Physics Physics;
        public Orientation Orientation;
        public Jump Jump;
        public Crouch Crouch;
		public FastFall FastFall;
        public AIController AIController;
        public PlayerState state;
        public Animate Animate;
        public BoxCollider2D Collider;
        public InputManager InputManager;
        // public GameObject Timeline;
        // public PlayableDirector timelineDirector;

        private DashRight DashRight;
        private DashLeft DashLeft;
        private Wavedash Wavedash;
        private DashUp DashUp;
        private DashDown DashDown;
        private GLaunch GLaunch;
        private GrabAttack Grab;
        // public SwordAttack SwordAttack;
        public Attack HighKick;
        public List<Collider2D> AttackResults = new List<Collider2D>();
        public Attack Punch;
        private RightDash RightDash;
        private LeftDash LeftDash;
        private SunFlare SunFlare;
        private Save Save;
        private AttackLag AttackLag;
        public Vector2 StandingSize = new Vector2(0.23f, 0.47f);
        public Vector2 StandingOffset = new Vector2(0, 0);
        public float moveSpeed = 6;
        private float velocityXSmoothing;
        private float velocityYSmoothing;
        public float accelerationTimeAirborne = .05f;
        public float accelerationTimeGrounded = .05f;
        public float gravity = 0;
        public float friction = 0;
        public float shortGravity = 0;
        public Transform Garg;


        
        void Start()
        {
            state = new PlayerState();

            // Make some of these required components
            controller = GetComponent<Controller2D>();
            AIController = GetComponent<AIController>();
            Animate = GetComponent<Animate>();
            Collider = GetComponent<BoxCollider2D>();
            InputManager = GetComponent<InputManager>();
            Orientation = GetComponent<Orientation>();
			Crouch = GetComponent<Crouch>();
			FastFall = GetComponent<FastFall>();
			Save = GetComponent<Save>();
            // Jump = GetComponent<Jump>();
            Jump = GetComponent<Jump>();
            Physics = GetComponent<Physics>();
            DashRight = GetComponent<DashRight>();
            Wavedash = GetComponent<Wavedash>();
            DashLeft = GetComponent<DashLeft>();
            DashDown = GetComponent<DashDown>();
            DashUp = GetComponent<DashUp>();
            GLaunch = GetComponent<GLaunch>();
            RightDash = GetComponent<RightDash>();
            LeftDash = GetComponent<LeftDash>();
            AttackLag = GetComponent<AttackLag>();
            SunFlare = GetComponentInChildren<SunFlare>();
            Grab = GetComponentInChildren<GrabAttack>();
            // SwordAttack = GetComponentInChildren<SwordAttack>();
            HighKick = GameObject.Find("HighKick").GetComponentInChildren<Attack>();
            Punch = GameObject.Find("Punch").GetComponentInChildren<Attack>();
            // Timeline = GameObject.Find("Timeline");
            // timelineDirector = Timeline.GetComponent<PlayableDirector>();
            
            // Gravity Calculation needs to go into its own script with a default world gravity, and also inputs for variable ones
            gravity = -(2 * Jump.jumpHeight) / Mathf.Pow(Jump.timeToJumpApex, 2); // Calculate gravity
            Jump.jumpVelocity = Mathf.Abs(gravity) * Jump.timeToJumpApex; // Calculate Jump self.velocity
            
			shortGravity = -(2 * Jump.shortJumpHeight) / Mathf.Pow(Jump.shortTimeToJumpApex, 2);
            Jump.shortJumpVelocity = Mathf.Abs(shortGravity) * Jump.shortTimeToJumpApex;
        }

        void Update()
        {
            // Input Collection
            InputManager.Capture();
            
            if(InputManager.LastInputDown("START"))
            {
                Save.Run();
            }

            Physics.CollisionCheck();
            Physics.WallSliding();
            
            KeyCombo();
            ProcessAttacks();
            ProcessMovement();
        }



        public void ProcessMovement()
        {
            Wavedash.Check();
            RightDash.Check();
            Orientation.Check();
            Jump.Check();
			Crouch.Check();
			FastFall.Check();
            
            // Always keep at bottom
            if(state.canMove) 
            {
                // friction = (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne;
                Move(targetVelocityX: InputManager.input.x * moveSpeed, velocity: ref velocity);
                // Animate.Check();
                Orientation.Flip();
            }
        }

        public void ProcessAttacks()
        {
            // Attacks
            AttackResults = new List<Collider2D>(); // clear the results from before 
            Grab.Check();
            SunFlare.Check();

            // SwordAttack.Check();
            if (InputManager.LastInputDown("HIGHKICK") && !state.isAttackLagging)
            {
                // AttackResults = new List<Collider2D>(); // reset current results
                AttackResults = HighKick.Check("Enemy");
            }
            
            if(InputManager.LastInputHold("PUNCH") && !state.isAttackLagging)
            {
                AttackResults = Punch.Check("Enemy");
            }

            // AttackLag
            AttackLag.Check(); // Some logic needs to be moved out of this call into here


            // Process Attacks
            if(AttackResults.Count > 0)
            {
                foreach(Collider2D r in AttackResults)
                {
                    Debug.Log(r.tag);
                }
                AttackResults = new List<Collider2D>();
            }
        }


        public void KeyCombo()
        {
            DashRight.Check();
            DashLeft.Check();
            DashDown.Check();
            DashUp.Check();
            GLaunch.Check();
        }

        public void Move(float targetVelocityX, ref Vector2 velocity)
        {
            Physics.Calculate();
            velocity = Physics.Gravity(velocity: velocity);
            velocity = Physics.Friction(velocity: velocity, targetVelocityX: targetVelocityX);
            controller.Move(velocity * Time.deltaTime);
        }


        public void MoveX(float targetVelocityX, ref Vector2 velocity, float timebeta)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, timebeta);
            controller.Move(velocity * Time.deltaTime);
        }
        public void MoveY(float targetVelocityY, ref Vector2 velocity, float timebeta)
        {
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, timebeta);
            controller.Move(velocity * Time.deltaTime);
        }
   

        void OnGUI () 
        {
            GUI.Box(new Rect(10,10,100,20), "FPS: " + (1/Time.deltaTime));
            GUI.Box(new Rect(125,10,100,20), InputManager.playerInputHistory.Count.ToString());
            GUI.Box(new Rect(125,30,100,20), "XV: " + velocity.x);
            GUI.Box(new Rect(125,50,100,20), "YV: " + velocity.y);
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
        // void Zip()
        // {
        //     if (InputManager.LastInputHold("DASHLEFT") && !holdR)
        //     {
        //         holdL = true;
        //         if (InputManager.LastInputHold("DASHRIGHT"))
        //         {
        //             // float backdashDirection = (state.facingRight) ? -1 : 1;
        //             // velocity.x = dashVelocity * backdashDirection;
        //             Animator.SetBool("zip", true);
        //             state.zip = true;
        //             velocity.x -= dashVelocity;
        //             Debug.Log(dashVelocity);
        //         }
        //     }
        //     else {
        //         holdL = false;
        //     }
            
        //     if (InputManager.LastInputHold("DASHRIGHT") && !holdL)
        //     {
        //         holdR = true;
        //         if (InputManager.LastInputHold("DASHLEFT"))
        //         {
        //             // float backdashDirection = (state.facingRight) ? 1 : -1;
        //             // velocity.x = dashVelocity * backdashDirection;
        //             Animator.SetBool("zip", true);
        //             state.zip = true;
        //             velocity.x += dashVelocity;
        //         }
        //     }
        //     else {
        //         holdR = false;
        //     }
        //     if (InputManager.LastInputUp("DASHLEFT") || InputManager.LastInputUp("DASHRIGHT"))
        //     {
        //         Animator.SetBool("zip", false);
        //         state.zip = false;
                
        //     }
        // }

        // if(InputManager.LastInputDown("GARG"))
        // {
        //     // StartCoroutine(CreateGarg());
        //     // Transform garg = Instantiate(Resources.Load("Prefabs/Enemies/Garg/garg")) as Transform;
        //     // // Transform garg = (Transform)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/Enemies/Garg/garg.prefab", typeof(Transform));
        //     Transform garg = Instantiate(Garg) as Transform;
        //     garg.position = new Vector2(-14,-25);
        // }

        // PlayerActionEvent.Raise();
            // if(InputManager.LastInputDown("JUMP"))
            // {
            //     timelineDirector.Play();
            // }