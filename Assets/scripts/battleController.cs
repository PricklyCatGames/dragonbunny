using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class battleController : MonoBehaviour
{
	#region variables
	public bool battleMode;
	public GameObject battleMenu;
	public Transform enemyContainer;
//	int gridWidth = 7;
//	int gridHeight = 6;
	public GameObject battleGridMain;
	public Transform[] battleGrid = new Transform[42];
//	public Vector2[][] battleGridPos = new Vector2[7][];
	public bool[] battleGridOccupancy = new bool[42];
	public GameObject increaseCharaButton;
	public GameObject decreaseCharaButton;
	public Text charaNameText;
	public Slider HPSlider;
	public Slider MPSlider;
	public Text defaultTimerText;
	public float defaultTimer;
	public Text attackTimerText;
	public float attackTimer;
	public Text defendTimerText;
	public float defendTimer;
	public Text fleeTimerText;
	public float fleeTimer;
	public Button magicButton;
	public GameObject magicMenu;
	public GameObject magicMenuContent;
	public GameObject magicSubmenu;
	public GameObject magicSubmenuContent;
	public GameObject fireSubmenu;
	public GameObject fireSubmenuContent;
	public GameObject lightningSubmenu;
	public GameObject lightningSubmenuContent;
	public GameObject windSubmenu;
	public GameObject windSubmenuContent;
	public GameObject iceSubmenu;
	public GameObject iceSubmenuContent;
	public GameObject lightSubmenu;
	public GameObject lightSubmenuContent;
	public GameObject waterSubmenu;
	public GameObject waterSubmenuContent;
	public GameObject earthSubmenu;
	public GameObject earthSubmenuContent;
	public GameObject darkSubmenu;
	public GameObject darkSubmenuContent;
//	public float[][] skillTimers = new float[3][];
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
	public GameObject[] originalEnemyParty;
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
	public GameObject[] attackTargetsList;
	public int numStandardSkills = 3;
	public int numTargets;
	public int numItems;
	public int selectedSkill = -1;
	public int selectedItem = -1;
	public int selectedItemNum;
	public int selectedTarget = -1;
	public int numPlayerCharas;
	public int numAIs;
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

		if (fleeTimer > 0)
		{
			fleeTimer -= Time.smoothDeltaTime;
			fleeTimerText.text = fleeTimer.ToString("F");
		}
		else if (fleeTimer < 0)
		{
			fleeTimer = 0;
			fleeTimerText.text = fleeTimer.ToString("F");
		}

//		for (int i = 0; i < numPlayerCharas; i++)
//		{
//			for (int j = 0; j < )
//			if (skillTimers[i] > 0)
//			{
//				skillTimers[i] -= Time.smoothDeltaTime;
//			}
//			else if (skillTimers[i] < 0)
//			{
//				skillTimers[i] = 0;
//			}
//		}

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

		if (skillsMenu.activeInHierarchy && (selectedSkill > -1))
		{
			battleSkillController skillController = 
				skillItems[selectedSkill].GetComponent<battleSkillController>();

			if (skillController.skillTimer <= 0)
			{
				openTargetList();
			}
		}

		if (itemsMenu.activeInHierarchy && (selectedItem > -1))//)
		{
			openTargetList();
		}

		if (targetMenu.activeInHierarchy && (selectedTarget > -1) &&
			(selectedItem > -1))
		{
			useItem();
		}

		if (targetMenu.activeInHierarchy && (selectedTarget > -1))
		{
			if (selectedSkill > -1)
			{
				useSkill();
			}
			else
			{
				attackTarget();
			}
		}

		if (battleMode)
		{
			HPSlider.value = playerCharaStatuses[selectedCharacter].currentHP;
			MPSlider.value = playerCharaStatuses[selectedCharacter].currentMP;
		}
	}

	public void setupBattle(GameObject enemy)
	{
//		clearLists();

		playerTargetIcons = new GameObject[3];
		playerCanvases = new GameObject[3];
		playerText = new Text[3];
		playerItems = new GameObject[40];
		playerCharaStatuses = new characterStatusController[3];

//		skillTimers.Clear();
		enemyParty.Clear();
		questEnemiesKilled.Clear();
		enemyCanvases.Clear();
		enemyTargetIcons.Clear();
		enemyStatuses.Clear();
		enemyControllers.Clear();
		drops.Clear();
		dropsClaimed.Clear();
		targets.Clear();
		coinsWon = 0;
		EXPEarned = 0;
		skillPointsGained = 0;
		skillsButton.interactable = true;
		itemsButton.interactable = true;

		numPlayerCharas = gameController.numCharasInParty;
		playerParty = gameController.currentParty;
		setBattleGrid();

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
			increaseCharaButton.SetActive(true);
			decreaseCharaButton.SetActive(true);
			positionPlayerParty();
		}
		loadCharaUI();
		loadSkills();
		loadItems();

		originalEnemyParty = enemy.GetComponent<enemyController>().enemyParty;
		enemy.transform.position = battleGrid[17].position;
		enemy.transform.rotation = battleGrid[17].rotation;
		battleGridOccupancy[17] = true;
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
	}

	public void setBattleGrid()
	{
		battleGridMain.transform.position = playerParty[0].transform.position;
		battleGridMain.transform.rotation = playerParty[0].transform.rotation;
		battleGridOccupancy[24] = true;
//		Vector2 charaStartPos = new Vector2(playerParty[0].transform.position.x, 
//			playerParty[0].transform.position.z);
//		Vector2 gridStartPos = new Vector2(charaStartPos.x - (gridWidth / 2), 
//			charaStartPos.y - (gridHeight / 2));
//		int counter = 0;
//
//		for (int i = 0; i < gridWidth; i++)
//		{
//			for (int j = 0; j < gridHeight; j++)
//			{
//				battleGridPos[i][j] = battleGrid[counter].position;
//				counter++;
//			}
//		}
	}

	public void positionPlayerParty()
	{
		for (int i = 1; i < numPlayerCharas; i++)
		{
			switch (i)
			{
				case 1:
				{
					playerParty[i].GetComponent<playerController>().enabled = true;
					playerParty[i].GetComponent<Collider>().enabled = true;
					playerParty[i].GetComponent<playerController>().isControllable = false;

					if (playerCharaStatuses[i].inFrontLine)
					{
						playerParty[i].transform.position = battleGrid[23].position;
						playerParty[i].transform.rotation = battleGrid[23].rotation;
						battleGridOccupancy[23] = true;
//								playerParty[0].transform.localPosition +
//							new Vector3(2, 0, 0);
					}
					else
					{
						playerParty[i].transform.position = battleGrid[30].position;
						playerParty[i].transform.rotation = battleGrid[30].rotation;
						battleGridOccupancy[30] = true;
//								playerParty[0].transform.localPosition +
//							new Vector3(2, 0, -2);
					}
					break;
				}
				case 2:
				{
					playerParty[i].GetComponent<playerController>().enabled = true;
					playerParty[i].GetComponent<Collider>().enabled = true;
					playerParty[i].GetComponent<playerController>().isControllable = false;

					if (playerCharaStatuses[i].inFrontLine)
					{
						playerParty[i].transform.position = battleGrid[25].position;
						playerParty[i].transform.rotation = battleGrid[25].rotation;
						battleGridOccupancy[25] = true;
//								playerParty[0].transform.localPosition +
//							new Vector3(0, -2, 0);
					}
					else
					{
						playerParty[i].transform.position = battleGrid[32].position;
						playerParty[i].transform.rotation = battleGrid[32].rotation;
						battleGridOccupancy[32] = true;
//								playerParty[0].transform.localPosition +
//							new Vector3(0, -2, -2);
					}
					break;
				}
			}
//			currentCharacter.transform.position = spawnPoint.position;
		}

		for (int i = 0; i < numAIs; i++)
		{
//			switch (i)
//			{
//				case 1:
//				{
//					if (playerCharaStatuses[i].inFrontLine)
//					{
//						playerParty[i].transform.position = battleGrid[23].position;
//								playerParty[0].transform.localPosition +
//							new Vector3(2, 0, 0);
//					}
//					else
//					{
//						playerParty[i].transform.position = battleGrid[30].position;
//								playerParty[0].transform.localPosition +
//							new Vector3(2, 0, -2);
//					}
//					break;
//				}
//				case 2:
//				{
//					if (playerCharaStatuses[i].inFrontLine)
//					{
//						playerParty[i].transform.position = battleGrid[25].position;
//								playerParty[0].transform.localPosition +
//							new Vector3(0, -2, 0);
//					}
//					else
//					{
//						playerParty[i].transform.position = battleGrid[32].position;
//								playerParty[0].transform.localPosition +
//							new Vector3(0, -2, -2);
//					}
//					break;
//				}
//			}
		}
	}

	public void spawnEnemies(GameObject enemy)
	{
		GameObject monster = null;
		for (int i = 1; i < numEnemies; i++)
		{
			monster = Instantiate(enemy.GetComponent<enemyController>().enemyParty[i]);
			enemyParty.Add(monster);
			monster.transform.SetParent(enemyContainer);

			switch (i)
			{
				case 1:
				{
					monster.transform.position = battleGrid[16].position;
					monster.transform.rotation = battleGrid[16].rotation;
					battleGridOccupancy[16] = true;
//							enemy.transform.localPosition +
//						new Vector3(2, 0, 0);
					break;
				}
				case 2:
				{
					monster.transform.position = battleGrid[18].position;
					monster.transform.rotation = battleGrid[18].rotation;
					battleGridOccupancy[18] = true;
//							enemy.transform.localPosition +
//						new Vector3(-2, 0, 0);
					break;
				}
				case 3:
				{
					monster.transform.position = battleGrid[10].position;
					monster.transform.rotation = battleGrid[10].rotation;
					battleGridOccupancy[10] = true;
//							enemy.transform.localPosition +
//						new Vector3(0, 0, -2);
					break;
				}
				case 4:
				{
					monster.transform.position = battleGrid[9].position;
					monster.transform.rotation = battleGrid[9].rotation;
					battleGridOccupancy[9] = true;
//							enemy.transform.localPosition +
//						new Vector3(2, 0, -2);
					break;
				}
				case 5:
				{
					monster.transform.position = battleGrid[11].position;
					monster.transform.rotation = battleGrid[11].rotation;
					battleGridOccupancy[11] = true;
//							enemy.transform.localPosition +
//						new Vector3(-2, 0, -2);
					break;
				}
//				case 6:
//				{
//					monster.transform.position = enemy.transform.localPosition +
//						new Vector3(0, 0, 0);
//					break;
//				}
			}
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
		defendTimer = playerCharaStatuses[selectedCharacter].skillTimers[1];
		defendTimerText.text = defendTimer.ToString("F");
		fleeTimer = playerCharaStatuses[selectedCharacter].skillTimers[2];
		fleeTimerText.text = fleeTimer.ToString("F");
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

		if (charaSkills.Length < numStandardSkills)
		{
			skillsButton.interactable = false;
		}
		else
		{
			skillItems = new GameObject[charaSkills.Length - numStandardSkills];
//			skillTimers[0] = charaSkills.Length - 2;
//			Debug.Log("loadSkills line 488, charaSkills length = " + 
//					charaSkills.Length);

			// all playerCharacters have attack, defend, flee as their 1st 3 skills
			for (int i = numStandardSkills; i < charaSkills.Length; i++)
			{
				skillItems[i - numStandardSkills] = Instantiate(skillPrefab);
				skillItems[i - numStandardSkills].transform.SetParent(skillsMenuContent.transform, false);
				skillItems[i - numStandardSkills].name = charaSkills[i].skillName;
				battleSkillController = skillItems[i - numStandardSkills].GetComponent<battleSkillController>();
				battleSkillController.battleController = this;
				battleSkillController.skillID = charaSkills[i].skillID;
				battleSkillController.skillIndex = i - numStandardSkills;
				battleSkillController.skillName = charaSkills[i].skillName;
				battleSkillController.MPCost = charaSkills[i].MPCost;
//				Debug.Log("loadSkills line 502, i = " + i);
				if (battleSkillController.MPCost > playerCharaStatuses[selectedCharacter].currentMP)
				{
					skillItems[i - numStandardSkills].GetComponent<Button>().interactable = false;
				}
				battleSkillController.rechargeTime = charaSkills[i].rechargeTime;
				battleSkillController.castingTime = charaSkills[i].castingTime;
				battleSkillController.skillTimer = playerCharaStatuses[selectedCharacter].skillTimers[i];
//				Debug.Log("loadSkills line 510, i = " + i);
				skillText = skillItems[i - numStandardSkills].GetComponentsInChildren<Text>();
				skillText[0].text = charaSkills[i].skillName + " (" + charaSkills[i].MPCost + ")";
//				if (playerCharaStatuses[selectedCharacter].skillTimers.Count >= i)
//				{
//					skillText[1].text = playerCharaStatuses[selectedCharacter].skillTimers[i].ToString("F");
//				}
//				else
//				{
//					skillText[1].text = 0.ToString("F");
//				}
			}
		}
	}

	public void loadItems()
	{
		if (numItems > 0)
		{
			for (int i = numItems - 1; i >= 0; i--)
			{
				Destroy(playerItems[i]);
				numItems--;
			}
			numItems = 0;
		}

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
//		clearTargetList();

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

		numTargets = targets.Count;
	}

	public void setDefaultAction()
	{
		
	}

	public void useDefaultAction()
	{
		
	}

	public void playerAttack()
	{
		if (!skillsMenu.activeInHierarchy && !itemsMenu.activeInHierarchy && 
			!targetMenu.activeInHierarchy && attackTimer <= 0)
		{
			if (numTargets > 2)
			{
//				selectedSkill = 0;
				openTargetList();
			}
			else
			{
				enemyStatuses[0].calculateDamage(charaSkills[0], 
					playerParty[selectedCharacter]);
				attackTimer = charaSkills[0].rechargeTime;
				defendTimer = charaSkills[0].rechargeTime;
				// TODO Hard coded for now
				// Play Attack Anim
				//var animPlayer = GameObject.Find("Ankalia v1(Clone)").GetComponent<Animator>();
				//animPlayer.Play("attack");
				print(playerController.isAttacking);
				playerController.isAttacking = true;
				StartCoroutine(AttackCoolDown());
				print(playerController.isAttacking);
			}
		}
	}

	IEnumerator AttackCoolDown()
	{
		yield return new WaitForSeconds(2.0f);
		playerController.isAttacking = false;
		print(playerController.isAttacking);
	}

	public void playerDefend()
	{
		if (!skillsMenu.activeInHierarchy && !itemsMenu.activeInHierarchy && 
			!targetMenu.activeInHierarchy && defendTimer <= 0)
		{
			playerCharaStatuses[selectedCharacter].useSkill(charaSkills[1]);
			attackTimer = charaSkills[1].rechargeTime;
			defendTimer = charaSkills[1].rechargeTime;
		}
	}

	public void flee()
	{
		if (!skillsMenu.activeInHierarchy && !itemsMenu.activeInHierarchy && 
			!targetMenu.activeInHierarchy && fleeTimer <= 0)
		{
			int fleeSuccess = 0;
			fleeSuccess = Random.Range(1, 101);
			int fleeRate = 
				playerCharaStatuses[selectedCharacter].availSkills[2].successRate;

			if (fleeSuccess > fleeRate)
			{
				fleeBattle();
			}
			else
			{
				fleeTimer = charaSkills[2].rechargeTime;
			}
		}
	}

	public void openSkillsMenu()
	{
		if (!itemsMenu.activeInHierarchy && !targetMenu.activeInHierarchy)
		{
			skillsMenu.SetActive(true);

			for (int i = numStandardSkills; i < charaSkills.Length; i++)
			{	
				if (charaSkills[i].MPCost > playerCharaStatuses[selectedCharacter].currentMP)
				{
					skillItems[i - numStandardSkills].GetComponent<Button>().interactable = false;
				}
				else
				{
					skillItems[i - numStandardSkills].GetComponent<Button>().interactable = true;
					battleSkillController = skillItems[i - 
						numStandardSkills].GetComponent<battleSkillController>();
					battleSkillController.skillTimer = 
						playerCharaStatuses[selectedCharacter].skillTimers[i];
					battleSkillController.skillTimerText.text = 
						battleSkillController.skillTimer.ToString("F");
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
		targetMenu.SetActive(true);
		clearTargetList();
		loadTargets();
	}

	public void attackTarget()
	{
		GameObject target = targets[selectedTarget];
		characterStatusController targetStatus = 
			target.GetComponent<characterStatusController>();

		targetStatus.calculateDamage(charaSkills[0], playerParty[selectedCharacter]);
		attackTimer = charaSkills[0].rechargeTime;
		targetMenu.SetActive(false);
		selectedTarget = -1;
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
		battleSkillController skillController = 
			skillItems[selectedSkill].GetComponent<battleSkillController>();

		if (skillController.skillTimer <= 0)
		{
//			if (selectedSkill > 2)
			{
				skillData skill = charaSkills[selectedSkill + numStandardSkills];
				GameObject target = targets[selectedTarget];
				characterStatusController targetStatus = target.GetComponent<characterStatusController>();
				skillController.skillTimer = skillController.rechargeTime + skillController.castingTime;
				playerCharaStatuses[selectedCharacter].currentMP -= skill.MPCost;
				playerCharaStatuses[selectedCharacter].skillTimers[selectedSkill + 
					numStandardSkills] = skillController.skillTimer;
				attackTimer = charaSkills[selectedSkill + numStandardSkills].castingTime;
				defendTimer = charaSkills[selectedSkill + numStandardSkills].castingTime;

				targetStatus.calculateDamage(skill, playerParty[selectedCharacter]);

				skillsMenu.SetActive(false);
			}
		}

		targetMenu.SetActive(false);
		selectedSkill = -1;
		selectedTarget = -1;
	}

	public void decreaseChara()
	{

	}

	public void increaseChara()
	{
		
	}

	public void switchChara(int chara)
	{
		
	}

//	public void selectTarget()
//	{
//		
//	}

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

		targets.Remove(enemy.gameObject);
		clearTargetList();
//		foreach (GameObject target in targetsList)
//		{
//			Destroy(target);
//		}
//		numTargets = 0;
		Destroy(enemyParty[enemyIndex]);
		enemyParty.RemoveAt(enemyIndex);
		enemyStatuses.RemoveAt(enemyIndex);
		enemyControllers.RemoveAt(enemyIndex);
		enemyCanvases.RemoveAt(enemyIndex);
		numEnemies--;

		if (numEnemies == 0)
		{
			endBattle();
		}
		else
		{
			loadTargets();
		}
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
//				Debug.Log("dropped: " + enemy.drops[i].ToString());
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
				playerParty[i].GetComponent<playerController>().enabled = false;
				playerParty[i].GetComponent<Collider>().enabled = false;
				playerParty[i].transform.position = Vector3.zero;
//				if (gameController.currentParty[i] != gameController.currentCharacter)
//				{
//					gameController.currentParty[i].SetActive(false);
//				}
			}
		}

		increaseCharaButton.SetActive(false);
		decreaseCharaButton.SetActive(false);
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

		playerController.isAttacking = false;
		playerController.isAttackIdle = false;
		
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
		clearDrops();
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
		clearDrops();
		levelUpAnnouncement.SetActive(false);
		winScreen.SetActive(false);
		gameController.endBattle();
	}

/*	public void clearLists()
	{
		int numChildren = 0;

		numChildren = skillsMenuContent.transform.childCount;
//		Debug.Log("numSkills: " + numChildren);
		if (numChildren > 0)
		{
			for (int i = numChildren - 1; i >= 0; i--)
			{
				Destroy(skillsMenuContent.transform.GetChild(i));
			}
		}

		numChildren = itemsMenuContent.transform.childCount;
//		Debug.Log("numItems: " + numChildren);
		if (numChildren > 0)
		{
			for (int i = numChildren - 1; i >= 0; i--)
			{
				Destroy(itemsMenuContent.transform.GetChild(i));
	//			playerItems[i];
			}
			numItems = 0;
		}

		numChildren = targetMenuContent.transform.childCount;
//		Debug.Log("numTargets: " + numChildren);
		if (numChildren > 0)
		{
			for (int i = numChildren - 1; i >= 0; i--)
			{
				Destroy(targetMenuContent.transform.GetChild(i));
			}
			numTargets = 0;
		}

//		numChildren = dropListContent.transform.childCount;
////		Debug.Log("numDrops: " + numChildren);
//		if (numChildren > 0)
//		{
//			for (int i = numChildren - 1; i >= 0; i--)
//			{
//				Destroy(dropListContent.transform.GetChild(i));
//			}
//		}
	}*/

	public void clearTargetList()
	{
		if (numTargets > 0)
		{
			for (int i = numTargets - 1; i >= 0; i--)
			{
				Destroy(targetsList[i]);
			}
			numTargets = 0;
		}
	}

	public void clearDrops()
	{
		for (int i = dropList.Length - 1; i >= 0; i--)
		{
			Destroy(dropList[i]);
		}
		drops.Clear();
	}

	public void fleeBattle()
	{
		for (int i = 1; i < numPlayerCharas; i++)
		{
			playerParty[i].GetComponent<playerController>().enabled = false;
			playerParty[i].GetComponent<Collider>().enabled = false;
			playerParty[i].transform.position = Vector3.zero;
		}

		for (int i = 0; i < numAIs; i++)
		{
//			playerParty[i].GetComponent<playerController>().enabled = false;
//			playerParty[i].GetComponent<Collider>().enabled = false;
//			playerParty[i].transform.position = Vector3.zero;
		}

		bool originalEnemyReset = false;
		for (int i = 0; i < numEnemies; i++)
		{
			if (enemyParty[i] != originalEnemyParty[0])
			{
				Destroy(enemyParty[i]);
			}
			else
			{
				enemyStatuses[i].currentHP = enemyStatuses[i].maxHP;
				originalEnemyReset = true;
			}
		}
		if (!originalEnemyReset)
		{
			GameObject monster = 
				Instantiate(originalEnemyParty[0].GetComponent<enemyController>().enemyParty[0]);
			monster.transform.SetParent(enemyContainer);
			monster.transform.position = battleGrid[17].position;
		}

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

		increaseCharaButton.SetActive(false);
		decreaseCharaButton.SetActive(false);
		targetMenu.SetActive(false);
		skillsMenu.SetActive(false);
		itemsMenu.SetActive(false);
		battleMenu.SetActive(false);
		gameController.endBattle();
	}

	public void loseBattle()
	{
//		clearLists();
		increaseCharaButton.SetActive(false);
		decreaseCharaButton.SetActive(false);
		targetMenu.SetActive(false);
		skillsMenu.SetActive(false);
		itemsMenu.SetActive(false);
		battleMenu.SetActive(false);
		loseScreen.SetActive(true);
		gameController.loseBattle();
	}
}