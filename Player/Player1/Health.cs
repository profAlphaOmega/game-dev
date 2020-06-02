using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{
	public class Health : MonoBehaviour {

		public FloatReference HP;
		public GameEvent Player1TakeDamageEvent;
		void Start () 
		{
			// HP.Value = SETTINGS.Player1Health;
		}
		
		public void ReduceHealth(float amt)
		{
			Player1TakeDamageEvent.Raise();	
			HP.Value -= amt;
			if(HP.Value <= 0) Destroy(gameObject);
			// need death gameevent
		}
	}
}
