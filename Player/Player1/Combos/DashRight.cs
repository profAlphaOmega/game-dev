using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class DashRight : MonoBehaviour {

		Main self;
		private KeyCombo dashRight = new KeyCombo(new string[] {"RIGHT","RIGHT"}, new int[] {5}, new int[] {10});
		
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			if (dashRight.Check(self.InputManager.playerInputDownLastArray))
			{
				self.velocity.x += 200;
			}
		}
	}
}
