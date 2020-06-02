using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageDebugRay : MonoBehaviour {

	public LayerMask _attackLagLayerMask;
	private float _attackLagMaxSight = 2;
	public void DamageTakenRay ()
	{
		Vector3 detectOrigin = this.transform.position;
		RaycastHit2D hit = Physics2D.Raycast(detectOrigin, Vector2.down, _attackLagMaxSight, _attackLagLayerMask);
		Debug.DrawRay(detectOrigin, Vector2.up*_attackLagMaxSight, Color.red);
	}
}
