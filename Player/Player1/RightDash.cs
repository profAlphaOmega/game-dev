using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class RightDash : MonoBehaviour 
	{
		Main self;

		public float rightDashVelocity = 50;
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			if(self.InputManager.LastInputHold("DASHRIGHT"))
				{
					self.velocity.x += rightDashVelocity;
					self.Animate.Animator.Play("BackdashAlucard",-1,0f);
					// self.state.backdashing = true;
				}
		
			// if(self.InputManager.LastInputUp("DASHRIGHT")) self.state.backdashing = false;
		}
	}
}
