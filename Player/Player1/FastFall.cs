using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class FastFall : MonoBehaviour 
	{

		Main self;
		float _velFastFall = 6;

		void Start ()
		{
			self = GetComponent<Main>();	
		}
		
		public void Check()
		{
			if((self.state.falling) && self.InputManager.LastInputDown("DOWN"))
			{
				self.velocity.y -= _velFastFall;
			}
		}
	}
}
