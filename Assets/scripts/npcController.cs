using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class npcController : MonoBehaviour
{
	#region variables
	public int npcID;
	public NPCType type;
	public string npcName;
	public Text charaNameText;
	public Sprite charaSprite;
	public bool alwaysAvail;
	public bool movesAtNight;
	public Vector3 mainLocation;
	public Vector3 secondaryLocation;
	public bool caresIfJackass;
	public float jackassRating;
	public bool isTalkingToPlayer;
	public bool resets;
	public bool loops;
	public bool talksInBackground;
	public int backgroundDialogueIndex;
	public float backgroundDialogueTimer;
	public float backgroundDialogueDelay;
	public bool dialogueRandom;
	public int currentDialogue = 0;
	public GameObject npcCanvas;
	public GameObject dialogueDisplay;
	public Text dialogueText;
	public string[] backgroundDialogue;
	public string[] dialogue;
	public string[] shopDialogue;
	public int numShopItemsAvail;
	public GameObject[] shopItems;
	public int numQuests;
	public GameObject[] quests;
	public int currentQuest;
	public bool questActive;

	public shopMenuController shopMenu;
	#endregion

	// Use this for initialization
	void Start()
	{
		dialogueDisplay.SetActive(false);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void backgroundTalk()
	{
		
	}

	public void talk()
	{
//		Debug.Log("currentDialogue = " + currentDialogue);
		dialogueDisplay.SetActive(true);
		dialogueDisplay.transform.rotation = GameObject.Find("Camera").transform.rotation;

		if (!dialogueRandom)
		{
			dialogueText.text = dialogue[currentDialogue];
			currentDialogue++;
			if (currentDialogue >= dialogue.Length)
			{
				if (loops)
				{
					currentDialogue = 0;
				}
				else
				{
					currentDialogue = dialogue.Length - 1;
				}
			}
		}
		else
		{
			currentDialogue = Random.Range(0, dialogue.Length);
			dialogueText.text = dialogue[currentDialogue];
		}
	}

	public void openShop()
	{
		dialogueDisplay.SetActive(true);
		dialogueDisplay.transform.rotation = GameObject.Find("Camera").transform.rotation;
		shopMenu.shopItems = shopItems;
		shopMenu.numItemsAvail = numShopItemsAvail;
		dialogueText.text = dialogue[currentDialogue];
		currentDialogue++;
	}

	public void closeShop()
	{
		dialogueText.text = dialogue[currentDialogue];
		currentDialogue = 0;
	}

	public void talkAboutQuest()
	{
		
	}

	public void silence()
	{
		dialogueText.text = "";
		dialogueDisplay.SetActive(false);

		if (resets)
		{
			currentDialogue = 0;
		}
	}
}