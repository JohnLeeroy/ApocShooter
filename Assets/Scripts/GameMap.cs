using UnityEngine;
using System.Collections;

public class GameMap : MonoBehaviour {

	
	int HEIGHT = 25, WIDTH = 25;
	int fOffset = 5;
	
	BoxTile[] lTiles;
	public GameObject prefabBoxTile;
	// Use this for initialization
	void Start () 
	{
		GameObject goMap = GameObject.Find("Map");
		lTiles = new BoxTile[HEIGHT * WIDTH];
		
		for(int i = 0; i < HEIGHT; i++)
		{
			for(int j = 0; j < WIDTH; j++)
			{
				GameObject newTile = (GameObject)Instantiate(prefabBoxTile);
				newTile.name = "Tile_" + i + "_" +  j;
				newTile.transform.position = new Vector3(fOffset * j, 0, -fOffset * i);
				newTile.transform.parent = goMap.transform;
				lTiles[HEIGHT * i + j] = newTile.GetComponent<BoxTile>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}
	
	void CheckInput()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if(Physics.Raycast(screenRay, out hitInfo))
			{
				//hitInfo.collider.gameObject.GetComponent<BoxTile>().Unlock();
//				if(hitInfo.collider.gameObject.name == "KeyPopUp")
//				{
//					
//				}
			}
		}	
	}
}
