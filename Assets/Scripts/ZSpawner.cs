using UnityEngine;
using System.Collections;

public class ZSpawner : MonoBehaviour {
	
	public float fSpawnRate = 5.0f;
	float fSpawnTimer = 0.0f;
	
	
	public float fNormal = 0, fFast = 0, fBoomer = 0, fTank = 0;
	
	float fNChance, fFChance, fBChance, fTChance;
	public GameObject prefabZombie, prefabFast, prefabBoomer, prefabTank;
	// Use this for initialization
	void Start () 
	{
		fNChance = fNormal;
		fFChance = fNChance + fFast;
		fBChance = fFChance + fBoomer;
		fTChance = fBChance + fTank;
			
		Debug.Log(fNChance + " " + fFChance + " " + fBChance + " " + fTChance);
	}
	
	// Update is called once per frame
	void Update () 
	{
		fSpawnTimer -= Time.deltaTime;
		if(fSpawnTimer <= 0.0f)
		{
			fSpawnTimer = fSpawnRate;
			int nRandom = Random.Range(0, 101);
			
			if(nRandom < fNChance)
			{
				GameObject.Instantiate(prefabZombie, transform.position, transform.rotation);
			}
			else if(nRandom < fFChance)
			{
				GameObject.Instantiate(prefabFast, transform.position, transform.rotation);
			}
			else if(nRandom < fBChance)
			{
				GameObject.Instantiate(prefabBoomer, transform.position, transform.rotation);
			}
			else
				GameObject.Instantiate(prefabTank, transform.position, transform.rotation);
				
		}
	}
}
