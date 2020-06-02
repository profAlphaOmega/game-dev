using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
    public class Jump : MonoBehaviour
    {
        Main self;
        public float jumpHeight = 2.4f;
        public float timeToJumpApex = .50f;
        public float shortJumpHeight = 1.0f;
        public float shortTimeToJumpApex = .35f;
        public int jumpsMax = 100;
        public int jumps;
        public float jumpVelocity;
        public float shortJumpVelocity;
        public int iJumpFrames = 0;
        public int iJumpFramesDiff = 0;

        public Vector2 wallJumpClimb = new Vector2(7.5f,16f);
        public Vector2 wallJumpOff = new Vector2(8.5f,7f);
        public Vector2 wallJumpLeap = new Vector2(18f,17f);
        

        void Start () {
            self = GetComponent<Player1.Main>();
            jumps = jumpsMax;
        }
        
        public void Check () 
        {
            if(self.controller.collisions.below)
            {
                jumps = jumpsMax;
            }

            if(self.InputManager.LastInputDown("JUMP"))
            {
                self.state.jumping = true;
                self.state.jumped = false;
                // self.velocity.y = self.shortJumpVelocity;
                //Animator.Play("JumpAlucard",-1,0f);
                
                if (self.state.wallSliding)
                {
                    if (self.Physics.wallDirX == self.InputManager.input.x)
                    {
                        WallJumpAction(wallJumpClimb);
                    }
                    if (self.InputManager.input.x == 0)
                    {
                        WallJumpAction(wallJumpOff);
                    }
                    if (self.Physics.wallDirX == -self.InputManager.input.x)
                    {
                        WallJumpAction(wallJumpLeap);
                    }
                }
            }
            if(self.InputManager.LastInputUp("JUMP") && (iJumpFramesDiff < 4) && jumps > 0 && !self.state.wallSliding)
                {
                    // if(self.jumps < jumpsMax) ? Animator.Play("Double Jump") : Animator.Play("Jump");
                    JumpCommand(shortJumpVelocity);
                }
            if (self.InputManager.LastInputHold("JUMP") && !self.state.jumped && jumps > 0 && !self.state.wallSliding)
            {
                if(iJumpFrames == 0) iJumpFrames = Time.frameCount;
                iJumpFramesDiff = Time.frameCount - iJumpFrames;

                if(iJumpFramesDiff >= 4)
                {
                    // if(self.jumps < jumpsMax) ? Animator.Play("Double Jump") : Animator.Play("Jump");
                    // self.velocity.y += 6;
                    JumpCommand(jumpVelocity);
                }  
            }
        }

        // Check and AI
        public void JumpCommand(float jumpVelocity)
        {
            iJumpFrames = 0;
            jumps -= 1;
            self.state.jumped = true;

            self.velocity.y = jumpVelocity;
            if(!self.state.zipping)
            {
                // self.Animate.Animator.Play("JumpAlucard",-1,0f);
            }
            // if(self.state.canMove) 
            // {
            //     self.Move(self.InputManager.input.x * self.moveSpeed, ref self.velocity);
            //     self.AnimateMove();
            //     // self.Flip();
            // }
        }

        // Check and AI
        public void WallJumpAction(Vector2 wallJumpVector)
        {
            self.velocity.x = -self.Physics.wallDirX * wallJumpVector.x;
            self.velocity.y = wallJumpVector.y;
            jumps += 1;
        }
    }
}
