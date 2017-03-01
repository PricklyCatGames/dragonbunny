using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class characterMenuController : MonoBehaviour
{
	#region variables
	public GameObject statsPanel;
	public GameObject equipList;
	public GameObject equipListContent;
	public GameObject equipListPrefab;
	public GameObject skillChart;
	public GameObject skillList;
	public GameObject skillListContent;
	public GameObject skillListPrefab;

	public int numCharas;
	public int selectedChara;
	public Characters currentChara;
	public GameObject charaPreview;
	public GameObject[] charaPreviews;
	public Transform charaPreviewPos;
	public Text nameText;
	public Text levelText;
	public Text hpText;
	public Text mpText;
	public Text expText;
	public Text skillPointText;
	public Text strText;
	public Text intText;
	public Text dexText;
	public Text agiText;
	public Text endurText;
	public Text luckText;
	public Text clarityText;
	public Text zenText;
	public Text atkText;
	public Text defText;
	public Text magAtkText;
	public Text magDefText;
	public Text evaText;
	public Text bonusText;

	public Text equippedWeaponText;
	public Text equippedSubWpnText;
	public Text equippedHeadText;
	public Text equippedChestText;
	public Text equippedHandsText;
	public Text equippedLegsText;
	public Text equippedFeetText;
	public Text equippedAccessoryText;

	public Color unequippedColor;
	public Color equippedColor;

	public int selectedEquipSlot;
	public int selectedEquipItem;
	public EquipmentSlot EquipSlot;

	public GameObject[] equipmentList; // = new GameObject[40];
	public int numEquipItems;

	gameController gameController;
	characterStatusController charaStatus;
	equipManager charaEquipManager;
	inventoryController inventoryController;
	equipItemController equipItemController;
	#endregion

	// Use this for initialization
	void Start()
	{
		equipmentList = new GameObject[40];
		selectedEquipSlot = -1;
		selectedEquipItem = -1;
		gameController = GameObject.Find("gameController").GetComponent<gameController>();
		inventoryController = GameObject.Find("gameController").GetComponent<inventoryController>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void openStatsPanel()
	{
		reset();
		statsPanel.SetActive(true);

		createPreview();
	}

	public void createPreview()
	{
		charaPreview = Instantiate(charaPreviews[selectedChara], charaPreviewPos.position, charaPreviewPos.rotation) as GameObject;
		charaPreview.GetComponent<playerController>().enabled = false;
		charaPreview.GetComponent<Collider>().enabled = false;
		charaPreview.transform.parent = transform;
		if (!equipList.activeInHierarchy)
		{
			charaPreview.layer = 5;
			for (int i = 0; i < charaPreview.transform.childCount; i++)
			{
				Transform child = charaPreview.transform.GetChild(i);
				child.gameObject.layer = 5;
			}
		}
		charaStatus = gameController.availCharas[selectedChara].GetComponent<characterStatusController>();

		nameText.text = charaStatus.charaName;
		levelText.text = "Level: " + charaStatus.level;
		hpText.text = "HP: " + charaStatus.currentHP + "/" + charaStatus.maxHP;
		mpText.text = "MP: " + charaStatus.currentMP + "/" + charaStatus.maxMP;
		expText.text = "Exp: " + charaStatus.currentExp + "/" + charaStatus.expToNextLvl;
		skillPointText.text = "Skill Points: " + charaStatus.skillPoints;
		strText.text = "Strength: " + charaStatus.strength;
		intText.text = "Intelligence: " + charaStatus.intelligence + "";
		dexText.text = "Dexterity: " + charaStatus.dexterity + "";
		agiText.text = "Agility: " + charaStatus.agility + "";
		endurText.text = "Endurance: " + charaStatus.endurance + "";
		luckText.text = "Luck: " + charaStatus.luck + "";
		clarityText.text = "Clarity: " + charaStatus.clarity + "";
		zenText.text = "Zen: " + charaStatus.zen + "";
		atkText.text = "Attack: " + charaStatus.currentAtk + "";
		defText.text = "Defense: " + charaStatus.currentDef + "";
		magAtkText.text = "Magic Attack: " + charaStatus.magicAtk + "";
		magDefText.text = "Magic Defense: " + charaStatus.magicDef + "";
		evaText.text = "Evasion: " + charaStatus.evasion + "";
		bonusText.text = "Bonus: ";
	}

	public void increasePreview()
	{
		Destroy(charaPreview);
		if (selectedChara < numCharas)
		{
			selectedChara++;
		}
		if (selectedChara == numCharas)
		{
			selectedChara = 0;
		}

		createPreview();
		if (equipList.activeInHierarchy)
		{
			loadEquipStats();
			displayCurrentEquip();
		}
	}

	public void decreasePreview()
	{
		Destroy(charaPreview);
		if (selectedChara >= 0)
		{
			selectedChara--;
		}
		if (selectedChara < 0)
		{
			selectedChara = numCharas - 1;
		}

		createPreview();
		if (equipList.activeInHierarchy)
		{
			loadEquipStats();
			displayCurrentEquip();
		}
	}

	public void openEquipList()
	{
		reset();
		statsPanel.SetActive(true);
		equipList.SetActive(true);

		loadEquipStats();
		listEquip();
		displayCurrentEquip();
	}

	public void loadEquipStats()
	{
		charaStatus = gameController.availCharas[selectedChara].GetComponent<characterStatusController>();
		charaEquipManager = gameController.availCharas[selectedChara].GetComponent<equipManager>();
		charaEquipManager.charaMenuController = this;
		currentChara = charaEquipManager.character;

		nameText.text = charaStatus.charaName;
		levelText.text = "Level: " + charaStatus.level;
		hpText.text = "HP: " + charaStatus.currentHP + "/" + charaStatus.maxHP;
		mpText.text = "MP: " + charaStatus.currentMP + "/" + charaStatus.maxMP;
		expText.text = "Exp: " + charaStatus.currentExp + "/" + charaStatus.expToNextLvl;
		skillPointText.text = "Skill Points: " + charaStatus.skillPoints;
		strText.text = "Strength: " + charaStatus.strength;
		intText.text = "Intelligence: " + charaStatus.intelligence + "";
		dexText.text = "Dexterity: " + charaStatus.dexterity + "";
		agiText.text = "Agility: " + charaStatus.agility + "";
		endurText.text = "Endurance: " + charaStatus.endurance + "";
		luckText.text = "Luck: " + charaStatus.luck + "";
		clarityText.text = "Clarity: " + charaStatus.clarity + "";
		zenText.text = "Zen: " + charaStatus.zen + "";
		atkText.text = "Attack: " + charaStatus.currentAtk + "";
		defText.text = "Defense: " + charaStatus.currentDef + "";
		magAtkText.text = "Magic Attack: " + charaStatus.magicAtk + "";
		magDefText.text = "Magic Defense: " + charaStatus.magicDef + "";
		evaText.text = "Evasion: " + charaStatus.evasion + "";
	}

	public void displayCurrentEquip()
	{
		numCharas = gameController.availCharas.Count;
		charaPreviews = gameController.availCharas.ToArray();
		if (selectedChara > numCharas)
		{
			selectedChara = gameController.currentChara;
		}
		charaEquipManager = gameController.availCharas[selectedChara].GetComponent<equipManager>();

		if (charaEquipManager.mainWeapon)
		{
			equippedWeaponText.text = 
				charaEquipManager.mainWeapon.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedWeaponText.text = "Empty";
		}

		if (charaEquipManager.subWeapon)
		{
			equippedSubWpnText.text = 
				charaEquipManager.subWeapon.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedSubWpnText.text = "Empty";
		}

		if (charaEquipManager.head)
		{
			equippedHeadText.text = 
				charaEquipManager.head.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedHeadText.text = "None";
		}

		if (charaEquipManager.chest)
		{
			equippedChestText.text = 
				charaEquipManager.chest.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedChestText.text = "Empty";
		}

		if (charaEquipManager.hands)
		{
			equippedHandsText.text = 
				charaEquipManager.hands.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedHandsText.text = "Empty";
		}

		if (charaEquipManager.legs)
		{
			equippedLegsText.text = 
				charaEquipManager.legs.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedLegsText.text = "Empty";
		}

		if (charaEquipManager.feet)
		{
			equippedFeetText.text = 
				charaEquipManager.feet.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedFeetText.text = "Empty";
		}

		if (charaEquipManager.accessory)
		{
			equippedAccessoryText.text = 
				charaEquipManager.accessory.GetComponent<itemData>().itemName;
		}
		else
		{
			equippedAccessoryText.text = "Empty";
		}
	}

	public void listEquip()
	{
		if (numEquipItems == 0)
		{
			for (int i = 0; i < inventoryController.numItemsPtInventory; i++)
			{
				if (inventoryController.partyInventory[i].type == ItemType.armour ||
					inventoryController.partyInventory[i].type == ItemType.weapon)
				{
					equipmentList[numEquipItems] = Instantiate(equipListPrefab);
					equipmentList[numEquipItems].transform.SetParent(equipListContent.transform, false);
					equipmentList[numEquipItems].name = inventoryController.partyInventory[i].itemName;
					equipmentList[numEquipItems].GetComponentInChildren<Text>().text = equipmentList[numEquipItems].name;
					equipItemController = equipmentList[numEquipItems].GetComponent<equipItemController>();
					equipItemController.charaMenu = this;
					equipItemController.itemID = inventoryController.partyInventory[i].itemID;
					equipItemController.inventoryIndex = i;
					equipItemController.itemNumber = numEquipItems;
					equipItemController.itemName = equipmentList[numEquipItems].name;
					equipItemController.itemSprite = inventoryController.partyInventory[i].itemImage;
					equipItemController.itemImage.sprite = equipItemController.itemSprite;
					equipItemController.isEquipped = inventoryController.partyInventory[i].isEquipped;
					equipItemController.EquipSlot = inventoryController.partyInventory[i].EquipSlot;

					if (equipItemController.isEquipped)
					{
						equipmentList[numEquipItems].GetComponentInChildren<Text>().color = equippedColor;
					}
					numEquipItems++;
				}
			}
		}
		else
		{
			for (int i = 0; i < numEquipItems; i++)
			{
				Destroy(equipmentList[i]);
			}
			numEquipItems = 0;
			listEquip();
		}
	}

	public void changeEquip()
	{
		if (selectedEquipItem != -1)
		{	
			equipItemController = equipmentList[selectedEquipItem].GetComponent<equipItemController>();
			int inventoryIndex = equipItemController.inventoryIndex;
			EquipmentSlot equipSlot = equipItemController.EquipSlot;
			if (!equipItemController.isEquipped)
			{
				charaEquipManager.equip(inventoryIndex, equipSlot);
				charaStatus.updateStats();
				equipItemController.isEquipped = true;
				equipmentList[selectedEquipItem].GetComponentInChildren<Text>().color = equippedColor;
			}
			displayCurrentEquip();
			selectedEquipSlot = -1;
			selectedEquipItem = -1;
		}
	}

	public void removeEquip()
	{
		if (selectedEquipItem != -1)
		{
			equipItemController = equipmentList[selectedEquipItem].GetComponent<equipItemController>();
			int index = equipItemController.inventoryIndex;
			itemData iteminfo = inventoryController.partyInventory[index].GetComponent<itemData>();
			Characters equippedChara = iteminfo.charaUsing;

			if (equipItemController.isEquipped)
			if (currentChara == equippedChara)
			{
				charaEquipManager.unequip(EquipSlot);
				charaStatus.updateStats();
				equipItemController.isEquipped = false;
				equipmentList[selectedEquipItem].GetComponentInChildren<Text>().color = unequippedColor;
			}
			displayCurrentEquip();
			loadEquipStats();
			selectedEquipSlot = -1;
			selectedEquipItem = -1;
		}

		if (selectedEquipSlot != -1)
		{
			charaEquipManager.unequip(EquipSlot);
			charaStatus.updateStats();
			displayCurrentEquip();
			loadEquipStats();
			selectedEquipSlot = -1;
			selectedEquipItem = -1;
		}
	}
		
	public void selectEquipSlot(int slot)
	{
		selectedEquipSlot = slot;
		switch (selectedEquipSlot)
		{
			case 0:
			{
				EquipSlot = EquipmentSlot.mainWpn;
				break;
			}
			case 1:
			{
				EquipSlot = EquipmentSlot.subWpn;
				break;
			}
			case 2:
			{
				EquipSlot = EquipmentSlot.head;
				break;
			}
			case 3:
			{
				EquipSlot = EquipmentSlot.chest;
				break;
			}
			case 4:
			{
				EquipSlot = EquipmentSlot.hands;
				break;
			}
			case 5:
			{
				EquipSlot = EquipmentSlot.legs;
				break;
			}
			case 6:
			{
				EquipSlot = EquipmentSlot.feet;
				break;
			}
			case 7:
			{
				EquipSlot = EquipmentSlot.accessory;
				break;
			}
		}
	}

	public void openSkillChart()
	{
		reset();
		//skillChart.SetActive(true);
		loadSkillChart();
	}

	public void loadSkillChart()
	{
		skillChart.GetComponent<CanvasGroup>().alpha = 1;
		skillChart.GetComponent<CanvasGroup>().interactable = true;
		skillChart.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void unloadSkillChart()
	{
		skillChart.GetComponent<CanvasGroup>().alpha = 0;
		skillChart.GetComponent<CanvasGroup>().interactable = false;
		skillChart.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void increaseSkillChart()
	{
		if (selectedChara < numCharas)
		{
			selectedChara++;
		}
		if (selectedChara == numCharas)
		{
			selectedChara = 0;
		}

		loadSkillChart();
	}

	public void decreseSkillChart()
	{
		if (selectedChara >= 0)
		{
			selectedChara--;
		}
		if (selectedChara < 0)
		{
			selectedChara = numCharas - 1;
		}

		loadSkillChart();
	}

	public void upgradeSkill()
	{
		
	}

	public void learnSkill()
	{
		
	}

	public void openSkillList()
	{
		reset();
		skillList.SetActive(true);
		loadSkillList();
	}

	public void loadSkillList()
	{

	}

	public void increaseSkillList()
	{
		if (selectedChara < numCharas)
		{
			selectedChara++;
		}
		if (selectedChara == numCharas)
		{
			selectedChara = 0;
		}

		loadSkillList();
	}

	public void decreseSkillList()
	{
		if (selectedChara >= 0)
		{
			selectedChara--;
		}
		if (selectedChara < 0)
		{
			selectedChara = numCharas - 1;
		}
		
		loadSkillList();
	}

	public void useSkill()
	{
		
	}

	public void reset()
	{
		Destroy(charaPreview);
		statsPanel.SetActive(false);
		equipList.SetActive(false);
		//skillChart.SetActive(false);
		unloadSkillChart();
		skillList.SetActive(false);

//		if (numCharas == 0)
		{
			numCharas = gameController.availCharas.Count;
			charaPreviews = gameController.availCharas.ToArray();
			if (selectedChara > numCharas)
			{
				selectedChara = gameController.currentChara;
			}
		}
	}
}