using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuBtn : MonoBehaviour 
{
	/// <summary>
	/// Each LoadButton references a file(s) to load the correct data
	/// </summary>

	public Transform player;
	public LoadSystem loadSystem;
	public string filename = "file0.steameng";
	public string saveslot = "0";
	public bool newgame = true;
	public int spawnindex = 0;
	
	Scene _nextScene;
	Vector3 _spawnPosition;
	Quaternion _rotation = Quaternion.Euler(0,0,0);

	void Start () 
	{
		Button btn = GetComponent<Button>();
		Text _text = GetComponentInChildren<Text>();
        btn.onClick.AddListener(Load);

		// If not newgame, see if file exists and change btn text
		if(!newgame)
		{
			bool r = SAVELOAD.LoadFileCheck(filename);
			if (r)
			{
				_text.text = filename;
			}
			return;
		}
		if(newgame)
		{

			_text.text = "Start New";
			
			// Should deactivate the New Game Button if this happens
			if(SAVELOAD.LoadFileCheck("file1.steameng")&&SAVELOAD.LoadFileCheck("file1.steameng")&&SAVELOAD.LoadFileCheck("file1.steameng"))
			{
				_text.text = "Overwrite 1";
			}
			newgame = true;
			return;
		}
	}

	/// <summary>
	/// Bound to the button OnClick event
	/// If not new game, tries to load the file via ILoad
	/// ILoad loads new scene async, waits until completed
	/// Sets new scene activate, then moves moves/loads objects
	/// Unloads scene
	/// <para><param name="filename"> serialized file name</param></para>  
	/// <para><param name="saveslot"> which file slot to load</param></para>  
	/// </summary>
	void Load()
	{
		// If not new game, load game
		if(!newgame) 
		{
			bool r = SAVELOAD.Load(filename);
			loadSystem.Load(saveslot);
			// Set fileName for new games
			SETTINGS.saveFileName = filename;
			SETTINGS.saveSlot = saveslot;
			StartCoroutine(ILoad());
			return;
		}

		// If new game, first find an open slot
		if(newgame)
		{
			if(!SAVELOAD.LoadFileCheck("file1.steameng"))
			{
				SETTINGS.saveFileName = "file1.steameng";		
				SETTINGS.saveSlot = "1";
				StartCoroutine(ILoad());
				return;
			}
			if(!SAVELOAD.LoadFileCheck("file2.steameng"))
			{
				SETTINGS.saveFileName = "file2.steameng";		
				SETTINGS.saveSlot = "2";
				StartCoroutine(ILoad());
				return;
			}
			if(!SAVELOAD.LoadFileCheck("file3.steameng"))
			{
				SETTINGS.saveFileName = "file3.steameng";		
				SETTINGS.saveSlot = "3";
				StartCoroutine(ILoad());
				return;
			}
		}
		
		// Default Settings for New Games
		SETTINGS.saveFileName = "file1.steameng";		
		SETTINGS.saveSlot = "1";
		StartCoroutine(ILoad());
	}

	IEnumerator ILoad()
	{
		// Create Player1	
		player = Instantiate(player);
		player.name = "alucard";
		
		// PUT THIS IN ANOTHER FUNCTION, put thing to load in a list and pass that to the Load function
		// Load where to go
		AsyncOperation a = SceneManager.LoadSceneAsync(SETTINGS.currentScene, LoadSceneMode.Additive);
		while (!a.isDone)
		{
			yield return null;
		}
		
		// Set Scene Active
		_nextScene = SceneManager.GetSceneByName(SETTINGS.currentScene);
		SceneManager.SetActiveScene(_nextScene);
		
		// Move Player(s) to Scene at SpawnPoint
		_spawnPosition = SCENES.SpawnPointLookUp(SETTINGS.currentScene, SETTINGS.spawnPointIndex);
		player.transform.SetPositionAndRotation(_spawnPosition, _rotation);
		SceneManager.MoveGameObjectToScene(player.gameObject, _nextScene);
		
		// Unload Previous Scene
		SceneManager.UnloadSceneAsync("SceneStartMenu");
		
		// if(!newgame)
		// {
		// 	SAVELOAD.Load(filename);
			
		// 	SETTINGS.saveFileName = filename;

		// 	player = Instantiate(player);
		// 	player.name = "alucard";
		// 	AsyncOperation a = SceneManager.LoadSceneAsync(SETTINGS.currentScene, LoadSceneMode.Additive);
		// 	while (!a.isDone)
		// 	{
		// 		yield return null;
		// 	}

		// 	_nextScene = SceneManager.GetSceneByName(SETTINGS.currentScene);
		// 	SceneManager.SetActiveScene(_nextScene);

		// 	_spawnPosition = SCENES.SpawnPointLookUp(SETTINGS.currentScene, SETTINGS.spawnPointIndex);
		// 	player.transform.SetPositionAndRotation(_spawnPosition, _rotation);
		// 	SceneManager.MoveGameObjectToScene(player.gameObject, _nextScene);
		// 	SceneManager.UnloadSceneAsync("SceneStartMenu");
		// }
		// else
		// {
			
			
		// 	SETTINGS.saveFileName = filename;
			
		// 	player = Instantiate(player);
		// 	player.name = "alucard";
		// 	AsyncOperation a = SceneManager.LoadSceneAsync("SceneStage", LoadSceneMode.Additive);
		// 	while (!a.isDone)
		// 	{
		// 		yield return null;
		// 	}
		// 	_nextScene = SceneManager.GetSceneByName(SCENES.Stage.sceneName);
		// 	SceneManager.SetActiveScene(_nextScene);
		// 	player.transform.SetPositionAndRotation(_spawnPosition, _rotation);
		// 	SceneManager.MoveGameObjectToScene(player.gameObject, _nextScene);
		// 	SceneManager.UnloadSceneAsync("SceneStartMenu");
		// }
	}
}
