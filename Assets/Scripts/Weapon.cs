using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour 
{
	public string sName;
	protected int nDmgMin = 0, nDmgMax = 0, nClipSize = 0;
	protected float fRange = 0, fReloadTime = 0, fPushBack = 0;
	
	public GameObject prefabBullet;
	public bool bFiring = false, bReloading = false;
	protected float fROF, fROFTimer;
	
	public int nCurClip;
	protected float fReloadTimer;
	
	public void Init(string _Name, int _DmgMin, int _DmgMax, int _Clip, float _Range, float _Reload, float _Pushback)
	{
		sName = _Name;
		nDmgMin = _DmgMin;
		nDmgMax = _DmgMax;
		nClipSize = _Clip;
		fRange = _Range;
		fReloadTime = _Reload;
		fPushBack = _Pushback;
		
		nCurClip = nClipSize;
		fReloadTimer = fReloadTime;
		switch(sName)
		{
		case "Pistol":
			fROF = 0.35f;
			fROFTimer = 0;
			break;
		case "SMG":
			fROF = 0.05f;
			fROFTimer = 0;
			break;
		case "Shotgun":
			fROF = 0.7f;
			fROFTimer = 0;
			break;
		case "ARifle":
			fROF = 0.05f;
			fROFTimer = 0;
			break;
		case "MGun":
			fROF = 0.1f;
			fROFTimer = 0;
			break;		
		}
	}
		
	
	// Update is called once per frame
	void Update () 
	{
		if(!bFiring && !bReloading)
			fROFTimer -= Time.deltaTime;
	}
	
	public virtual void FireWeapon()
	{
		StartCoroutine(Fire());
	}
	protected virtual IEnumerator Fire()
	{
		bFiring = true;
		while((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))&& !Player.bSwitching)
		{
			while(bReloading)
				yield return 0;
			
			fROFTimer -= Time.deltaTime;
			if(fROFTimer <= 0.0f)
			{
				Quaternion rotation = Quaternion.AngleAxis(Random.Range(-2, 2), Vector3.up);
				GameObject newBullet = Instantiate(prefabBullet, transform.position,rotation * transform.rotation) as GameObject;
				newBullet.GetComponent<Bullet>().Init(Random.Range(nDmgMin, nDmgMax), fRange, fPushBack, gameObject);
				newBullet.name = "Bullet";
				fROFTimer = fROF;
				
				nCurClip--;
				if(nCurClip <= 0)
					StartCoroutine(Reload());
			}
			yield return 0;
		}
		bFiring = false;
	}
	
	protected IEnumerator Reload()
	{
		bReloading = true;
		while(fReloadTimer >= 0)
		{
			fReloadTimer -= Time.deltaTime;
			yield return 0;
		}
		nCurClip = nClipSize;
		fReloadTimer = fReloadTime;
		bReloading = false;
	}
	
		public void ReloadGun()
	{
		StartCoroutine(Reload());
	}
}

public class Shotgun : Weapon
{
	float fSpread = 20;
	int nShells = 6;
	
	public void Init(string _Name, int _DmgMin, int _DmgMax, int _Clip, float _Range, float _Reload, float _Pushback, int _Shells, float _Spread)
	{
		sName = _Name;
		nDmgMin = _DmgMin;
		nDmgMax = _DmgMax;
		nClipSize = _Clip;
		fRange = _Range;
		fReloadTime = _Reload;
		fPushBack = _Pushback;
		nShells = _Shells;
		fSpread = _Spread;
		
		nCurClip = nClipSize;
		fReloadTimer = fReloadTime;
		switch(sName)
		{
		case "Shotgun":
			fROF = 0.7f;
			fROFTimer = 0;
			break;
		}
	}	
	
	public override void FireWeapon()
	{
		StartCoroutine(Fire());
	}
	protected override IEnumerator Fire()
	{
		bFiring = true;
		while((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && !Player.bSwitching)
		{
			while(bReloading)
				yield return 0;
			
			fROFTimer -= Time.deltaTime;
			if(fROFTimer <= 0.0f)
			{
				for(int i = 0; i < nShells; i++)
				{
					Quaternion rotation = Quaternion.AngleAxis(Random.Range(-fSpread, fSpread), Vector3.up);
					GameObject newBullet = Instantiate(prefabBullet, transform.position,rotation * transform.rotation) as GameObject;
					newBullet.name = "Bullet";
					newBullet.GetComponent<Bullet>().Init(Random.Range(nDmgMin, nDmgMax), fRange, fPushBack, gameObject);
				}
				fROFTimer = fROF;
				
				nCurClip--;
				if(nCurClip <= 0)
					StartCoroutine(Reload());
			}
			yield return 0;
		}
		bFiring = false;
	}
	
	
	protected void UpdateDmg()
	{
		
	}
	
}

public class TurretWpn : Weapon
{
	float fSpread = 20;
	
	public void Init(string _Name, int _DmgMin, int _DmgMax, int _Clip, float _Range, float _Reload, float _Pushback, float _Spread)
	{
		sName = _Name;
		nDmgMin = _DmgMin;
		nDmgMax = _DmgMax;
		nClipSize = _Clip;
		fRange = _Range;
		fReloadTime = _Reload;
		fPushBack = _Pushback;
		fSpread = _Spread;
		
		nCurClip = nClipSize;
		fReloadTimer = fReloadTime;
		
		fROF = 1.5f;
		fROFTimer = 0;
//		switch(sName)
//		{
//		case "Shotgun":
//			
//			break;
//		}
	}	
	
	public override void FireWeapon()
	{
		StartCoroutine(Fire());
	}
	protected override IEnumerator Fire()
	{
		bFiring = true;
		while(bFiring)
		{
			while(bReloading)
				yield return 0;
			
			fROFTimer -= Time.deltaTime;
			if(fROFTimer <= 0.0f)
			{
				Quaternion rotation = Quaternion.AngleAxis(Random.Range(-fSpread, fSpread), Vector3.up);
				GameObject newBullet = Instantiate(prefabBullet, transform.position, rotation * transform.parent.rotation) as GameObject;
				newBullet.name = "Bullet";
				newBullet.GetComponent<Bullet>().Init(Random.Range(nDmgMin, nDmgMax), fRange, fPushBack, gameObject);
				fROFTimer = fROF;
				
				nCurClip--;
				if(nCurClip <= 0)
					StartCoroutine(Reload());
			}
			yield return 0;
		}
		bFiring = false;
	}
}

//Pistol
//Dmg 
//Range
//Clip
//Range

//Pushback - 5 lvls
//Reload Speed
