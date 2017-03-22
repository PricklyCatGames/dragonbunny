using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAllign : MonoBehaviour {

	public bool startFight;
	public bool alligned;
	public RectTransform p1;
	public RectTransform p2;
	public RectTransform p3;
	public RectTransform p4;
	public Camera mainCamera;
	public GameObject enemyUI;
	
	// Update is called once per frame
	void Update () 
	{
		if(!alligned && startFight)
		{
			alligned = true;
			/*
			var screenPos1 = mainCamera.WorldToScreenPoint (p1.transform.position);
			var ray1 = mainCamera.ScreenPointToRay(screenPos1);
 			Debug.DrawRay (ray1.origin, ray1.direction *  500, Color.green);

			RaycastHit hit1;
			if (Physics.Raycast(ray1, out hit1))
                if (hit1.collider != null)
					print(hit1.collider.name);
			*/
			var screenPos2 = mainCamera.WorldToScreenPoint (p2.transform.position);
			var ray2 = mainCamera.ScreenPointToRay(screenPos2);
			Debug.DrawRay (ray2.origin, ray2.direction *  500, Color.green);

			RaycastHit hit2;
			if (Physics.Raycast(ray2, out hit2))
                if (hit2.collider != null)
					//print(hit2.collider.name);
					print("Hit");
			/*
			var screenPos3 = mainCamera.WorldToScreenPoint (p3.transform.position);
			var ray3 = mainCamera.ScreenPointToRay(screenPos3);
			Debug.DrawRay (ray3.origin, ray3.direction *  500, Color.green);

			RaycastHit hit3;
			if (Physics.Raycast(ray3, out hit3))
                if (hit3.collider != null)
					print(hit3.collider.name);
			*/
			var screenPos4 = mainCamera.WorldToScreenPoint (p4.transform.position);
			var ray4 = mainCamera.ScreenPointToRay(screenPos4);
			Debug.DrawRay (ray4.origin, ray4.direction *  500, Color.green);

			RaycastHit hit4;
			if (Physics.Raycast(ray4, out hit4))
                if (hit4.collider != null)
                    //print(hit4.collider.name);
					print("Hit");


			var enemyRect = enemyUI.GetComponent<RectTransform>();
           	if(hit2.collider.tag == "Player")
           	{
           		alligned = false;
				enemyRect.position = new Vector3(enemyRect.position.x - 20f, enemyRect.position.y, enemyRect.position.z);
				print(hit4.collider.name);
           	}
			if(hit4.collider.tag == "Player")
           	{
				alligned = false;
				enemyRect.position = new Vector3(enemyRect.position.x + 20f, enemyRect.position.y, enemyRect.position.z);
				print(hit4.collider.name);
           	}

 		}
	}
}
