using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class Animate : MonoBehaviour
	{
		public Main self; 
		public Animator Animator;

		void Start () 
		{		
			self = GetComponent<Main>();
			Animator = GetComponent<Animator>();
		}
		
		public void Check ()
		{
			Animator.SetFloat ("absVelX", Mathf.Abs(self.velocity.x));
			Animator.SetFloat ("inputX", Mathf.Abs(self.InputManager.input.x));
			Animator.SetFloat ("velocityY", self.velocity.y);
			if(self.state.grounded)
			{
				Animator.SetBool("grounded", true);
			}
			// subtley difference than else statement
			if(!self.state.grounded)
			{
				Animator.SetBool("grounded", false);
			}
			if(self.state.crouching)
			{
				Animator.SetBool("crouch", true);
			}
			if(!self.state.crouching)
			{
				Animator.SetBool("crouch", false);
			}
		}
	}
}
