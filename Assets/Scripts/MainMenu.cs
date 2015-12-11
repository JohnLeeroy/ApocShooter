using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	Rect rNewGame, rOptions,rExtra, rOptionsBack;
	bool bOptions = false;
	
	void Awake()
	{
		rNewGame = new Rect(Screen.width * .1f, Screen.height * .6f, Screen.width * .3f, Screen.height * .2f);
		rOptions = new Rect(Screen.width * .5f, Screen.height * .6f, Screen.width * .3f, Screen.height * .2f);
		//rExtra = new Rect(Screen.width * .6f, Screen.height * .6f, Screen.width * .3f, Screen.height * .2f);
		rOptionsBack = new Rect(Screen.width * .6f, Screen.height * .6f, Screen.width * .3f, Screen.height * .2f);
	}
	
	void Start () 
	{
		GameObject.Find("gtHighScore").guiText.text = "Highscore : " + PlayerPrefs.GetInt("HScore", 0);
	}
	
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		if(!bOptions)
		{
			if(GUI.Button(rNewGame, "Start Game"))
			{
				Application.LoadLevel("Game");
			}
			if(GUI.Button(rOptions, "Options"))
			{
				bOptions = true;
			}
			if(GUI.Button(rExtra, "Extra"))
			{
				Application.OpenURL("www.skyparlorstudios.com");
			}		
		}
		else
		{
			if(GUI.Button(rOptionsBack, "Back"))
			{
				bOptions = false;	
			}
		}
		
	}
}
