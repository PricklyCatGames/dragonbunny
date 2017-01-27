using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class battleController : MonoBehaviour
{
	#region variables
	public bool battleMode;
	public GameObject battleMenu;
	public Text charaNameText;
	public Slider HPSlider;
	public Slider MPSlider;
	public Text attackTimerText;
	public float attackTimer;
	public Text defendTimerText;
	public float defendTimer;
	public Button skillsButton;
	public GameObject skillsMenu;
	public GameObject skillsMenuContent;
	public GameObject skillPrefab;
	public Button itemsButton;
	public GameObject itemsMenu;
	public GameObject itemsMenuContent;
	public GameObject itemPrefab;
	public GameObject targetMenu;
	public GameObject targetMenuContent;
	public GameObject targetPrefab;
	public Text[] targetsText;
	public GameObject winScreen;
	public Text coinsGainedText;
	public Text EXPGainedText;
	public GameObject levelUpAnnouncement;
	public Text skillPointsGainedText;
	public GameObject dropPrefab;
	public GameObject takeAllButton;
	public GameObject continueButton;
	public GameObject loseScreen;
	public Transform playerCamera;
	public GameObject[] playerParty;
	public float[] partyActionTimers;
	public GameObject[] playerTargetIcons;
	public GameObject[] playerCanvases;
	public Text[] playerText;
	public List<GameObject> enemyParty = new List<GameObject>();
	public List<GameObject> enemyTargetIcons = new List<GameObject>();
	public List<GameObject> enemyCanvases = new List<GameObject>();
	public List<Text> enemyText = new List<Text>();
	public List<GameObject> targets = new List<GameObject>();
	public int selectedCharacter;
	public skillData[] charaSkills;
	public GameObject[] skillItems;
	Text[] skillText = new Text[2];
	public GameObject[] playerItems;
	Text[] itemText = new Text[2];
	public GameObject[] targetsList;
	public int numTargets;
	public int numItems;
	public int selectedSkill = -1;
	public int selectedItem = -1;
	public int selectedItemNum;
	public int selectedTarget = -1;
	public int numPlayerCharas;
	public int numEnemies;
	public List<int> questEnemiesKilled = new List<int>();
	public List<itemData> drops = new List<itemData>();
	public GameObject[] dropList;
	public GameObject dropListContent;
	public GameObject claimedListContent;
	public List<int> dropsClaimed = new List<int>();
	public int coinsWon;
	public int EXPEarned;
	public int skillPointsGained;

	public gameController gameController;
	public inventoryController inventoryController;
	public characterStatusController[] playerCharaStatuses;
	public List<characterStatusController> enemyStatuses = new List<characterStatusController>();
	public List<enemyController> enemyControllers = new List<enemyController>();
	itemBattleController itemBattleController;
	battleSkillController battleSkillController;
	battleTargetController battleTargetController;
	battleDropController battleDropController;
	#endregion

	// Use this for initialization
	void Start()
	{
		gameController = GameObject.Find("gameController").GetComponent<gameController>();
		playerCamera = GameObject.Find("Camera").transform;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (attackTimer > 0)
		{
			attackTimer -= Time.smoothDeltaTime;
			attackTimerText.text = attackTimer.ToString("F");
		}
		else if (attackTimer < 0)
		{
			attackTimer = 0;
			attackTimerText.text = attackTimer.ToString("F");
		}

		if (defendTimer > 0)
		{
			defendTimer -= Time.smoothDeltaTime;
			defendTimerText.text = defendTimer.ToString("F");
		}
		else if (defendTimer < 0)
		{
			defendTimer = 0;
			defendTimerText.text = defendTimer.ToString("F");
		}

		bool cancelButton = Input.GetButtonDown("Cancel");
		if (cancelButton)
		{
			if (targetMenu.activeInHierarchy)
			{
				selectedTarget = -1;
				selectedItem = -1;
				selectedSkill = -1;
				targetMenu.SetActive(false);
			}
			else if (skillsMenu.activeInHierarchy || 
					itemsMenu.activeInHierarchy)
			{
				skillsMenu.SetActive(false);
				itemsMenu.SetActive(false);
			}
		}

		if ((skillsMenu.activeInHierarchy && (selectedSkill > -1)) || 
			(itemsMenu.activeInHierarchy && (selectedItem > -1)))
		{
			openTargetList();
		}

		if (targetMenu.activeInHierarchy && (selectedTarget > -1) &&
			(selectedItem > -1))
		{
			useItem();
		}

		if (targetMenu.activeInHierarchy && (selectedTarget > -1) &&
			(selectedSkill > -1))
		{
			useSkill();
		}

		if (battleMode)
		{
			HPSlider.value = playerCharaStatuses[selectedCharacter].currentHP;
			MPSlider.value = playerCharaStatuses[selectedCharacter].currentMP;
		}
	}

	public void setupBattle(GameObject enemy)
	{
		playerTargetIcons = new GameObject[3];
		playerCanvases = new GameObject[3];
		playerText = new Text[3];
		playerItems = new GameObject[40];
		playerCharaStatuses = new characterStatusController[3];
		enemyParty.Clear();
		questEnemiesKilled.Clear();
		enemyCanvases.Clear();
		enemyTargetIcons.Clear();
		enemyStatuses.Clear();
		enemyControllers.Clear();
		dropsClaimed.Clear();
		targets.Clear();
		skillsButton.interactable = true;
		itemsButton.interactable = true;

		numPlayerCharas = gameController.numCharasInParty;
		playerParty = gameController.currentParty;
		for (int i = 0; i < numPlayerCharas; i++)
		{
			playerCharaStatuses[i] = playerParty[i].GetComponent<characterStatusController>();
			playerCanvases[i] = playerParty[i].transform.Find("Canvas").gameObject;
			playerText[i] = playerCanvases[i].transform.GetComponentInChildren<Text>();
			playerCanvases[i].transform.rotation = playerCamera.rotation;
			playerTargetIcons[i] = playerParty[i].transform.Find("targetCone").gameObject;
			targets.Add(playerParty[i]);
		}
		if (numPlayerCharas > 1)
		{
			positionPlayerParty();
		}
		loadCharaUI();
		loadSkills();
		loadItems();

		enemy.transform.LookAt(playerParty[selectedCharacter].transform);
		numEnemies = enemy.GetComponent<enemyController>().enemyParty.Length;
		enemyParty.Add(enemy);
		if (numEnemies > 1)
		{
			spawnEnemies(enemy);
		}
		for (int i = 0; i < numEnemies; i++)
		{
			targets.Add(enemyParty[i]);
			enemyStatuses.Add(enemyParty[i].GetComponent<characterStatusController>());
			enemyControllers.Add(enemyParty[i].GetComponent<enemyController>());
			enemyControllers[i].inBattle = true;
			enemyCanvases.Add(enemyParty[i].transform.Find("Canvas").gameObject);
//			enemyText.Add(enemyCanvases[i].transform.GetComponentInChildren<Text>());
			enemyCanvases[i].transform.rotation = playerCamera.rotation;
		}

		numTargets = targets.Count;
		targetsList = new GameObject[numTargets];
		loadTargets();

		battleMode = true;
		battleMenu.SetActive(true);

		drops.Clear();
		coinsWon = 0;
		EXPEarned = 0;
		skillPointsGained = 0;
	}

	public void positionPlayerParty()
	{
		
	}

	public void spawnEnemies(GameObject enemy)
	{
		GameObject monster = null;
		for (int i = 1; i < numEnemies; i++)
		{
			monster = Instantiate(enemy.GetComponent<enemyController>().enemyParty[i]);
			enemyParty.Add(monster);
		}
	}

	public void loadCharaUI()
	{
		HPSlider.maxValue = playerCharaStatuses[selectedCharacter].maxHP;
		HPSlider.value = playerCharaStatuses[selectedCharacter].currentHP;
		MPSlider.maxValue = playerCharaStatuses[selectedCharacter].maxMP;
		MPSlider.value = playerCharaStatuses[selectedCharacter].currentMP;

		attackTimer = playerCharaStatuses[selectedCharacter].skillTimers[0];
		attackTimerText.text = attackTimer.ToString("F");
		defendTimer = playerCharaStatuses[selectedCharacter].skillTimers[1];;
		defendTimerText.text = defendTimer.ToString("F");
	}

	public void loadSkills()
	{
//		if (charaSkills != null)
//		{
//			for (int i = charaSkills.Length; i > 0; i--)
//			{
//				Destroy(charaSkills[i]);
//			}
//		}

//		int numSkills = playerCharaStatuses[selectedCharacter].availSkills.Count;
//		charaSkills = new skillData[numSkills];
		charaSkills = playerCharaStatuses[selectedCharacter].availSkills.ToArray();
		skillItems = new GameObject[charaSkills.Length - 2];
		for (int i = 2; i < charaSkills.Length; i++)
		{
			skillItems[i - 2] = Instantiate(skillPrefab);
			skillItems[i - 2].transform.SetParent(skillsMenuContent.transform, false);
			skillItems[i - 2].name = charaSkills[i].skillName;
			battleSkillController = skillItems[i - 2].GetComponent<battleSkillController>();
			battleSkillController.battleController = this;
			battleSkillController.skillID = charaSkills[i].skillID;
			battleSkillController.skillIndex = i - 2;
			battleSkillController.skillName = charaSkills[i].skillName;
			battleSkillController.MPCost = charaSkills[i].MPCost;
			if (battleSkillController.MPCost > playerCharaStatuses[selectedCharacter].currentMP)
			{
				skillItems[i - 2].GetComponent<Button>().interactable = false;
			}
			battleSkillController.rechargeTime = charaSkills[i].rechargeTime;
			battleSkillController.castingTime = charaSkills[i].castingTime;
			battleSkillController.skillTimer = playerCharaStatuses[selectedCharacter].skillTimers[i];

			skillText = skillItems[i - 2].GetComponentsInChildren<Text>();
			skillText[0].text = charaSkills[i].skillName + " (" + charaSkills[i].MPCost + ")";
//			if (playerCharaStatuses[selectedCharacter].skillTimers.Count >= i)
//			{
//				skillText[1].text = playerCharaStatuses[selectedCharacter].skillTimers[i].ToString("F");
//			}
//			else
//			{
//				skillText[1].text = 0.ToString("F");
//			}
		}

		if (charaSkills.Length < 3)
		{
			skillsButton.interactable = false;
		}
	}

	public void loadItems()
	{
//		for (int i = numItems; i > 0; i--)
//		{
//			Destroy(playerItems[i]);
//			numItems--;
//		}
//		numItems = 0;
		for (int i = 0; i < inventoryController.numItemsPtInventory; i++)
		{
//			Debug.Log("item# " + i + ", name: " + inventoryController.partyInventory[i].name
//				+ ", type: " + inventoryController.partyInventory[i].type + ", use in battle: "
//				+ inventoryController.partyInventory[i].usableInBattle);
			if (inventoryController.partyInventory[i].type == ItemType.usable &&
				inventoryController.partyInventory[i].usableInBattle)
			{
//				Debug.Log("adding item# " + i + ", name: " + inventoryController.partyInventory[i].name);
				playerItems[numItems] = Instantiate(itemPrefab);
				playerItems[numItems].transform.SetParent(itemsMenuContent.transform, false);
				playerItems[numItems].name = inventoryController.partyInventory[i].itemName;
//				playerItems[numItems].GetComponentInChildren<Text>().text = playerItems[numItems].name;
				itemBattleController = playerItems[numItems].GetComponent<itemBattleController>();
				itemBattleController.battleController = this;
				itemBattleController.itemID = inventoryController.partyInventory[i].itemID;
				itemBattleController.inventoryIndex = i;
				itemBattleController.itemNumber = numItems;
				itemBattleController.itemName = playerItems[numItems].name;
				itemBattleController.itemSprite = inventoryController.partyInventory[i].itemImage;
//				itemBattleController.itemImage.sprite = itemBattleController.itemSprite;
				itemBattleController.itemCount = inventoryController.partyInventory[i].currentAmount;
				itemBattleController.useTimer = inventoryController.partyInventory[i].useTimer;
//
				itemText = playerItems[numItems].GetComponentsInChildren<Text>();
				itemText[0].text = itemBattleController.itemName;
				if (itemBattleController.itemCount > 1)
				{
					itemText[1].text = itemBattleController.itemCount.ToString();
				}
				else
				{
					itemText[1].text = "";
				}
				numItems++;
			}
		}

		if (numItems == 0)
		{
			itemsButton.interactable = false;
		}
	}

	public void loadTargets()
	{
		for (int i = 0; i < targets.Count; i++)
		{
			targetsList[i] = Instantiate(targetPrefab) as GameObject;
			targetsList[i].transform.SetParent(targetMenuContent.transform, false);
			targetsList[i].name = targets[i].GetComponent<characterStatusController>().charaName;
			battleTargetController = targetsList[i].GetComponent<battleTargetController>();
			battleTargetController.battleController = this;
			battleTargetController.targetIndex = i;
			battleTargetController.targetName = targetsList[i].name;
			battleTargetController.targetCone = targets[i].transform.Find("targetCone").gameObject;

			targetsText = targetsList[i].GetComponentsInChildren<Text>();
			targetsText[0].text = targetsList[i].name;
		}
	}

	public void playerAttack()
	{
		if (!skillsMenu.activeInHierarchy && !itemsMenu.activeInHierarchy && 
			!targetMenu.activeInHierarchy && attackTimer <= 0)
		{
			if (numEnemies > 1)
			{
				selectedSkill = 0;
				openTargetList();
			}
			else
			{
				enemyStatuses[0].calculateDamage(charaSkills[0], 
					playerParty[selectedCharacter]);
				attackTimer = charaSkills[0].rechargeTime;
			}
		}
	}

	public void playerDefend()
	{
		if (!skillsMenu.activeInHierarchy && !itemsMenu.activeInHierarchy && 
			!targetMenu.activeInHierarchy && defendTimer <= 0)
		{
			playerCharaStatuses[selectedCharacter].useSkill(charaSkills[1]);
		}
	}

	public void openSkillsMenu()
	{
		if (!itemsMenu.activeInHierarchy && !targetMenu.activeInHierarchy)
		{
			skillsMenu.SetActive(true);

			for (int i = 2; i < charaSkills.Length; i++)
			{	
				if (charaSkills[i].MPCost > playerCharaStatuses[selectedCharacter].currentMP)
				{
					skillItems[i - 2].GetComponent<Button>().interactable = false;
				}
				else
				{
					skillItems[i - 2].GetComponent<Button>().interactable = true;
				}
			}
		}
	}

	public void openItemsMenu()
	{
		if (!skillsMenu.activeInHierarchy && !targetMenu.activeInHierarchy)
		{
//			loadItems();
			itemsMenu.SetActive(true);
		}
	}

	public void openTargetList()
	{
//		if (numTargets > 0)
//		{
//			for (int i = numTargets; i > 0; i--)
//			{
//				Destroy(targetsList[i]);
//				numTargets--;
//			}
//		}
//		loadTargets();
		targetMenu.SetActive(true);
	}

	public void useItem()
	{
//		GameObject[] playerItems
//		int itemIndex = selectedItem;
		itemData item = inventoryController.partyInventory[selectedItem].GetComponent<itemData>();
		GameObject target = targets[selectedTarget];
		characterStatusController targetStatus = target.GetComponent<characterStatusController>();
		inventoryController.useItem(selectedItem);
		itemBattleController itemController = playerItems[selectedItemNum].GetComponent<itemBattleController>();
		itemController.itemCount--;

		if (itemController.itemCount > 0)
		{
			itemController.itemCountText.text = itemController.itemCount.ToString();
		}
		else
		{
			foreach (GameObject playerItem in playerItems)
			{
				Destroy(playerItem);
			}
			numItems = 0;
			loadItems();
		}

		targetStatus.calculateDamage(item);

		targetMenu.SetActive(false);
		itemsMenu.SetActive(false);
		selectedItem = -1;
		selectedTarget = -1;
	}

	public void useSkill()
	{
		skillData skill = charaSkills[selectedSkill + 2];
		GameObject target = targets[selectedTarget];
		characterStatusController targetStatus = target.GetComponent<characterStatusController>();
		battleSkillController skillController = 
		skillItems[selectedSkill].GetComponent<battleSkillController>();
		skillController.skillTimer = skillController.rechargeTime + skillController.castingTime;
		playerCharaStatuses[selectedCharacter].currentMP -= skill.MPCost;

		targetStatus.calculateDamage(skill, playerParty[selectedCharacter]);

		targetMenu.SetActive(false);
		skillsMenu.SetActive(false);
		selectedSkill = -1;
		selectedTarget = -1;
	}

	public void selectTarget()
	{
		
	}

	public void killCharacter(GameObject chara)
	{
		bool found = false;
//		Debug.Log("killed " + chara);

		for (int i = 0; i < numEnemies; i++)
		{
			if (chara == enemyParty[i])
			{
				killEnemy(i);
				found = true;
				break;
			}
		}

		if (!found)
		{
			for (int i = 0; i < numPlayerCharas; i++)
			{
				if (chara == playerParty[i])
				{
					killPlayerChara(i);
				}
			}
		}
	}

	public void killEnemy(int enemyIndex)
	{
		enemyController enemy = enemyParty[enemyIndex].GetComponent<enemyController>();
		coinsWon += enemy.coinsDropped;
		EXPEarned += enemy.EXPValue;
		skillPointsGained += enemy.skillPointValue;
		determineDrops(enemy);

		foreach (GameObject target in targetsList)
		{
			Destroy(target);
		}
		numTargets = 0;
		targets.Remove(enemy.gameObject);
		Destroy(enemyParty[enemyIndex]);
		enemyParty.RemoveAt(enemyIndex);
		numEnemies--;

		if (numEnemies == 0)
		{
			endBattle();
		}

		loadTargets();
	}

	public void killPlayerChara(int playerCharaIndex)
	{
		numPlayerCharas--;

		if (numPlayerCharas == 0)
		{
			loseBattle();
		}
	}

	public void revivePlayerChara(int playerCharaIndex)
	{
		numPlayerCharas++;
	}

	public void determineDrops(enemyController enemy)
	{
		for (int i = 0; i < enemy.drops.Length; i++)
		{
			float random = Random.Range(0, 100);
			if (random <= enemy.dropRates[i])
			{
				drops.Add(enemy.drops[i]);
			}
		}
	}

	public void endBattle()
	{
		if (!gameController.paused)
		{
			gameController.PauseGame();
		}
		awardEXP();
		awardSkillPoints();
		listDrops();

		for (int i = 0; i < numPlayerCharas; i++)
		{
			playerTargetIcons[i].SetActive(false);
		}

		if (numPlayerCharas > 1)
		{
			for (int i = 0; i < numPlayerCharas; i++)
			{
				if (gameController.currentParty[i] != gameController.currentCharacter)
				{
					gameController.currentParty[i].SetActive(false);
				}
			}
		}

		targetMenu.SetActive(false);
		skillsMenu.SetActive(false);
		itemsMenu.SetActive(false);
		battleMenu.SetActive(false);
		winScreen.SetActive(true);

		foreach (GameObject playerItem in playerItems)
		{
			Destroy(playerItem);
		}
		numItems = 0;

		foreach (GameObject target in targetsList)
		{
			Destroy(target);
		}
		numTargets = 0;

		foreach (GameObject skill in skillItems)
		{
			Destroy(skill);
		}

//		foreach (GameObject player in playerParty)
//		{
//			Destroy(player);
//		}
	}

	public void awardEXP()
	{
		EXPGainedText.text = "EXP gained: " + EXPEarned;
		for (int i = 0; i < numPlayerCharas; i++)
		{
			playerCharaStatuses[i].currentExp += EXPEarned;
			if (playerCharaStatuses[i].currentExp > playerCharaStatuses[i].expToNextLvl)
			{
				playerCharaStatuses[i].level++;
				playerCharaStatuses[i].currentExp -= playerCharaStatuses[i].expToNextLvl;
				playerCharaStatuses[i].levelUp();
				levelUpAnnouncement.SetActive(true);
			}
		}
	}

	public void awardSkillPoints()
	{
		skillPointsGainedText.text = "Skill points: " + skillPointsGained;
		for (int i = 0; i < numPlayerCharas; i++)
		{
			playerCharaStatuses[i].skillPoints += skillPointsGained;
			if (playerCharaStatuses[i].skillPoints > playerCharaStatuses[i].maxSkillPoints)
			{
				playerCharaStatuses[i].skillPoints = playerCharaStatuses[i].maxSkillPoints;
			}
		}
	}

	public void listDrops()
	{
		coinsGainedText.text = "Coins: " + coinsWon;
		dropList = new GameObject[drops.Count];
		for (int i = 0; i < drops.Count; i++)
		{
			dropList[i] = Instantiate(dropPrefab);
			dropList[i].transform.SetParent(dropListContent.transform, false);
			battleDropController = dropList[i].GetComponent<battleDropController>();
			battleDropController.dropItemName = drops[i].itemName;
			battleDropController.dropItemID = drops[i].itemID;
			battleDropController.dropItemIndex = i;
			battleDropController.dropItemNameText.text = drops[i].itemName;

		}
	}

	public void collectDrops()
	{
		foreach (GameObject item in dropList)
		{
			battleDropController = item.GetComponent<battleDropController>();
			if (battleDropController.isSelected)
			{
				inventoryController.addItem(battleDropController.dropItemID);
			}
		}
		inventoryController.coins += coinsWon;
		inventoryController.coinsText.text = "Coins: " + inventoryController.coins;

		battleMode = false;
		levelUpAnnouncement.SetActive(false);
		winScreen.SetActive(false);
		gameController.endBattle();
	}

	public void collectAllDrops()
	{
		foreach (itemData item in drops)
		{
			inventoryController.addItem(item.itemID);
		}
		inventoryController.coins += coinsWon;
		inventoryController.coinsText.text = "Coins: " + inventoryController.coins;

		battleMode = false;
		levelUpAnnouncement.SetActive(false);
		winScreen.SetActive(false);
		gameController.endBattle();
	}

	public void loseBattle()
	{
		targetMenu.SetActive(false);
		skillsMenu.SetActive(false);
		itemsMenu.SetActive(false);
		battleMenu.SetActive(false);
		loseScreen.SetActive(true);
		gameController.loseBattle();
	}
}