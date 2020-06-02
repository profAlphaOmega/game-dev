using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SCENES
{

	public static IEnumerator LoadScene (string LoadScene)
	{
		AsyncOperation a = SceneManager.LoadSceneAsync(LoadScene, LoadSceneMode.Additive);
		while (!a.isDone)
		{
			yield return null;
		}
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(LoadScene));
	}
	// public static IEnumerator LoadScene(Scene LoadScene, Vector3 SpawnPoint, Quaternion _rotation, List<Transform> TransformList = null,  string UnloadScene=null)
	// {
		
	// 	// // Load All Default values here of ScriptableOjbects here
	// 	// // Create Player1	
		
	// 	// player.name = "alucard";
		
	// 	// // PUT THIS IN ANOTHER FUNCTION, put thing to load in a list and pass that to the Load function
	// 	// // Load where to go
	// 	// AsyncOperation a = SceneManager.LoadSceneAsync(LoadScene, LoadSceneMode.Additive);
	// 	// while (!a.isDone)
	// 	// {
	// 	// 	yield return null;
	// 	// }
		
	// 	// // Set Scene Active
	// 	// LoadScene = SceneManager.GetSceneByName(LoadScene);
	// 	// SceneManager.SetActiveScene(LoadScene);
		
	// 	// // Move Player(s) to Scene at SpawnPoint
	// 	// SpawnPoint = SCENES.SpawnPointLookUp(SETTINGS.currentScene, SETTINGS.spawnPointIndex);
	// 	// player.transform.SetPositionAndRotation(SpawnPoint, _rotation);
	// 	// SceneManager.MoveGameObjectToScene(player.gameObject, LoadScene);
	// 	// if(UnloadScene != null)
	// 	// 	SceneManager.UnloadSceneAsync(UnloadScene);
		
	// }

	public static class StartMenu
	{
		public static string name = "StartMenu";
		public static string sceneName = "SceneStartMenu";
		public static Vector3[] spawnPoint = {new Vector3(0,0,0), new Vector3(0,0,0)};
	}

	public static class Legion
	{
		public static string name = "Legion";
		public static string sceneName = "SceneLegion";
		public static Vector3[] spawnPoint = {new Vector3(-1,1,0), new Vector3(-2,1,0)};
	} 

	public static class Stage
	{
		public static string name = "Stage";
		public static string sceneName = "SceneStage";
		public static Vector3[] spawnPoint = {new Vector3(-13.85f,10f,0), new Vector3(-6.24f,5.61f,0)};
	} 
	
	public static class FD
	{
		public static string name = "FD";
		public static string sceneName = "SceneFD";
		public static Vector3[] spawnPoint = {new Vector3(-50,1,0), new Vector3(-70,1,0)};
	} 

	public static Vector3 SpawnPointLookUp (string s, int index)
	{
		Vector3 SpawnPoint = new Vector3(0,0,0);
		switch(s)
			{
				default:
					SpawnPoint = SCENES.StartMenu.spawnPoint[index=0];
					break;

				case "SceneStage":
					SpawnPoint = SCENES.Stage.spawnPoint[index];
					break;
				case "SceneLegion":
					SpawnPoint = SCENES.Legion.spawnPoint[index];
					break;
				case "SceneFD":
					SpawnPoint = SCENES.FD.spawnPoint[index];
					break;
			}
		return SpawnPoint;
	}
}
