using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class Shinespark : MonoBehaviour 
	{
		public Main self;
		public float sparkspeed = 35;
		public int sparkframes = 30;
		public int directionX = 0;
		public int directionY = 0;
		// Use this for initialization
		public bool canShineSpark = false;
		public bool shineCountdown = false;
		void Start () 
		{
			self = GetComponent<Main>();	
		}
		
		void Update ()
		{
			if(!canShineSpark)
			{
				if((Mathf.Abs(self.velocity.x) > 0.1f))
				{
					if(self.InputManager.LastInputDown("DOWN") && self.state.grounded)	
					{
						canShineSpark = true;
					}
				}
			}
			if(canShineSpark)
			{
				// start timer
				if(!shineCountdown)StartCoroutine(ShineSparkTimer());
				// ShineSpark
				if(self.InputManager.LastInputDown("SHINESPARK") && self.InputManager.LastInputHold("UP","DOWN","LEFT","RIGHT"))
				{
					directionX = self.state.dirX;
					directionY = self.state.dirY;
					StartCoroutine(Spark());
				}
			}
		}

		IEnumerator ShineSparkTimer()
        {
			shineCountdown = true;
			yield return StartCoroutine(UTILS.WaitForFrames(60)); // delay after coming to senses
			if(!self.state.shinesparking) canShineSpark = false;	
			shineCountdown = false;
		}
		IEnumerator Spark()
        {
			self.state.shinesparking = true;
            self.state.canMove = false;

			self.velocity = Vector2.zero;

			yield return StartCoroutine(UTILS.WaitForFrames(20));

			self.velocity.x = sparkspeed * directionX;
			self.velocity.y = sparkspeed * directionY;

			int i = sparkframes;
			while (i > 0)
            {
                self.MoveY(targetVelocityY: 0,velocity: ref self.velocity,timebeta: 1f);
                self.MoveX(targetVelocityX: 0,velocity: ref self.velocity,timebeta: 1f);
				i--;
                yield return null;
            }
			canShineSpark = false;
			self.state.shinesparking = false;
            self.state.canMove = true;
		}
	}
}
