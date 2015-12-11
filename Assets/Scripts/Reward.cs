using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour {
	
	public static GameObject goPlayer;
	bool bAnimate = true;
	bool bUp = true;
	
	public int rewardType = 0; //0 token, 1 health
	public int rewardAmount = 5;
	// Use this for initialization
	void Start () 
	{
		if (goPlayer == null)
			goPlayer = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine(Animate());	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Player")
		{
			bAnimate = false;
			if(rewardType == 0)
				goPlayer.GetComponent<Player>().AddToken(rewardAmount);
			else
				goPlayer.GetComponent<Player>().HealthReward();
				
			Destroy(gameObject);
			//StartCoroutine(GoToPlayer());	
		}
	}
	IEnumerator Animate()
	{
		while(bAnimate)
		{
			transform.Rotate(Vector3.up, 250 * Time.deltaTime);
			if(bUp)
			{
				transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
				if(transform.position.y > 1.5f)
				{
					bUp = false;	
				}
			}
			else
			{
				transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);
				if(transform.position.y < -1)
				{
					bUp = true;	
				}
			}
			
			yield return null;
		}
	}
	IEnumerator GoToPlayer()
	{
		while(transform.position != goPlayer.transform.position)
		{
			transform.position = Vector3.Lerp(transform.position, goPlayer.transform.position, 20 * Time.deltaTime);	
			yield return null;
		}
	}
}
