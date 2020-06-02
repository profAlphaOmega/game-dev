using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class DashUp : MonoBehaviour {

		Main self;
		private KeyCombo dashUp = new KeyCombo(new string[] {"UP","UP"}, new int[] {5}, new int[] {10});
		
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			if (dashUp.Check(self.InputManager.playerInputDownLastArray))
			{
				self.velocity.y = 50;
			}
		}
	}
}