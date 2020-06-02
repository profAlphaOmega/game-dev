using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy 
{
	public class Health : MonoBehaviour {

		public FloatReference HP;
		public float hp;
		public GameEvent TakeDamageEvent;


		void Start () 
		{
			hp = HP.Value;
			Debug.Log("Player HP: " + hp);
		}
		
		public void ReduceHealth(float amt)
		{
			TakeDamageEvent.Raise();	
			Debug.Log(hp);
			hp -= amt;
			if(hp <= 0) Destroy(gameObject);
		}
	}
}
