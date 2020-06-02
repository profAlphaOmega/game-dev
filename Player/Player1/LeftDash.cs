using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class LeftDash : MonoBehaviour 
	{
		Main self;

		public float leftDashVelocity = 50;
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			if(self.InputManager.LastInputHold("DASHLEFT"))
				{
					self.velocity.x -= leftDashVelocity;
					self.Animate.Animator.Play("BackdashAlucard",-1,0f);
					// self.state.backdashing = true;
				}
		
			// if(self.InputManager.LastInputUp("DASHLEFT")) self.state.backdashing = false;
		}
	}
}
