using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class questManager : MonoBehaviour
{
	#region variables
	List<GameObject> activeQuests = new List<GameObject>();
	public questLogController questLogController;
	public GameObject quest;
	questData questData;
	#endregion

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void acceptQuest()
	{
		questData = quest.GetComponent<questData>();
		activeQuests.Add(quest);
		// flag npc
		addQuestToLog();
	}

	public void addQuestToLog()
	{
		questLogController.questTitle = questData.questName;
		questLogController.npcName = questData.questNPC.ToString();
		questLogController.questDescription = questData.questDescription;
		questLogController.isComplete = false;
		questLogController.progress = 0;
		questLogController.rewardItemID = questData.rewardItemID;
		questLogController.moneyReward = questData.moneyReward;
		questLogController.EXPReward = questData.EXPReward;
	}

	public void updateProgress()
	{
		if (questData.numberRequired > questLogController.progress)
		{
			questLogController.progress++;
		}
		else
		{
			completeQuest();
		}
	}

	public void completeQuest()
	{
		questLogController.isComplete = true;
		if (questData.type == questType.story)
		{
			removeQuest();
		}
	}

	public void removeQuest()
	{
		activeQuests.Remove(quest);
	}
}