using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class Wavedash : MonoBehaviour 
	{
		Main self;
		public float dashspeed = 20;
		public int dashframes = 10;

		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			// THIS FIRST ONE INCLUDES THE STALL TECH
			if(self.InputManager.LastInputDown("WAVEDASH") && !self.state.grounded)
			// if(self.InputManager.LastInputDown("WAVEDASH") && !self.state.grounded && self.InputManager.LastInputHold("UP","DOWN","LEFT","RIGHT"))
			{
				self.Animate.Animator.Play("BackdashAlucard",-1,0f);
				// self.state.backdashing = true;
				if(!self.state.wavedash) StartCoroutine(IWaveDash());
			}
		
			// if(self.InputManager.LastInputUp("DASHRIGHT")) self.state.backdashing = false;
		}

		IEnumerator IWaveDash()
        {
            self.state.wavedash = true;
            self.state.canMove = false;
			int dirX = self.state.dirX;
			int dirY = self.state.dirY;
			self.velocity = Vector2.zero;
            
			yield return StartCoroutine(UTILS.WaitForFrames(2)); // delay after coming to senses
			
			self.velocity.x = dashspeed * dirX;
			self.velocity.y = dashspeed * dirY;
            int i = dashframes;
			while (i > 0)
            {
                self.MoveY(targetVelocityY: 0,velocity: ref self.velocity,timebeta: 1f);
                self.MoveX(targetVelocityX: 0,velocity: ref self.velocity,timebeta: 1f);
				i--;
                yield return null;
            }
			
			self.velocity = Vector2.zero;
            yield return StartCoroutine(UTILS.WaitForFrames(2)); // delay after coming to senses
			self.state.wavedash = false;
			self.state.canMove = true;
		}

	}
}
