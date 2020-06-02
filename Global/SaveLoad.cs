using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


public static class SAVELOAD
{

	/// <summary>
	/// Seralizaes Static Class information
	/// Requires a game file name and a persistent path
	/// Uses TempData class for searlization
	/// <para><param name="gamefile"> String representation of gamefile name</param></para>  
	/// </summary>
	public static bool Save(string _gamefile) 
	{
		TempData data = new TempData();
		// _SaveSettings(data);

		// PLAYER1 HEALTH
		data.lastScene = SETTINGS.lastScene;                  
		data.currentScene = SETTINGS.currentScene;
		data.controllerDevice = SETTINGS.controllerDevice;
		data.controllerDevice2 = SETTINGS.controllerDevice2;
		// data.Players = SETTINGS.Players;
		// data.connectionType = SETTINGS.connectionType;
		data.saveFileName = SETTINGS.saveFileName;

		// KEY BINDINGS
		// data.A_xboxBtn = SETTINGS.A_xboxBtn;
		// data.X_xboxBtn = SETTINGS.X_xboxBtn;
		// data.B_xboxBtn = SETTINGS.B_xboxBtn;
		// data.Y_xboxBtn = SETTINGS.Y_xboxBtn; 
		// data.LB_xboxBtn = SETTINGS.LB_xboxBtn;
		// data.RB_xboxBtn = SETTINGS.RB_xboxBtn;
		// data.LSIN_xboxBtn = SETTINGS.LSIN_xboxBtn;
		// data.RSIN_xboxBtn = SETTINGS.RSIN_xboxBtn;
		// data.SELECT_xboxBtn = SETTINGS.SELECT_xboxBtn;
		// data.START_xboxBtn = SETTINGS.START_xboxBtn;
		// data.LT_xboxBtn = SETTINGS.LT_xboxBtn;
		// data.RT_xboxBtn = SETTINGS.RT_xboxBtn;
		// data.LSX_xboxBtn = SETTINGS.LSX_xboxBtn;
		// data.LSY_xboxBtn = SETTINGS.LSY_xboxBtn;
		// data.RSX_xboxBtn = SETTINGS.RSY_xboxBtn;
		// data.RSY_xboxBtn = SETTINGS.RSY_xboxBtn;
		// data.DPADX_xboxBtn = SETTINGS.DPADX_xboxBtn;
		// data.A_xboxBtn2 = SETTINGS.A_xboxBtn2;
		// data.X_xboxBtn2 = SETTINGS.X_xboxBtn2;
		// data.B_xboxBtn2 = SETTINGS.B_xboxBtn2;
		// data.Y_xboxBtn2 = SETTINGS.Y_xboxBtn2; 
		// data.LB_xboxBtn2 = SETTINGS.LB_xboxBtn2;
		// data.RB_xboxBtn2 = SETTINGS.RB_xboxBtn2;
		// data.LSIN_xboxBtn2 = SETTINGS.LSIN_xboxBtn2;
		// data.RSIN_xboxBtn2 = SETTINGS.RSIN_xboxBtn2;
		// data.SELECT_xboxBtn2 = SETTINGS.SELECT_xboxBtn2;
		// data.START_xboxBtn2 = SETTINGS.START_xboxBtn2;
		// data.LT_xboxBtn2 = SETTINGS.LT_xboxBtn2;
		// data.RT_xboxBtn2 = SETTINGS.RT_xboxBtn2;
		// data.LSX_xboxBtn2 = SETTINGS.LSX_xboxBtn2;
		// data.LSY_xboxBtn2 = SETTINGS.LSY_xboxBtn2;
		// data.RSX_xboxBtn2 = SETTINGS.RSY_xboxBtn2;
		// data.RSY_xboxBtn2 = SETTINGS.RSY_xboxBtn2;
		// data.DPADX_xboxBtn2 = SETTINGS.DPADX_xboxBtn2;
		// data.A_key = SETTINGS.A_key;
		// data.X_key = SETTINGS.X_key;
		// data.B_key = SETTINGS.B_key;
		// data.Y_key = SETTINGS.Y_key;
		// data.LB_key = SETTINGS.LB_key;
		// data.RB_key = SETTINGS.RB_key;
		// data.LSIN_key = SETTINGS.LSIN_key;
		// data.RSIN_key = SETTINGS.RSIN_key;
		// data.SELECT_key = SETTINGS.SELECT_key;
		// data.START_key = SETTINGS.START_key;
		// data.LT_key = SETTINGS.LT_key;
		// data.RT_key = SETTINGS.RT_key;
		// data.UP_key = SETTINGS.UP_key;
		// data.DOWN_key = SETTINGS.DOWN_key;
		// data.LEFT_key = SETTINGS.LEFT_key;
		// data.RIGHT_key = SETTINGS.RIGHT_key;
		
		// // INVENTORY
		// data.option0 = INVENTORY_P1.option0;
		// data.option1 = INVENTORY_P1.option1;
		// data.option2 = INVENTORY_P1.option2;
		// data.option3 = INVENTORY_P1.option3;
		// data.option4 = INVENTORY_P1.option4;
		// data.option5 = INVENTORY_P1.option5;
		// data.p2_option0 = INVENTORY_P2.option0;
		// data.p2_option1 = INVENTORY_P2.option1;
		// data.p2_option2 = INVENTORY_P2.option2;
		// data.p2_option3 = INVENTORY_P2.option3;
		// data.p2_option4 = INVENTORY_P2.option4;
		// data.p2_option5 = INVENTORY_P2.option5;

		try
		{
			BinaryFormatter bf = new BinaryFormatter();
			using(FileStream file = File.Create (Application.persistentDataPath + "/" + _gamefile)) // _gamefile
			{
				bf.Serialize(file, data);
				file.Close();
				Debug.Log("Saved at" + Application.persistentDataPath + "/" + _gamefile);
			}
		}
		catch {return false;}


		return true;
	}

