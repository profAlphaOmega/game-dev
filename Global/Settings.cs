using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SETTINGS 
{
	// All Default values are implied as New Game Settings

	// Scene Settings
	public static string lastScene = "SceneStartMenu";
	public static string currentScene = "SceneStage";
	public static int spawnPointIndex = 0;
	
	// Game Settings
	public static int Players = 1;
	// public static int connectionType = 0; 
	public static string saveFileName = "file0.steameng";
	public static string saveSlot = "0";
	public static string controllerDevice = "XBOX"; // XBOX or KEYBOARD
	public static string controllerDevice2 = "KEYBOARD"; // XBOX or KEYBOARD
	

	// Buttons
	public static string A_xboxBtn = "JUMP";
	public static string X_xboxBtn = "ATTACK";
	public static string B_xboxBtn = "OPTION0";
	public static string Y_xboxBtn = "OPTION1"; 
	public static string LB_xboxBtn = "OPTION2";
	public static string RB_xboxBtn = "OPTION3";
	public static string LSIN_xboxBtn = "OPTION4";
	public static string RSIN_xboxBtn = "OPTION5";
	public static string SELECT_xboxBtn = "SELECT";
	public static string START_xboxBtn = "START";
	public static string LT_xboxBtn = "DASHLEFT";
	public static string RT_xboxBtn = "DASHRIGHT";
	public static string LSX_xboxBtn = "DX_LS";
	public static string LSY_xboxBtn = "DY_LS";
	public static string RSX_xboxBtn = "DX_RSX";
	public static string RSY_xboxBtn = "DY_RSY";
	public static string DPADX_xboxBtn = "DX_DPADX";
	public static string DPADY_xboxBtn = "DY_DPADY";

	// Player2
	public static string A_xboxBtn2 = "JUMP";
	public static string X_xboxBtn2 = "ATTACK";
	public static string B_xboxBtn2 = "OPTION0";
	public static string Y_xboxBtn2 = "OPTION1"; 
	public static string LB_xboxBtn2 = "OPTION2";
	public static string RB_xboxBtn2 = "OPTION3";
	public static string LSIN_xboxBtn2 = "OPTION4";
	public static string RSIN_xboxBtn2 = "OPTION5";
	public static string SELECT_xboxBtn2 = "SELECT";
	public static string START_xboxBtn2 = "START";
	public static string LT_xboxBtn2 = "DASHLEFT";
	public static string RT_xboxBtn2 = "DASHRIGHT";
	public static string LSX_xboxBtn2 = "DX_LS";
	public static string LSY_xboxBtn2 = "DY_LS";
	public static string RSX_xboxBtn2 = "DX_RSX";
	public static string RSY_xboxBtn2 = "DY_RSY";
	public static string DPADX_xboxBtn2 = "DX_DPADX";
	public static string DPADY_xboxBtn2 = "DY_DPADY";


	public static string[] A_key = {"Space","JUMP"};
	public static string[] X_key = {"R","ATTACK"};
	public static string[] B_key = {"F","OPTION0"};
	public static string[] Y_key = {"T","OPTION1"};
	public static string[] LB_key = {"Alpha1","OPTION2"};
	public static string[] RB_key = {"Alpha2","OPTION3"};
	public static string[] LSIN_key = {"Y","OPTION4"};
	public static string[] RSIN_key = {"H","OPTION5"};
	public static string[] SELECT_key = {"N","SELECT"};
	public static string[] START_key = {"Return","START"};
	public static string[] LT_key = {"Q","DASHLEFT"};
	public static string[] RT_key = {"E","DASHRIGHT"};
	public static string[] UP_key = {"W","UP"};
	public static string[] DOWN_key = {"S","DOWN"};
	public static string[] LEFT_key = {"A","LEFT"};
	public static string[] RIGHT_key = {"D","RIGHT"};
	
	// Keyboard
	public static KeyCode A_keycode;
	public static KeyCode X_keycode;
	public static KeyCode B_keycode;
	public static KeyCode Y_keycode;
	public static KeyCode LB_keycode;
	public static KeyCode RB_keycode;
	public static KeyCode LSIN_keycode;
	public static KeyCode RSIN_keycode;
	public static KeyCode SELECT_keycode;
	public static KeyCode START_keycode;
	public static KeyCode LT_keycode;
	public static KeyCode RT_keycode;
	public static KeyCode UP_keycode;
	public static KeyCode DOWN_keycode;
	public static KeyCode LEFT_keycode;
	public static KeyCode RIGHT_keycode;

	public static void SetXBOXBindings()
	{

	}

	public static void SetKeyboardMapping()
	{
		A_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.A_key[0]);
		X_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.X_key[0]);
		B_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.B_key[0]);
		Y_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.Y_key[0]);
		LB_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.LB_key[0]);
		RB_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.RB_key[0]);
		LSIN_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.LSIN_key[0]);
		RSIN_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.RSIN_key[0]);
		SELECT_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.SELECT_key[0]);
		START_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.START_key[0]);
		LT_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.LT_key[0]);
		RT_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.RT_key[0]);
		UP_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.UP_key[0]);
		DOWN_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.DOWN_key[0]);
		LEFT_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.LEFT_key[0]);
		RIGHT_keycode = (KeyCode) System.Enum.Parse(typeof(KeyCode), SETTINGS.RIGHT_key[0]);
	}

}
