using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour {


	static DDOL instance;
	public static DDOL GetInstance()
	{
		return instance;
	}

	void Start () 
	{
		
		if (instance != null)
		{
			Destroy(this.gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	
	// SceneManager.sceneLoaded += OnSceneLoaded;
	// void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("LAST" + SETTINGS.lastScene);
    //     Debug.Log("CURRNET" + SETTINGS.currentScene);
    // }

}
