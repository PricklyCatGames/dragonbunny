using UnityEngine;
using System.Collections;

public class questData : MonoBehaviour
{
	#region variables
	public int questID;
	public string questName;
	public GameObject questNPC;
	public Vector3 questLocation;
	public GameObject targetNPC;
	public int targetID;
	public Vector3 targetLocation;
	public questType type;
	public bool isOptional;
	public bool canDelay;
	public bool timeSensitive;
	public int cutoffDay;
	public int cutoffMonth;
	public int cutoffYear;
	public bool eventSensitive;
	public int eventID;
	public bool repeatable;
	public int numberRepeats;
	public bool canOverAchieve;
	public bool overAchieveIncreasesPerItem;
	public bool givesTargetLocation;
	public string questDescription;
	public string[] questDialogue;
	public string questAcceptDialogue;
	public string questDeclineDialogue;
	public string[] questCompleteDialogue;
	public string[] extraQuestDialogue;

	public int numberRequired;
	public int maxOverAchieveAmt;
	public int rewardItemID;
	public int extraRewardItemID;
	public int rewardItemQuantity;
	public int minExtraRewardItemQuantity;
	public int maxExtraRewardItemQuantity;
	public int moneyReward;
	public int minExtraMoneyReward;
	public int maxExtraMoneyReward;
	public int EXPReward;
	public int minExtraEXP;
	public int maxExtraEXP;
	#endregion

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}