using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class Physics : MonoBehaviour 
	{
		public Main self;
		public Animator Animator;
        public FloatVariable NormalGravity;
        public Friction NormalFriction;
        public FloatVariable WaterGravity;
        public Friction WaterFriction;
        public FloatVariable CustomGravity;
        public Friction CustomFriction;

        public float friction = 0;
        public float gravity = 0;
		public float wallSlideSpeedMax = 1; 
        public int wallDirX;
        public float wallStickTime = 50.0f;
        public float timeToWallUnstick = 0f;
        private float velocityXSmoothing;
        // private float velocityYSmoothing;

		void Start ()
		{
			self = GetComponent<Main>();
			Animator = GetComponent<Animator>();
		}

        public void Calculate()
        {
            /// <summary>
            /// Handles State Value Setting
            /// Checks what state you are in, priority matters. Normal is implied default
            /// You shouldn't really ever be in two different states at the same time, so priority is easy solution
            /// <para><param name="self.state"> State variable of gameObject</param></para>  
            /// </summary>

            // All this logic might not have to be in Update, this logic can be callable...maybe not
            if(self.state.customPhysics)
            {
                // Debug.Log("custom");
                gravity = self.gravity * CustomGravity.Value ;
                friction = (self.controller.collisions.below) ? CustomFriction.GroundFriction : CustomFriction.AirFriction;
                return;
            }
            if(self.state.waterPhysics)
            {
                // Debug.Log("water");
                gravity = self.gravity * WaterGravity.Value;
                friction = (self.controller.collisions.below) ? WaterFriction.GroundFriction : WaterFriction.AirFriction;
                return;
            }

            // Debug.Log("normal");
            gravity = self.gravity * NormalGravity.Value;
            friction = (self.controller.collisions.below) ? NormalFriction.GroundFriction : NormalFriction.AirFriction;
        }
		
        public Vector2 Gravity(Vector2 velocity)
        {
            velocity.y += gravity * Time.deltaTime;
            return velocity;
        }
        public Vector2 Friction(Vector2 velocity, float targetVelocityX)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, friction);
            return velocity;
        }
     

		public void CollisionCheck()
        {
            if (self.controller.collisions.left)
            {
                // wallDirX = -1;
            } 
            if (self.controller.collisions.right)
            {
                // wallDirX = 1;
            }
            if (self.controller.collisions.above)
            {
                self.velocity.y = 0;
                // Debug.Log("here");
            } 
            if (self.controller.collisions.below)
            {
                self.velocity.y = 0; // Don't accumulate gravity
                // if(!self.state.jumping) self.velocity.y = 0; // Don't accumulate gravity
                // Jump.jumps = Jump.jumpsMax;
                // Animator.SetBool("grounded", true);
                self.state.grounded = true;
                self.state.jumping = false;
                self.state.falling = false;   
            }

            if (!self.controller.collisions.below)
            {
                // Animator.SetBool("grounded", false); 
                self.state.grounded = false;
                self.state.falling = (self.velocity.y < 0) ? true : false;
                // Detect when descending from jump, because Jump doesn't set itself yet
                if (self.state.jumping && self.state.falling) self.state.jumping = false;    
            }

        }


		public void WallSliding()
		{
			// WallSliding
			if (self.controller.collisions.left)
            {
                wallDirX = -1;
            } 
            if (self.controller.collisions.right)
            {
                wallDirX = 1;
            }

            self.state.wallSliding = false;
            
            if (((self.controller.collisions.left && !self.state.facingRight) || (self.controller.collisions.right && self.state.facingRight) ) && !self.controller.collisions.below && self.state.falling)
                {
                    self.state.wallSliding = true;
                    if (self.velocity.y < -wallSlideSpeedMax)
                    {
                        self.velocity.y = -wallSlideSpeedMax;
                    }
                    if (timeToWallUnstick > 0)
                    {
                        self.velocity.x = 0;

                        if (self.InputManager.input.x != wallDirX && self.InputManager.input.x != 0)
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

            if(self.state.wallGrabbing)
            {
                self.velocity = new Vector2(0,0);
            }
		}


	}
}
