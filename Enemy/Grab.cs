using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class Grab : MonoBehaviour {

		Main self;
		void Start () {
			self = GetComponent<Main>();
		}
		
		public void Grabbed()
        {
            if(!self.state.grabbed) self.state.setGrabbed(true); // don't have to keep resetting (onInputHold)
            self.velocity = Vector2.zero;
            self.smoothTime = (self.controller.collisions.below) ? self.accelerationTimeGrounded : self.accelerationTimeAirborne;
            self.Move(velocity: ref self.velocity, finalVelocity: 0, smoothTime: self.smoothTime);
        }

        public void Throw(Vector2 velocity)
        {
            StartCoroutine(IThrow(velocity));
        }

        IEnumerator IThrow(Vector2 velocity)
        {
            self.state.setThrown(true);
            self.state.setGrabbed(false);
            
            while ((((Mathf.Abs(velocity.x) >= 1f) && self.state.grounded) || !self.state.grounded) && !self.state.grabbed)
            {
                self.state.setThrown(true);
                self.Move(ref velocity, 0,(self.state.grounded) ? 1 : 4);
                yield return null;
            }
            yield return StartCoroutine(UTILS.WaitForFrames(40)); // delay after coming to senses
            self.state.setThrown(false);
        }
	}
}