	public static bool LoadFileCheck(string _gamefile)
	{
		if(!File.Exists(Application.persistentDataPath + "/" + _gamefile))
             return false;
		return true;
	}

	public static bool Load(string _gamefile) 
	{
		if(!File.Exists(Application.persistentDataPath + "/" + _gamefile))
            return false;

		// File Not Found
		// Redundant, test and take out top
		if(!LoadFileCheck(_gamefile))
			return false;

		TempData data;
		BinaryFormatter bf = new BinaryFormatter();
		using(FileStream file = File.Open(Application.persistentDataPath + "/" + _gamefile, FileMode.Open))
		{
			data = (TempData)bf.Deserialize(file);
			file.Close();
		}
			
			// PLAYER1 HEALTH
			// Debug.Log(AssetDatabase.GetAssetPath(Player1HP));
			// SETTINGS.Player1Health = data.player1health;
			
			SETTINGS.lastScene = data.lastScene;
			SETTINGS.currentScene = data.currentScene;
			SETTINGS.controllerDevice = data.controllerDevice;
			SETTINGS.controllerDevice2 = data.controllerDevice2;
			// SETTINGS.Players = data.Players;
			// SETTINGS.connectionType = data.connectionType;
			SETTINGS.saveFileName = data.saveFileName; 
			// SETTINGS.A_xboxBtn = data.A_xboxBtn;
			// SETTINGS.X_xboxBtn = data.X_xboxBtn;
			// SETTINGS.B_xboxBtn = data.B_xboxBtn;
			// SETTINGS.Y_xboxBtn = data.Y_xboxBtn;
			// SETTINGS.LB_xboxBtn = data.LB_xboxBtn;
			// SETTINGS.RB_xboxBtn = data.RB_xboxBtn;
			// SETTINGS.LSIN_xboxBtn = data.LSIN_xboxBtn;
			// SETTINGS.RSIN_xboxBtn = data.RSIN_xboxBtn;
			// SETTINGS.SELECT_xboxBtn = data.SELECT_xboxBtn;
			// SETTINGS.START_xboxBtn = data.START_xboxBtn;
			// SETTINGS.LT_xboxBtn = data.LT_xboxBtn;
			// SETTINGS.RT_xboxBtn = data.RT_xboxBtn;
			// SETTINGS.LSX_xboxBtn = data.LSX_xboxBtn;
			// SETTINGS.LSY_xboxBtn = data.LSY_xboxBtn;
			// SETTINGS.RSY_xboxBtn = data.RSX_xboxBtn;
			// SETTINGS.RSY_xboxBtn = data.RSY_xboxBtn;
			// SETTINGS.DPADX_xboxBtn = data.DPADX_xboxBtn;
			// SETTINGS.A_xboxBtn2 = data.A_xboxBtn2;
			// SETTINGS.X_xboxBtn2 = data.X_xboxBtn2;
			// SETTINGS.B_xboxBtn2 = data.B_xboxBtn2;
			// SETTINGS.Y_xboxBtn2 = data.Y_xboxBtn2;
			// SETTINGS.LB_xboxBtn2 = data.LB_xboxBtn2;
			// SETTINGS.RB_xboxBtn2 = data.RB_xboxBtn2;
			// SETTINGS.LSIN_xboxBtn2 = data.LSIN_xboxBtn2;
			// SETTINGS.RSIN_xboxBtn2 = data.RSIN_xboxBtn2;
			// SETTINGS.SELECT_xboxBtn2 = data.SELECT_xboxBtn2;
			// SETTINGS.START_xboxBtn2 = data.START_xboxBtn2;
			// SETTINGS.LT_xboxBtn2 = data.LT_xboxBtn2;
			// SETTINGS.RT_xboxBtn2 = data.RT_xboxBtn2;
			// SETTINGS.LSX_xboxBtn2 = data.LSX_xboxBtn2;
			// SETTINGS.LSY_xboxBtn2 = data.LSY_xboxBtn2;
			// SETTINGS.RSY_xboxBtn2 = data.RSX_xboxBtn2;
			// SETTINGS.RSY_xboxBtn2 = data.RSY_xboxBtn2;
			// SETTINGS.DPADX_xboxBtn2 = data.DPADX_xboxBtn2;
			// SETTINGS.A_key = data.A_key;
			// SETTINGS.X_key = data.X_key;
			// SETTINGS.B_key = data.B_key;
			// SETTINGS.Y_key = data.Y_key;
			// SETTINGS.LB_key = data.LB_key;
			// SETTINGS.RB_key = data.RB_key;
			// SETTINGS.LSIN_key = data.LSIN_key;
			// SETTINGS.RSIN_key = data.RSIN_key;
			// SETTINGS.SELECT_key = data.SELECT_key;
			// SETTINGS.START_key = data.START_key;
			// SETTINGS.LT_key = data.LT_key;
			// SETTINGS.RT_key = data.RT_key;
			// SETTINGS.UP_key = data.UP_key;
			// SETTINGS.DOWN_key = data.DOWN_key;
			// SETTINGS.LEFT_key = data.LEFT_key;
			// SETTINGS.RIGHT_key = data.RIGHT_key;
			// INVENTORY_P1.option0 = data.option0;
			// INVENTORY_P1.option1 = data.option1;
			// INVENTORY_P1.option2 = data.option2;
			// INVENTORY_P1.option3 = data.option3;
			// INVENTORY_P1.option4 = data.option4;
			// INVENTORY_P1.option5 = data.option5;
			// INVENTORY_P2.option0 = data.p2_option0;
			// INVENTORY_P2.option1 = data.p2_option1;
			// INVENTORY_P2.option2 = data.p2_option2;
			// INVENTORY_P2.option3 = data.p2_option3;
			// INVENTORY_P2.option4 = data.p2_option4;
			// INVENTORY_P2.option5 = data.p2_option5;
			Debug.Log("Loaded");
		
			return true;
	}

