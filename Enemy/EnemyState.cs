using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {
	public int lookDirection {get; set;}
	public bool grounded {get; set;}
	public bool canMove {get; set;}
	public bool trans {get; set;}
	public bool busy {get; set;}
	public bool canPatrol {get; set;}
	public bool isPatrolling {get; set;}
	public bool isDetecting {get; set;}
	public bool hasDetected {get; set;}
	public bool isTracking {get; set;}
	public bool canDetect {get; set;}
	public bool grabbed {get; set;}
	public bool thrown {get; set;}
	
	public bool falling {get; set;}
	public bool crouching {get; set;}

	public EnemyState()
	{
		// might not need
		canMove = true;
		trans = true;
		
		// action
		grabbed = false;
		canPatrol = true;
		isPatrolling = false;
		thrown = false;
		isDetecting = false;
		hasDetected = false;
		isTracking = false;
		canDetect = false;
		
		// physics
		grounded = false;
		falling = false;
		crouching = false;
		lookDirection = 0;
	}

	public void Reset()
	{
		canMove = true;
		trans = true;
		
		grabbed = false;
		canPatrol = true;
		isPatrolling = false;
		thrown = false;
		isDetecting = false;
		hasDetected = false;
		isTracking = false;
		canDetect = false;
		
		grounded = false;
		falling = false;
		crouching = false;
		lookDirection = 0;
	}

	public void setPatrol(bool n)
	{
		if(n)
		{
			isPatrolling = true;
			busy = true;
		}
		if(!n) 
		{
			isPatrolling = false;
			canPatrol = true;
			busy = false;
		}
	}
	public bool canPatrolCheck()
	{
		if(
			grounded
			&& !falling
			&& !thrown
			&& !grabbed
			&& !isPatrolling // odd one out
			&& !isTracking
		) return true;

		return false;
	}
	public void setDetect(bool n)
	{
		if(n)
		{
			isDetecting = true;
			busy = true;
		}
		if(!n) 
		{
			isDetecting = false;
			busy = false;
		}
	}
	public bool canDetectCheck()
	{
		if(
			grounded
			&& !falling
			&& !thrown
			&& !grabbed
			&& !isDetecting // odd one out
			&& !isTracking
		) return true;

		return false;
	}
	public void setTrack(bool n)
	{
		if(n)
		{
			isTracking = true;
			isPatrolling = false;
			hasDetected = true;
			busy = true;
		}
		if(!n) 
		{
			isTracking = false;
			hasDetected = false;
			busy = false;
		}
	}
	public bool canTrackCheck()
	{
		if(
			grounded
			&& !falling
			&& !thrown
			&& !grabbed
			&& !isDetecting
			&& !isTracking
		) return true;

		return false;
	}
	public void setGrabbed(bool n) // where you set this for grabbed, because it is onInputHold, do a if(!grabbed) check so it doesn't run every frame
	{
		if(n)
		{
			grabbed = true;
			isTracking = false;
			busy = true;
		}
		if(!n) 
		{
			grabbed = false;
			thrown = true;
			busy = false;
		}
	}
	public void setThrown(bool n)
	{
		if(n)
		{
			thrown = true;
			busy = true;
		}
		if(!n) 
		{
			thrown = false;
			busy = false;
		}
	}
	
	public void setArray(object[] objArray)
	{
	}
}
