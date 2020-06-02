using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player1
{
	public class SunFlare : MonoBehaviour {

		Main self;
		public ParticleSystem SunFlareParticleSystem;
		List<ParticleCollisionEvent> collisionEvents;
		public float dmg = 1;

		void Start() 
		{
			SunFlareParticleSystem = GetComponent<ParticleSystem>();
			collisionEvents = new List<ParticleCollisionEvent>();
			self = this.transform.parent.GetComponent<Main>();
			// this.Disable();
		}

		public void Check()
		{
			if (self.InputManager.LastInputDown("SUNFLARE"))
            {
                self.Animate.Animator.Play("SunFlare");
            }
            if (self.InputManager.LastInputHold("SUNFLARE"))
            {
                Emit(self.state.lookDirection);
            }
		}
		void OnParticleCollision(GameObject other)
		{
			int numCollisionEvents = SunFlareParticleSystem.GetCollisionEvents(other, collisionEvents);
			int i = 0;

			while(i < numCollisionEvents)
			{
				switch(other.tag)
                {
                    case "Enemy":
						// other.SendMessageUpwards("ReduceHealth", dmg);
						other.GetComponent<Enemy.Health>().ReduceHealth(dmg);
                        break;
                }
				i++;
			}	
		}

		public void Emit(int degs)
		{
			SunFlareParticleSystem.transform.rotation = Quaternion.Euler(0, 0, degs);
			SunFlareParticleSystem.Emit(1);
		}
		public void Enable()
		{
			this.gameObject.SetActive(true);
		}
		public void Disable()
		{
			this.gameObject.SetActive(false);
		}
		public bool isActive()
		{
			return this.gameObject.activeSelf;
		}
		public bool isPlaying()
		{
			return SunFlareParticleSystem.isPlaying;
		}
	}
}
