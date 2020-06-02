using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	// public GameObject player;
	// private Vector3 offset;
	public GameObject alucard;
	public Player1.Controller2D target;
	public float verticalOffset;
	public float lookAheadDistX;
	public float lookSmoothTimeX;
	public float verticalSmoothTime;
	public Vector2 focusAreaSize;
	public Player.InputManager InputManager;


	FocusArea focusArea;

	float currentLookAheadX;
	float targetLookAheadX;
	float lookAheadDirX;
	float smoothLookVelocityX;
	float smoothLookVelocityY;
	
	bool lookAheadStopped;

	
	
	void Start()
	{
		focusArea = new FocusArea(target.collider.bounds, focusAreaSize);
		alucard = GameObject.Find("alucard");
		InputManager = alucard.GetComponent<Player.InputManager>();
	}
	
	void LateUpdate()
	{
		focusArea.Update(target.collider.bounds);
		Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
		if(focusArea.velocity.x != 0)
		{
			lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
			int input = 0;
			if(InputManager.LastInputHold("RIGHT")) input = 1;
			if(InputManager.LastInputHold("RIGHT")) input = -1;
			if(Mathf.Sign(input) == Mathf.Sign(focusArea.velocity.x) && input != 0)
			{
				lookAheadStopped = false;
				targetLookAheadX = lookAheadDirX * lookAheadDistX;
			}
			else
			{
				if(!lookAheadStopped)
				{
					lookAheadStopped = true;
					targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDistX - currentLookAheadX)/4f;
				}
			}
		}

		targetLookAheadX = lookAheadDirX * lookAheadDistX;
		currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

		focusPosition.y = Mathf.SmoothDamp(transform.position.y,focusPosition.y, ref smoothLookVelocityY, verticalSmoothTime);
		focusPosition += Vector2.right * currentLookAheadX;
		transform.position = (Vector3)focusPosition + Vector3.forward * -10;
	}

	void OnDrawGizmos() 
	{
		// Gizmos.color = new Color(1,0,0,0.5f);
		// Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}

	struct FocusArea
	{
		public Vector2 center;
		float left,right;
		float top,bottom;
		public Vector2 velocity;


		public FocusArea(Bounds targetBounds, Vector2 size)
		{
			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;
			velocity = Vector2.zero;
			center = new Vector2((left+right)/2, (top+bottom)/2);

		}


		public void Update(Bounds targetBounds)
		{
			float shiftX = 0;
			if (targetBounds.min.x < left)
			{
				shiftX = targetBounds.min.x - left;
			}
			else if (targetBounds.max.x > right)
			{
				shiftX = targetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if (targetBounds.min.y < bottom)
			{
				shiftY = targetBounds.min.y - bottom;
			}
			else if (targetBounds.max.y > top)
			{
				shiftY = targetBounds.max.y - top;
			}
			bottom += shiftY;
			top += shiftY;
			center = new Vector2((left+right)/2,(top+bottom)/2);
			velocity = new Vector2(shiftX, shiftY);
		}

	}

	// // Use this for initialization
	// void Start () 
	// {
	// 	player = GameObject.Find("alucard");
	// 	offset = transform.position - player.transform.position;
	// }
	
	// // Update is called once per frame
	// void LateUpdate () {

	// 	// transform.position = player.transform.position + offset;
	// 	transform.position = player.transform.position;

	// }
}
