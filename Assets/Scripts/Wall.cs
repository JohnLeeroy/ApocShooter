using UnityEngine;
using System.Collections;

public class Wall : Unit {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	protected override void Hit(int _Dmg)
	{
		nHealth -= 	_Dmg;
		if(nHealth <= 0)
			Destroy(gameObject);
		//Debug.Log("base hit for " + _Damage);	
		Debug.Log("Base Hit for " + _Dmg + " HP Left: " +nHealth);		
	}
}
