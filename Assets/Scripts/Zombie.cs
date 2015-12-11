using UnityEngine;
using System.Collections;

public class Zombie : Unit {
	
	public static GameObject goPlayer;
	public bool bAttacking = false;
	
	protected float fROF = 1.0f, fAttackTimer = 0;
	protected int nDmg = 0;
	public float fRange = 3.0f;
	
	public GameObject prefabHealth, prefabToken;
	
	void Awake()
	{
		cController = GetComponent<CharacterController>();
	}
	
	// Use this for initialization
	protected override void Start () 
	{
		//nUnitType = 4;
		//nHealth = 20;
		//fSpeed = 7.5f;
		
		nHealth = (int)(nHealth * GameManager.fDifficulty);
		nDmg = (int)(nDmg * GameManager.fDifficulty);
		switch(nUnitType)
		{
		case 4:
			nDmg = 5;
			StartCoroutine(CR_Attack());
			break;
		case 5:
			nDmg = 5;
			StartCoroutine(CR_Attack());
			break;
		case 6:
			//StartCoroutine(CR_Boom());
			break;
		case 8:
			nDmg = 10;
			StartCoroutine(CR_Attack());
			break;
		}
	}
	

	
	// Update is called once per frame
	protected override void Update() 
	{	
		//transform.position += transform.forward * fSpeed * Time.deltaTime;
		GetComponent<CharacterController>().Move(transform.forward * fSpeed * Time.deltaTime);
		transform.LookAt(goPlayer.transform);
	}
	
	protected void Pathfinding()
	{
		transform.LookAt(goPlayer.transform);
	}
	
	protected void Hit(Bullet.HitData _HitData)
	{
		nHealth -= _HitData.nDmg;
		
		if(nHealth <= 0)
		{
			Player.nAdrenaline += 5;
			
			if(nUnitType == 6)
			{
				Debug.Log("Boom");
				Collider[] hitCol = Physics.OverlapSphere(transform.position, 10);
				for(int i = 0; i < hitCol.Length; i++)
				{
					Unit cUnit = hitCol[i].collider.GetComponent<Unit>();
					if(cUnit != null)
					{
						hitCol[i].transform.position += (new Vector3(hitCol[i].transform.position.x - transform.position.x, 0, hitCol[i].transform.position.z - transform.position.z)).normalized * 10.0f;
						if(cUnit.nUnitType < 4)
							cUnit.SendMessage("Hit", nDmg);
					}
				}	
			}
			int nRandom = Random.Range(0,100);
			if( nRandom < 10)
			{
				Instantiate(prefabHealth,transform.position, transform.rotation);	
			}
			else if(nRandom < 50)
			{
				Instantiate(prefabToken,transform.position, transform.rotation);	
			}
			
			switch(nUnitType)
			{
			case 4:
				Player.nScore += 5;
				break;
			case 5:
				Player.nScore += 10;
				break;
			case 6:
				Player.nScore += 15;
				break;
			case 8:
				Player.nScore += 20;
				break;
			}
			StartCoroutine(Sink());
			//Destroy(gameObject);
		}
		transform.position += (transform.position - _HitData.Owner.transform.position).normalized * _HitData.fPush;
		Debug.Log("Hit for " + _HitData.nDmg + " HP Left: " +nHealth);	
	}
	IEnumerator Sink()
	{
		while(transform.position.y > -5)
		{
			transform.position -= new Vector3(0, 20 * Time.deltaTime, 0);
			yield return null;
		}
		GameObject.Destroy(gameObject);
	}

	
	protected IEnumerator CR_Attack()
	{
		while(true)
		{
			fAttackTimer -= Time.deltaTime;
			if(fAttackTimer <= 0)
			{	
//				Vector3 adjustedPos = new Vector3(transform.position.x, 1, transform.position.z);
//				Ray atk = new Ray(adjustedPos, transform.forward);
//				RaycastHit hit;
//				if(Physics.Raycast(atk, out hit, fRange))
//				{
//					//Debug.Log(hit.collider.gameObject.name);
//					fAttackTimer = fROF;
//					Unit cUnit = hit.collider.GetComponent<Unit>();
//					if(cUnit != null && cUnit.nUnitType < 4)
//						cUnit.SendMessage("Hit", nDmg);
//				}
				RaycastHit hit;
				if(Physics.SphereCast(transform.position + transform.forward * fRange * .5f, 0,transform.forward,  out hit, fRange * .5f))
				{
					//Debug.Log(hit.collider.gameObject.name);
					fAttackTimer = fROF;
					Unit cUnit = hit.collider.GetComponent<Unit>();
					if(cUnit != null && cUnit.nUnitType < 4)
						cUnit.SendMessage("Hit", nDmg);
				}
			}
			yield return 0;
		}
	}

	
#if UNITY_EDITOR
	protected void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position + transform.forward * fRange, fRange * .5f);
	//	Gizmos.DrawWireSphere(transform.position, fRange);
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * fRange);
	}
#endif
}


