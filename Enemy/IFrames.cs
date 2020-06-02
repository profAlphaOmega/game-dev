using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy 
{
	public class IFrames : MonoBehaviour {

		public int _iframes;
		public bool _isInvincible;



		void Start () 
		{
			_isInvincible = false;
		}

		public IEnumerator StartIFrames()
		{
			_isInvincible = true;
			yield return StartCoroutine(UTILS.WaitForFrames(_iframes));
			_isInvincible = false;
		}
	}
}
