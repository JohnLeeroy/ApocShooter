using UnityEngine;
using System.Collections;

public class BoxTile : MonoBehaviour {
	
	bool bUnlocked = false;
	public Material mLocked, mUnlocked;
	// Use this for initialization
	void Start () 
	{
		renderer.material = mLocked;
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	public void Unlock()
	{
		renderer.material = mUnlocked;
	}
}
