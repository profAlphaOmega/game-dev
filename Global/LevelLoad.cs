using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]
public class LevelLoad : MonoBehaviour {

	public string _next;
	public bool _additive = false;

	Vector3 _spawnPosition = new Vector3(-50,1,0);
	Quaternion _rotation = Quaternion.Euler(0,0,0);
	string _unloadScene;
	public GameObject alucard;
	public Scene _nextScene;
	AsyncOperation a;
	public int _spawnPositionIndex = 0;

	void Start()
	{
		alucard = GameObject.Find("alucard");
		switch(_next) 
		{
			case "SceneFD":
				_spawnPosition = SCENES.FD.spawnPoint[_spawnPositionIndex];
				break;
			case "SceneLegion":
				_spawnPosition = SCENES.Legion.spawnPoint[_spawnPositionIndex];
				break;
			case "SceneStage":
				_spawnPosition = SCENES.Stage.spawnPoint[_spawnPositionIndex];
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{	
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene()
	{
		if (_additive)
            {
                SETTINGS.lastScene = SceneManager.GetActiveScene().name;
				AsyncOperation a = SceneManager.LoadSceneAsync(_next, LoadSceneMode.Additive);
				while (!a.isDone)
				{
					yield return null;
				}
				_nextScene = SceneManager.GetSceneByName(_next);
				SceneManager.SetActiveScene(_nextScene);
				
				SETTINGS.currentScene = SceneManager.GetActiveScene().name;
				
				SceneManager.MoveGameObjectToScene(alucard, _nextScene); 
				alucard.transform.SetPositionAndRotation(_spawnPosition, _rotation);
            }
		else
			{
				// SceneManager.LoadScene(_next);
				SETTINGS.lastScene = _unloadScene = SceneManager.GetActiveScene().name;
				AsyncOperation a = SceneManager.LoadSceneAsync(_next, LoadSceneMode.Additive);
				while (!a.isDone)
				{
					yield return null;
				}
				
				//set active theme
				_nextScene = SceneManager.GetSceneByName(_next);
				SceneManager.SetActiveScene(_nextScene);
				SETTINGS.currentScene = SceneManager.GetActiveScene().name;
				
				// move player
				alucard.transform.SetPositionAndRotation(_spawnPosition, _rotation);
				
				//unload last scene
				SceneManager.MoveGameObjectToScene(alucard, _nextScene);
				SceneManager.UnloadSceneAsync(_unloadScene);
				// yield return new WaitForSeconds(5); 
			}
	}
}
