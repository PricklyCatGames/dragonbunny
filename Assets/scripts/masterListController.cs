using UnityEngine;
using System.Collections;

public class masterListController : MonoBehaviour
{
	#region variables
	public GameObject[] characterList;
	public GameObject[] itemList;
	public GameObject[] questList;
	public GameObject[] eventList;
	public GameObject[] enemyList;
	public GameObject[] skillList;
	#endregion

	// Use this for initialization
	void Start()
	{
//		Debug.Log("Charas:");
//		for (int i = 0; i < characterList.Length; i++)
//		{
//			Debug.Log(characterList[i].ToString());
//		}
//
//		Debug.Log("Items:");
//		for (int i = 0; i < itemList.Length; i++)
//		{
//			Debug.Log(itemList[i].ToString());
//		}
//
//		Debug.Log("Quests:");
//		for (int i = 0; i < questList.Length; i++)
//		{
//			Debug.Log(questList[i].ToString());
//		}
//
//		Debug.Log("Events:");
//		for (int i = 0; i < eventList.Length; i++)
//		{
//			Debug.Log(eventList[i].ToString());
//		}
//
//		Debug.Log("Enemies:");
//		for (int i = 0; i < enemyList.Length; i++)
//		{
//			Debug.Log(enemyList[i].ToString());
//		}
//
//		Debug.Log("Skills:");
//		for (int i = 0; i < skillList.Length; i++)
//		{
//			Debug.Log(skillList[i].ToString());
//		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public GameObject getChara(int charaID)
	{
		return characterList[charaID];
	}

	public GameObject getItem(int itemID)
	{
		return itemList[itemID];
	}

	public GameObject getQuest(int questID)
	{
		return questList[questID];
	}

	public GameObject getevent(int eventID)
	{
		return eventList[eventID];
	}

	public GameObject getEnemy(int enemyID)
	{
		return enemyList[enemyID];
	}

	public GameObject getSkill(int skillID)
	{
		return skillList[skillID];
	}
}