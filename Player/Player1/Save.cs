using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player1 
{
	public class Save : MonoBehaviour {

		Player1.Main self;
		public SaveSystem saveSystem;
		
		void Start () {
			self = GetComponent<Player1.Main>();
		}
		
		public void Run () 
		{
			string _s = SETTINGS.saveFileName;
			// switch(SETTINGS.saveFileName)
			// {
			// 	case "file0.steameng":
			// 		_s = "file0.steameng";
			// 		break;
			// 	case "file1.steameng":
			// 		_s = "file1.steameng";
			// 		break;
			// 	case "file2.steameng":
			// 		_s = "file2.steameng";
			// 		break;
			// }
			
			// Appears we need both, one for settings, one for scripatable objects
			saveSystem.Save(saveslot:SETTINGS.saveSlot);
			SAVELOAD.Save(_s);
			Debug.Log(_s);
		}
	}
}