	static void _SaveSettings(TempData data)
	{
	}
	static void _SaveInventory(TempData data)
	{
	}
	static void _LoadSettings(TempData data)
	{
	}
	static void _LoadInventory(TempData data)
	{
	}
}

[Serializable]
public class TempData 
{
	//Settings
	public string lastScene;
	public string currentScene;
	public string controllerDevice;
	public string controllerDevice2;
	public int Players;
	public float player1health;
	public int connectionType;
	public string saveFileName;
	
	//Buttons
	public string A_xboxBtn;
	public string X_xboxBtn;
	public string B_xboxBtn;
	public string Y_xboxBtn;
	public string LB_xboxBtn;
	public string RB_xboxBtn;
	public string LSIN_xboxBtn;
	public string RSIN_xboxBtn;
	public string SELECT_xboxBtn;
	public string START_xboxBtn;
	public string LT_xboxBtn;
	public string RT_xboxBtn;
	public string LSX_xboxBtn;
	public string LSY_xboxBtn;
	public string RSX_xboxBtn;
	public string RSY_xboxBtn;
	public string DPADX_xboxBtn;
	public string A_xboxBtn2;
	public string X_xboxBtn2;
	public string B_xboxBtn2;
	public string Y_xboxBtn2;
	public string LB_xboxBtn2;
	public string RB_xboxBtn2;
	public string LSIN_xboxBtn2;
	public string RSIN_xboxBtn2;
	public string SELECT_xboxBtn2;
	public string START_xboxBtn2;
	public string LT_xboxBtn2;
	public string RT_xboxBtn2;
	public string LSX_xboxBtn2;
	public string LSY_xboxBtn2;
	public string RSX_xboxBtn2;
	public string RSY_xboxBtn2;
	public string DPADX_xboxBtn2;
	public string[] A_key;
	public string[] X_key;
	public string[] B_key;
	public string[] Y_key;
	public string[] LB_key;
	public string[] RB_key;
	public string[] LSIN_key;
	public string[] RSIN_key;
	public string[] SELECT_key;
	public string[] START_key;
	public string[] LT_key;
	public string[] RT_key;
	public string[] UP_key;
	public string[] DOWN_key;
	public string[] LEFT_key;
	public string[] RIGHT_key;
	
	//Inventory
	public string option0;
	public string option1;
	public string option2;
	public string option3;
	public string option4;
	public string option5;
	public string p2_option0;
	public string p2_option1;
	public string p2_option2;
	public string p2_option3;
	public string p2_option4;
	public string p2_option5;
}
