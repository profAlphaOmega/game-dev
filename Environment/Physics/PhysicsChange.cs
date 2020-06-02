using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsChange : MonoBehaviour
{
	/// <summary>
	/// Trigger Class that handles changing Physics States and their Values
	/// </summary>

	public string physicsState;
	public bool setCustom = false;
	public bool revertPrevious = false;
	public Friction customFriction;
	public float customGroundFrictionValue;
	public float customAirFrictionValue;

	void OnTriggerEnter2D(Collider2D other) 
	{

		switch(physicsState)
		{
			default:
				break;
			case "Water":
				if(other.CompareTag("Player"))
				{
					other.GetComponent<Player1.Main>().state.waterPhysics = true;
				}
				break;
			case "Custom":
				if(other.CompareTag("Player"))
				{
					other.GetComponent<Player1.Main>().state.customPhysics = true;
					if(setCustom)
					{
						customFriction.GroundFriction = customGroundFrictionValue;
						customFriction.AirFriction = customAirFrictionValue;
					}
				}
				break;
		}
	}

	void OnTriggerExit2D(Collider2D other)
    {
        switch(physicsState)
		{
			default:
				break;
			case "Water":
				if(other.CompareTag("Player"))
				{
					other.GetComponent<Player1.Main>().state.waterPhysics = false;
				}
				break;
			case "Custom":
				if(other.CompareTag("Player"))
				{
					other.GetComponent<Player1.Main>().state.customPhysics = false;
				}
				break;
		}
    }


}
