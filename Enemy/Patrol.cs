using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{

	public class Patrol : MonoBehaviour {

		Enemy.Main self;
		
		void Start () {
			self = GetComponent<Enemy.Main>();
		}
		
		void Update () {
			if(self.state.canPatrolCheck()) 
            {
                StartCoroutine(IPatrol());
            }
		}
		public IEnumerator IPatrol()
			{
				self.state.setPatrol(true);
				while (self.state.isPatrolling) {
					int _direction = (int)Mathf.Round(Random.Range(0f,1f));
					switch(_direction)
					{
						case 0:
							if(self.facingRight) self.Flip();
							yield return StartCoroutine(PatrolLeft());
							self.state.setPatrol(false);
							break;
						case 1:
							if(!self.facingRight) self.Flip();
							yield return StartCoroutine(PatrolRight());
							self.state.setPatrol(false);
							break;
					}
				}   
			}

			public IEnumerator PatrolLeft()
			{
				int startMoveFrame = 0;
				int endMoveFrame = (int)Mathf.Round(Random.Range(10f,100f));
				while ((startMoveFrame < endMoveFrame) && self.state.isPatrolling)
				{
					startMoveFrame++;
					self.Move(ref self.velocity, -self.moveSpeed, self.smoothTime);
					yield return null;
				}
				// int waitForFrames = (int)Mathf.Round(Random.Range(10f,100f));
				// yield return StartCoroutine(UTILS.WaitForFrames(waitForFrames));
				// yield return new WaitForSeconds(4f);
			}

			public IEnumerator PatrolRight()
			{
				int startMoveFrame = 0;
				int endMoveFrame = (int)Mathf.Round(Random.Range(10f,100f));
				while ((startMoveFrame < endMoveFrame) && self.state.isPatrolling)
				{
					startMoveFrame++;
					self.Move(ref self.velocity, self.moveSpeed, self.smoothTime);
					yield return null;
				}
				// int waitForFrames = (int)Mathf.Round(Random.Range(10f,100f));
				// yield return StartCoroutine(UTILS.WaitForFrames(waitForFrames));
				// yield return new WaitForSeconds(4f);
			}
	}
}
