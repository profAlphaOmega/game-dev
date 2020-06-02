using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour 
{
	public bool canMove {get; set;}
	public bool canAnimate {get; set;}
	public bool grounded {get; set;}
	public bool facingRight {get; set;}
	public int lookDirection {get; set;}
	public int dirX {get; set;}
	public int dirY {get; set;}

	public bool backdashing {get; set;}
	public bool frontdashing {get; set;}
	public bool falling {get; set;}
	public bool grabbing {get; set;}
	public bool jumping {get; set;}
	public bool jumped {get; set;}
	public bool wavedash {get; set;}
	public bool shinesparking {get; set;}
	public bool wallGrabbing {get; set;}
	public bool wallSliding {get; set;}
	public bool crouching {get; set;}
	public bool zipping {get; set;}
	public bool attackLag {get; set;}
	public bool attackLagCancelled {get; set;}
	public bool isAttackLagging {get; set;}
	public bool inputDisabled {get; set;}
	public bool normalPhysics {get; set;}
	public bool waterPhysics {get; set;}
	public bool icePhysics {get; set;}
	public bool customPhysics {get; set;}

	public PlayerState()
	{
		// Physics
		normalPhysics = true;
		waterPhysics = false;
		icePhysics = false;
		customPhysics = false;
		
		// High Level States
		canMove = true;
		inputDisabled = false;
		canAnimate = true;

		grounded = false;
		falling = false;
		crouching = false;
		wallSliding = false;
		
		// Special Moves
		backdashing =  false;
		frontdashing = false;
		zipping = false;
		wavedash = false;
		shinesparking = false;
		
		// Jump
		jumping = false;
		jumped = false;
		
		// Grabbing
		grabbing = false;
		wallGrabbing = false;
		
		// Attacking
		attackLag = false;
		attackLagCancelled = false;
		isAttackLagging = false;
		
		// Direction
		dirX = 1;
		dirY = 1;
		lookDirection = 270;
		facingRight = true;
	}
	public void Reset()
	{
		// Physics
		normalPhysics = true;
		waterPhysics = false;
		icePhysics = false;
		customPhysics = false;
		
		// High Level States
		canMove = true;
		inputDisabled = false;
		canAnimate = true;

		grounded = false;
		falling = false;
		crouching = false;
		wallSliding = false;
		
		// Special Moves
		backdashing =  false;
		frontdashing = false;
		zipping = false;
		wavedash = false;
		shinesparking = false;
		
		// Jump
		jumping = false;
		jumped = false;
		
		// Grabbing
		grabbing = false;
		wallGrabbing = false;
		
		// Attacking
		attackLag = false;
		attackLagCancelled = false;
		isAttackLagging = false;
		
		// Direction
		dirX = 1;
		dirY = 1;
		lookDirection = 270;
		facingRight = true;
		
		
		// normalPhysics = true;
		// waterPhysics = false;
		// customPhysics = false;
		// backdashing =  false;
		// frontdashing = false;
		// jumping = false;
		// jumped = false;
		// canMove = true;
		// canAnimate = true;
		// grounded = false;
		// falling = false;
		// grabbing = false;
		// wallGrabbing = false;
		// wallSliding = false;
		// crouching = false;
		// zipping = false;
		// attackLag = false;
		// attackLagCancelled = false;
		// isAttackLagging = false;
		// inputDisabled = false;
		// wavedash = false;
		// dirX = 1;
		// dirY = 1;
		// lookDirection = 0;
		// facingRight = true;
	}
}