using UnityEngine;
using System.Collections;

public class Player : Unit {
	
	int MAX_HEALTH = 50;
	float MAX_ADREN = 100.0f, SWITCH_TIME = 3.0f;
	Weapon[] lWeapons;
	
	public GameObject prefabBullet, prefabSGBullet, prefabWall, prefabTurret;
	GameObject goGun;
	Weapon cEquippedWpn;
	float fSwitchWpnTimer = 0;
	public static bool bSwitching = false;
	
	
	float fSensitivity = 1;
	float vPrevMouseX;
	
	public static int nAdrenaline = 0;
	public static int nScore = 0;
	
	int nToken = 0;
//#if UNITY_EDITOR
	GUIText gtHPMeter, gtBulletMeter;
//#endif
	

	void Awake()
	{
		//Pistol, SMGD, Shotgun, AR, MG, Lazorzz
		lWeapons = new Weapon[6];
		Zombie.goPlayer = gameObject;
		Reward.goPlayer = gameObject;
		cController = GetComponent<CharacterController>();
		
//#if UNITY_EDITOR
		//gtHPMeter = GameObject.Find("_dbHealth").guiText;
		gtBulletMeter = GameObject.Find("_dbBullet").guiText;
//#endif
		
	}
	void Start () 
	{
		nUnitType = 0;
		nHealth = 50;
		fSpeed = 20.0f;
		goGun = GameObject.Find("Gun");
		
		//(string _Name, int _DmgMin, int _DmgMax, int _Clip, float _Range, float _Reload, float _Pushback)
		
		Weapon cPistol = goGun.AddComponent<Weapon>();
		cPistol.Init("Pistol", 5, 7, 12, 8.0f, 1.0f, 1.0f);
		cPistol.prefabBullet = prefabBullet;
		lWeapons[0] = cPistol;
		
		Weapon cSMG = goGun.AddComponent<Weapon>();
		cSMG.Init("SMG", 5, 7, 30, 8.0f, 2.0f,  0.0f);
		cSMG.prefabBullet = prefabBullet;
		lWeapons[1] = cSMG;
		
		Weapon cShotgun = goGun.AddComponent<Shotgun>();
		cShotgun.Init("Shotgun", 11, 15, 8, 8.0f, 4.0f,  4.0f);
		cShotgun.prefabBullet = prefabSGBullet;
		lWeapons[2] = cShotgun;
		
		Weapon cARifle = goGun.AddComponent<Weapon>();
		cARifle.Init("ARifle", 11, 15, 40, 20.0f, 1.0f,  1.0f);
		cARifle.prefabBullet = prefabBullet;
		lWeapons[3] = cARifle;

		Weapon cMachineGun = goGun.AddComponent<Weapon>();
		cMachineGun.Init("MGun", 13, 17, 100, 20.0f, 4.0f,  1.0f);
		cMachineGun.prefabBullet = prefabBullet;
		lWeapons[4] = cMachineGun;
		
		cEquippedWpn = cPistol;
		vPrevMouseX = Input.mousePosition.x;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}
	
