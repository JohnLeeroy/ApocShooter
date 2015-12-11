using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	
	public static float fDifficulty = 1;
	float fDFTimer = 40;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		fDFTimer -= Time.deltaTime;
		if(fDFTimer < 0)
		{
			fDFTimer = 40;	
			fDifficulty *= 1.1f;
		}
		
//		if(Input.GetKeyDown(KeyCode.V))
//		{
//			Application.CaptureScreenshot("Screeny" + Random.Range(0,50));
//		}
	}
	
}
