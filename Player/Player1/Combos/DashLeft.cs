using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class DashLeft : MonoBehaviour {

		Main self;
		private KeyCombo dashLeft = new KeyCombo(new string[] {"LEFT","LEFT"}, new int[] {5}, new int[] {10});
		
		void Start () 
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
			if (dashLeft.Check(self.InputManager.playerInputDownLastArray))
			{
				self.velocity.x += -200;
			}
		}
	}
}