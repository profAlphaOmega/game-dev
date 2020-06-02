using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{

	public class Crouch : MonoBehaviour {

		Main self;
		Vector2 CrouchingSize = new Vector2(0.22f, 0.15f);
		Vector2 CrouchingOffset = new Vector2(0, -0.16f);
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check ()
		{
			if (self.InputManager.LastInputHold("DOWN") && self.state.grounded)
				{
					self.Collider.size = CrouchingSize;
					self.Collider.offset = CrouchingOffset;
					// self.Animator.SetBool("crouch", true); 
					self.state.crouching = true;
					self.InputManager.input.x = 0; 
				}
				if (self.InputManager.LastInputUp("DOWN") && self.state.crouching)
				{
					// self.Animator.SetBool("crouch", false);
					self.state.crouching = false; 
					self.Collider.size = self.StandingSize;
					self.Collider.offset = self.StandingOffset;
					
				}
		}
	}
}
