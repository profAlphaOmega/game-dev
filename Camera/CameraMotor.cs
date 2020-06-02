using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

	public float cameraMoveSpeed = 2f;
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(-cameraMoveSpeed * Time.deltaTime,0,0));
		}	
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(cameraMoveSpeed * Time.deltaTime,0,0));
		}	
	}
}
