using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class Orientation : MonoBehaviour {

		public Main self;
		void Start ()
		{
			self = GetComponent<Main>();	
		}
		
		public void Check ()
		{
			Orientate();
		}

		void Orientate()
		{
			self.state.dirX = 0;
			self.state.dirY = 0;
			// self.state.lookDirection = 0;
			if(self.InputManager.LastInputHold("RIGHT")) {self.state.lookDirection = 270; self.state.dirX = 1;} 
			if(self.InputManager.LastInputHold("UP")) {self.state.lookDirection = 0; self.state.dirY = 1;} 
			if(self.InputManager.LastInputHold("LEFT")) {self.state.lookDirection = 90; self.state.dirX = -1;} 
			if(self.InputManager.LastInputHold("DOWN")) {self.state.lookDirection = 180; self.state.dirY = -1;} 
		}

		public void Flip()
        {
            if (
                // (state.facingRight && InputManager.input.x < 0 && !state.backdashing)
                (self.state.facingRight && self.velocity.x < 0 && !self.state.backdashing)
                ||
                (!self.state.facingRight && self.velocity.x > 0 && !self.state.backdashing)
                // (!state.facingRight && InputManager.input.x > 0 && !state.backdashing)
                )
                {
                    self.state.facingRight = !self.state.facingRight;

                    // Multiply the player's x local scale by -1.
                    Vector2 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
        }
	}
}
