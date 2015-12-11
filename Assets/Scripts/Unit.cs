using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public int nHealth = 0;
	public float fSpeed = 0;
	public int nUnitType = -1; //0 player, 1 trap, 2 turret, 3 wall, 4 zombie, 5 rager, 6 boomer, 7 spitter, 8 tank
	
	protected CharacterController cController;
	// Use this for initialization
	protected virtual void Start () 
	{
	
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
	
	}
	
	protected virtual void Hit(int _Damage)
	{
		nHealth -= 	_Damage;
		//Debug.Log("base hit for " + _Damage);	
		Debug.Log("Base Hit for " + _Damage + " HP Left: " +nHealth);	
	}
	
	protected virtual void Hit(Bullet.HitData _HitData)
	{
		nHealth -= 	_HitData.nDmg;
		//Debug.Log("base hit for " + _Damage);	
		Debug.Log("Base Hit for " + _HitData.nDmg + " HP Left: " +nHealth);	
	}
	void OnCollisionExit(Collision collision)
	{
		
	}
}