	void CheckInput()
	{
		if(Input.GetKey(KeyCode.W))	
		{
			cController.Move(transform.forward * fSpeed * Time.deltaTime);
			//transform.position += transform.forward * fSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S))	
		{
			cController.Move(-transform.forward * fSpeed * Time.deltaTime);
			//transform.position -= transform.forward * fSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.A))	
		{
			cController.Move(-transform.right * fSpeed * Time.deltaTime);
			//transform.position -= transform.right * fSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D))	
		{
			cController.Move(transform.right * fSpeed * Time.deltaTime);
			//transform.position += transform.right * fSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.Alpha1))
		{
			EquipItems(0);
		}
		
		if(Input.GetKey(KeyCode.Alpha2))
		{
			EquipItems(1);
		}	
		if(Input.GetKey(KeyCode.Alpha3))
		{
			EquipItems(2);
		}
		
		if(Input.GetKey(KeyCode.Alpha4))
		{
			EquipItems(3);
		}	
		if(Input.GetKey(KeyCode.Alpha5))
		{
			EquipItems(4);
		}
		//MouseInput1
//	    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//		RaycastHit hit;
//		if(Physics.Raycast(mouseRay, out hit))
//		{
//			transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
//		}
		
		//MouseInput2
		float fDeltaX = Input.mousePosition.x - vPrevMouseX;
		transform.RotateAround(Vector3.up, fDeltaX * Time.deltaTime);
		vPrevMouseX = Input.mousePosition.x;
		
		//transform.RotateAround(Vector3.up, 20 * Time.deltaTime);
		
		
		
		
		
		if((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))&& !cEquippedWpn.bFiring && !cEquippedWpn.bReloading  && !bSwitching )
		{
			cEquippedWpn.FireWeapon();
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			cEquippedWpn.ReloadGun();
		}
		
		
		if(Input.GetKeyDown(KeyCode.Q) && nToken >= 2)
		{
			Instantiate(prefabWall, transform.position + transform.forward * 4.0f, Quaternion.identity);
			nToken -= 2;
		}
		
		if(Input.GetKeyDown(KeyCode.E) && bSpawnReady && nToken >= 10)
		{
			if(!Physics.Raycast(transform.position, transform.forward, 3))
			{
				Instantiate(prefabTurret, transform.position + transform.forward * 4.0f, Quaternion.identity);
				nToken -= 10;
				StartCoroutine(TurretSpawnCoolDown(5.0f));
			}
		}
	}
	
	public void AddAdrenaline(int _Adrenaline)
	{
		nAdrenaline += _Adrenaline;
		if(nAdrenaline >= MAX_ADREN)
		{
			//Go Ape-Shit
			
		}
	}
	
	void EquipItems(int _Index)
	{
		StopCoroutine("Fire");
		StartCoroutine(CR_EquipWpn(_Index));
	}
	
	IEnumerator CR_EquipWpn(int _Index)
	{
		fSwitchWpnTimer = SWITCH_TIME;
		bSwitching = true;
		while(fSwitchWpnTimer >= 0)
		{
			fSwitchWpnTimer -= Time.deltaTime;
			yield return 0;
		}
		cEquippedWpn = lWeapons[_Index];
		bSwitching = false;
	}
	
	void FixedUpdate()
	{
//#if UNITY_EDITOR
		//gtHPMeter.text = ;
		//gtBulletMeter.text = "Health: " + nHealth + "\nAdrenaline: " + nAdrenaline + "\nGun: " + cEquippedWpn.sName + "\nBullets: " + cEquippedWpn.nCurClip;
		gtBulletMeter.text = "Health: " + nHealth + "\nTokens: " + nToken + "\nGun: " + cEquippedWpn.sName + "\nBullets: " + cEquippedWpn.nCurClip;
		GameObject.Find("_gtScore").guiText.text = nScore.ToString();
//#endif
		
		if(nHealth <= 0)
		{
			Application.LoadLevel("MainMenu");
			if(nScore > PlayerPrefs.GetInt("HScore", 0))
			{
				PlayerPrefs.SetInt("HScore", nScore);
				PlayerPrefs.Save();	
			}
		}
	}
	
	public void AddToken(int _Num)
	{
		nToken += _Num;
		Debug.Log(nToken);
	}
	
	public void HealthReward()
	{
		nHealth +=  (int)(MAX_HEALTH * .25f);
		if(nHealth > MAX_HEALTH)
			nHealth = MAX_HEALTH;
	}
	
	bool bSpawnReady = true;
	float fCDTimer = 0;
	IEnumerator TurretSpawnCoolDown(float fCoolDown)
	{
		fCDTimer = fCoolDown;
		bSpawnReady = false;
		while(fCDTimer > 0)
		{
			fCDTimer -= Time.deltaTime;
			yield return 0;
		}
		
		bSpawnReady = true;
	}
}

//Adrenaline - become invincible, shoot twice as fast, move 1.5x faster


//Pistol
//Dmg 
//Range
//Clip
//Range

//Pushback - 5 lvls
//Reload Speed