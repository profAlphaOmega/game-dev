using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class DashDown : MonoBehaviour {

		Main self;
		private KeyCombo dashDown = new KeyCombo(new string[] {"DOWN","DOWN"}, new int[] {5}, new int[] {10});
		
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			if (dashDown.Check(self.InputManager.playerInputDownLastArray))
			{
				self.velocity.y = -50;
			}
		}
	}
}