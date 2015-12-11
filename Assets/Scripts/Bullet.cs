using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	float fRange = 0.0f;
	float fSpeed = 80.0f;
	float fPushback = 0.0f;
	int nDamage = 5;
	
	float fCurDist = 0;
	GameObject goOwner;
	public struct HitData
	{
		public int nDmg;
		public float fPush;
		public GameObject Owner;
		public HitData(int _Dmg, float _Push, GameObject _Owner)
		{
			nDmg = _Dmg;
			fPush = _Push;
			Owner = _Owner;
		}
		                                   
	}
	public void Init(int _Damage, float _Range, float _PushBack, GameObject _Owner) 
	{
		fRange = _Range;
		nDamage = _Damage;
		fPushback = _PushBack;
		goOwner = _Owner;
		StartCoroutine(DestroySelf(3.0f));
		
		//Fixes bug where move back, hit parent
		if(goOwner.transform.parent.collider == null)
			Physics.IgnoreCollision(collider, goOwner.transform.parent.parent.collider);
		else
			Physics.IgnoreCollision(collider, goOwner.transform.parent.collider);
	}

	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * fSpeed * Time.deltaTime;

	}

	IEnumerator CR_CheckCollision()
	{
		Ray newRay;
		RaycastHit hit;
		while (true) {
			newRay = new Ray(transform.position, transform.forward);
			if(Physics.Raycast(newRay, out hit, fSpeed * Time.deltaTime))
			{
				if(hit.collider.name == "Bullet")
					continue;
				HitData newHit = new HitData(nDamage, fPushback, goOwner);
				//Debug.Log(hit.collider.gameObject.name);
				if(hit.collider.gameObject.GetComponent<Unit>() != null)
					hit.collider.gameObject.SendMessage("Hit", newHit);
				
			}
			yield return new WaitForSeconds(.35f);
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		print ("Bullet hit : " + collision.gameObject.name);
		if(collision.gameObject.name =="Bullet")
			return;
		
		HitData newHit = new HitData(nDamage, fPushback, goOwner);
		if(collision.gameObject.GetComponent<Unit>() != null)
			collision.gameObject.SendMessage("Hit", newHit);

		Destroy(gameObject);


	}
	
	IEnumerator DestroySelf(float _Time)
	{
		float fTimer = _Time;
		while(fTimer > 0)
		{
			fTimer-= Time.deltaTime;
			yield return null;	
		}
		Destroy(gameObject);
	}
	
}
