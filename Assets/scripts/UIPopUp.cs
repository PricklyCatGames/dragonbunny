using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : MonoBehaviour {

	public GameObject popupObject;
	public bool displaying;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) && displaying)
		{
			displaying = false;
			HideUIPopUp();
		}
	}

	public void DisplayUIPopUp()
	{
		popupObject.GetComponent<CanvasGroup>().alpha = 1;
		popupObject.GetComponent<CanvasGroup>().interactable = true;
		popupObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

		displaying = true;
	}

	public void HideUIPopUp()
	{
		popupObject.GetComponent<CanvasGroup>().alpha = 0;
		popupObject.GetComponent<CanvasGroup>().interactable = false;
		popupObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

		displaying = false;
	}
}
