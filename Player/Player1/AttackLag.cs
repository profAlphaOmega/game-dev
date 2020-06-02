using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1
{

	public class AttackLag : MonoBehaviour
	{
		public int _cancelledLagWaitFrames = 3;
		public int _lagWaitFrames = 30;
		public int _attackLagWaitFrames = 30;
		public float _attackLagMaxSight = .65f;
		public bool _canLagCancel = false;
		public LayerMask _attackLagLayerMask;
		Main self;
		void Start ()
		{
			self = GetComponent<Main>();
		}
		
		public void Check()
		{
            // Did you attack in the air, and not currently lagging
			if(
				self.InputManager.LastInputDown("ATTACK") 
				&& !self.state.isAttackLagging
			 	&& !self.state.grounded
			)
				{
				 	self.state.attackLag = true;
				}

            // If you are or are going to lag (unless you l-cancel)
			if(self.state.attackLag)
            {
                // Detect if you can cancel and are falling. Attack Lag Max Sight parameters determine how much time you have to l-cancel
                if (self.state.falling && !_canLagCancel)
                {
                    Vector3 detectOrigin = this.transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(detectOrigin, Vector2.down, _attackLagMaxSight, _attackLagLayerMask);
                    Debug.DrawRay(detectOrigin, Vector2.down*_attackLagMaxSight, Color.green);
                    if (hit)
                    {
                        _canLagCancel = true;
                    }
                }
                // Start Lagging, can't cancel anymore if you currently haven't
                if (self.state.grounded)
                {
                    _canLagCancel = false;
                    if (!self.state.isAttackLagging)
                    {
                        StartCoroutine(Lag());
                    }
                }
            }
			
            // Did you l-cancel in time?
			if (_canLagCancel && self.InputManager.LastInputDown("RB", "LB"))
            {
                self.state.attackLagCancelled = true;
            }


		}

		public IEnumerator Lag()
        {
            self.state.isAttackLagging = true;
            self.state.canMove = false;
            // Either incure the lag wait frames if not cancelled, or standard lag frames
            // This should really be a function itself to determine standard lag frames vs cancelled lag frames
            _attackLagWaitFrames = (self.state.attackLagCancelled) ? _cancelledLagWaitFrames : _lagWaitFrames;
             
            yield return StartCoroutine(UTILS.WaitForFrames(_attackLagWaitFrames));

            _canLagCancel = false;
			self.state.isAttackLagging = false;
			self.state.attackLagCancelled = false;
            self.state.attackLag = false;
            self.state.canMove = true;
        }

		

	}
}
