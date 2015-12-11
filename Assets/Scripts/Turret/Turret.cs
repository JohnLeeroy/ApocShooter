using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : Unit {
	
	public GameObject goPivot, goGunR, goGunL, prefabBullet;
	public float fRange = 50;
	
	Weapon cGunR, cGunL;
	List<GameObject> lTargets;
	
	float fScanInterval = 1.5f, fScanTimer = 0;
	void Awake()
	{
		lTargets = new List<GameObject>();	
	}
	
	void Start () 
	{
		cGunR = goGunR.AddComponent<TurretWpn>();
		((TurretWpn)cGunR).Init("MGR", 7, 9, 15, 12.0f, 3.0f, 1.5f, 5);
		cGunR.prefabBullet = prefabBullet;
		
		cGunL = goGunL.AddComponent<TurretWpn>();
		((TurretWpn)cGunL).Init("MGL", 7, 9, 15, 12.0f, 3.0f, 1.5f, 5);
		cGunL.prefabBullet = prefabBullet;
		
		InvokeRepeating("ScanArea", 0.0f, fScanInterval);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void ScanArea()
	{
		lTargets.Clear();
		fScanTimer = fScanInterval;	
		Collider[] hitCol = Physics.OverlapSphere(transform.position, fRange);
		for(int i = 0; i < hitCol.Length; i++)
		{
			Unit cUnit = hitCol[i].collider.GetComponent<Unit>();
			if(cUnit != null)
			{
				if(cUnit.nUnitType > 3)
				{
					lTargets.Add(hitCol[i].gameObject);
				}
			}
		}
		
		if(lTargets.Count > 0)
		{
			if(!cGunL.bFiring && !cGunR.bFiring)
			{
			StartCoroutine("Fire");
				
			}
		}
	}
	
	IEnumerator Fire()
	{
		cGunL.FireWeapon();
		cGunR.FireWeapon();
		while(lTargets.Count > 0)
		{
			if(lTargets[0] != null)
			{
				goPivot.transform.LookAt(lTargets[0].transform.position);	
			}
			else
				lTargets.RemoveAt(0);
			
			yield return null;		
		}
		cGunL.bFiring = false;
		cGunR.bFiring = false;
	}
	
#if UNITY_EDITOR
	protected void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, fRange);
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * fRange);
	}
#endif
}
