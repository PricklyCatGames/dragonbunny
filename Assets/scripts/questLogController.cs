using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class questLogController : MonoBehaviour
{
	#region variables
	public Text questTitleText;
	public Text questNPC;
	public Text questDescriptionText;
	public List<GameObject> questList;
	public GameObject questListPrefab;

	public string questTitle;
	public string npcName;
	public string questDescription;
	public bool isComplete;
	public int progress;
	public int rewardItemID;
	public int moneyReward;
	public int EXPReward;
//	public 

//	questManager questManager;
	#endregion

	// Use this for initialization
	void Start()
	{
//		questManager = GameObject.Find("gameController").GetComponent<questManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void displayQuest()
	{
		questTitleText.text = questTitle;
		questNPC.text = npcName;
		questDescriptionText.text = questDescription;
	}
}