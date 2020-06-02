using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class AIController : MonoBehaviour
	{
		Main self;
		public void Start()
		{
			self = GetComponent<Main>();
		}

		public void GoTo(Transform t)
		{
			if(Mathf.Abs(this.transform.position.x - t.position.x) < 1)
			{
				return; 
			} 
			if(this.transform.position.x < t.position.x)
			{
				float friction = (self.controller.collisions.below) ? self.accelerationTimeGrounded : self.accelerationTimeAirborne;
				self.Move(self.moveSpeed,ref self.velocity);
				// self.Move(-self.moveSpeed,ref self.velocity);
				self.Animate.Check();
				self.Orientation.Flip();
			}
			if(this.transform.position.x > t.position.x )
			{
				float friction = (self.controller.collisions.below) ? self.accelerationTimeGrounded : self.accelerationTimeAirborne;
				self.Move(self.moveSpeed,ref self.velocity);
				// self.Move(-self.moveSpeed,ref self.velocity);
				self.Animate.Check();
				self.Orientation.Flip();
			}
		}

		public void Freeze()
		{
			self.state.canMove = false;
			self.velocity = Vector2.zero;
			self.state.inputDisabled = true;
		}
		public void Unfreeze()
		{
			self.state.canMove = true;
			self.state.inputDisabled = false;
		}
	}
}