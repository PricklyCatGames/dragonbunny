using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class inventoryController : MonoBehaviour
{
	#region variables
	public int coins;
	public Text coinsText;
	public int numKeyItems = 20;
	public itemData[] keyItems;
	public int keyItemsObtained = 0;
	public int partyInventorySize = 40;
	public itemData[] partyInventory;
//	public int[] inventoryStackSizes;
	public int numItemsPtInventory;
	public int storageSize = 40;
	public itemData[] storage;
//	public int[] storageStackSizes;
	public int numItemsStorage;
	public int selectedAmount;

	public masterListController masterList;
	public gameController gameController;
	public inventoryMenuController inventoryMenuController;
	public GameObject inventory;
	public alertManager alertManager;
	#endregion

	// Use this for initialization
	void Start()
	{
		coinsText.text = "Coins: " + coins;
		numItemsPtInventory = inventory.transform.childCount;
		for (int i = 0; i < numItemsPtInventory; i++)
		{
			partyInventory[i] = inventory.transform.GetChild(i).GetComponent<itemData>();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void addKeyItem(int itemID)
	{
		keyItems[keyItemsObtained] = masterList.getItem(itemID).GetComponent<itemData>();
		keyItemsObtained++;
	}

	public void addItem(int itemID)
	{
		if (numItemsPtInventory == 0)
		{
			GameObject newItem = Instantiate(masterList.getItem(itemID).GetComponent<itemData>().itemPrefab);
			partyInventory[0] = newItem.GetComponent<itemData>();
			newItem.name = partyInventory[0].itemName;
			newItem.transform.SetParent(inventory.transform, false);
			masterList.getItem(itemID).GetComponent<itemData>().totalOwned++;
			masterList.getItem(itemID).GetComponent<itemData>().currentAmount = 1;
			partyInventory[0].numInStack = 1;
			partyInventory[0].inventoryIndex = 0;
			numItemsPtInventory = 1;
		}
		else
		{
			bool found = false;
			for (int i = 0; i < numItemsPtInventory; i++)
			{
				if (partyInventory[i].itemID == itemID)
				{
					if (partyInventory[i].currentAmount < partyInventory[i].carryLimit &&
						partyInventory[i].numInStack < partyInventory[i].stackLimit)
					{
						masterList.getItem(itemID).GetComponent<itemData>().currentAmount++;
						masterList.getItem(itemID).GetComponent<itemData>().totalOwned++;
//						if (partyInventory[i].numInStack < partyInventory[i].stackLimit)
//						{
							found = true;
							partyInventory[i].numInStack++;
							break;
//						}
//						else
//						{
//							continue;
//						}
					}
					else if (partyInventory[i].currentAmount > partyInventory[i].carryLimit)
					{
						found = true;
						carryLimitAlert();
						break;
					}
					else
					{
						continue;
					}
				}
			}
			if (!found)
			{
				if (numItemsPtInventory < partyInventorySize)
				{
					GameObject newItem = Instantiate(masterList.getItem(itemID).GetComponent<itemData>().itemPrefab);
					partyInventory[numItemsPtInventory] = newItem.GetComponent<itemData>();
					newItem.name = partyInventory[numItemsPtInventory].itemName;
					newItem.transform.SetParent(inventory.transform, false);
					masterList.getItem(itemID).GetComponent<itemData>().totalOwned++;
					masterList.getItem(itemID).GetComponent<itemData>().currentAmount++;
					partyInventory[numItemsPtInventory].inventoryIndex = numItemsPtInventory;
					numItemsPtInventory++;
				}
				else
				{
					inventoryFullAlert();
				}
			}
		}
		coinsText.text = coins.ToString();
	}

	public void addItem(int itemID, int amount)
	{
		if (numItemsPtInventory == 0)
		{
			GameObject newItem = Instantiate(masterList.getItem(itemID).GetComponent<itemData>().itemPrefab);
			partyInventory[0] = newItem.GetComponent<itemData>();
			newItem.name = partyInventory[0].itemName;
			newItem.transform.SetParent(inventory.transform, false);
			partyInventory[0].inventoryIndex = 0;
			numItemsPtInventory++;
			if (amount <= partyInventory[0].stackLimit)
			{
				partyInventory[0].numInStack = amount;
				masterList.getItem(itemID).GetComponent<itemData>().currentAmount = amount;
				masterList.getItem(itemID).GetComponent<itemData>().totalOwned += amount;
			}
			else
			{
				masterList.getItem(itemID).GetComponent<itemData>().totalOwned += partyInventory[0].stackLimit;
				masterList.getItem(itemID).GetComponent<itemData>().currentAmount = partyInventory[0].stackLimit;
				partyInventory[0].numInStack = partyInventory[0].stackLimit;
				amount -= partyInventory[0].stackLimit;
				addItem(itemID, amount);
			}
		}
		else
		{
			if ((masterList.getItem(itemID).GetComponent<itemData>().totalOwned + amount) < 
				masterList.getItem(itemID).GetComponent<itemData>().carryLimit)
			{
				bool found = false;
				for (int i = 0; i < numItemsPtInventory; i++)
				{
					if (partyInventory[i].itemID == itemID)
					{
						if (partyInventory[i].numInStack < partyInventory[i].stackLimit)
						{
							if ((amount + partyInventory[i].numInStack) <= partyInventory[i].stackLimit)
							{
								partyInventory[i].numInStack += amount;
								masterList.getItem(itemID).GetComponent<itemData>().currentAmount += amount;
								masterList.getItem(itemID).GetComponent<itemData>().totalOwned += amount;
								found = true;
								break;
							}
							else
							{
								int spaceInStack = partyInventory[i].stackLimit - partyInventory[i].numInStack;
								partyInventory[i].numInStack = partyInventory[i].stackLimit;
								masterList.getItem(itemID).GetComponent<itemData>().currentAmount += spaceInStack;
								masterList.getItem(itemID).GetComponent<itemData>().totalOwned += spaceInStack;
								amount -= spaceInStack;
								addItem(itemID, amount);
							}
						}
						else
						{
							continue;
						}
					}
				}
				if (!found)
				{
					if (numItemsPtInventory < partyInventorySize)
					{
						GameObject newItem = Instantiate(masterList.getItem(itemID).GetComponent<itemData>().itemPrefab);
						partyInventory[numItemsPtInventory] = newItem.GetComponent<itemData>();
						newItem.name = partyInventory[numItemsPtInventory].itemName;
						newItem.transform.SetParent(inventory.transform, false);
						partyInventory[numItemsPtInventory].inventoryIndex = numItemsPtInventory;

						if (amount <= masterList.getItem(itemID).GetComponent<itemData>().stackLimit)
						{
							masterList.getItem(itemID).GetComponent<itemData>().totalOwned += amount;
							masterList.getItem(itemID).GetComponent<itemData>().currentAmount += amount;
							partyInventory[numItemsPtInventory].numInStack = amount;
						}
						else
						{
							int stackLimit = masterList.getItem(itemID).GetComponent<itemData>().stackLimit;
							partyInventory[numItemsPtInventory].numInStack = stackLimit;
							masterList.getItem(itemID).GetComponent<itemData>().currentAmount += stackLimit;
							masterList.getItem(itemID).GetComponent<itemData>().totalOwned += stackLimit;
							amount -= stackLimit;
							addItem(itemID, amount);
						}
						numItemsPtInventory++;
					}
					else
					{
						inventoryFullAlert();
					}
				}
			}
			else
			{
				carryLimitAlert();
			}
		}
		coinsText.text = coins.ToString();
	}

	public void moveItem()
	{
		int inventorySlot = inventoryMenuController.selectedItemIndex;
		if (inventoryMenuController.inventoryPanel.activeInHierarchy)
		{
			if (!partyInventory[inventorySlot].isEquipped)
			{
				storage[numItemsStorage] = partyInventory[inventorySlot];
				storage[numItemsStorage].inventoryIndex = -1;
				numItemsStorage++;

				if (partyInventory[inventorySlot].currentAmount == 1)
				{
					numItemsPtInventory--;
//					inventoryMenuController.numItemsPtInventory--;

					if (inventorySlot >= numItemsPtInventory)
					{
						inventoryMenuController.selectedItemIndex--;
					}
					for (int i = inventorySlot; i < numItemsPtInventory; i++)
					{
						partyInventory[i] = partyInventory[i + 1];
						partyInventory[i].inventoryIndex = i;
//						partyInventory[i].currentAmount = partyInventory[i + 1].currentAmount;
					}
					partyInventory[numItemsPtInventory + 1] = null;
//					partyInventory[numItemsPtInventory + 1].currentAmount = 0;
				}
				else
				{
//					partyInventory[inventorySlot].currentAmount -= selectAmount();
//					numItemsPtInventory--;
//					for (int i = 0; i < numItemsPtInventory; i++)
//					{
//						partyInventory[inventorySlot] = partyInventory[inventorySlot + 1];
//						partyInventory[inventorySlot].currentAmount = inventoryStackSizes[inventorySlot + 1];
//					}
//					partyInventory[numItemsPtInventory + 1] = null;
//					inventoryStackSizes[numItemsPtInventory + 1] = 0;
				}

				if (inventoryMenuController.isActiveAndEnabled)
				{
					inventoryMenuController.updateList();
				}
			}
		}
		else
		{
			partyInventory[numItemsPtInventory] = storage[inventorySlot];
			partyInventory[numItemsPtInventory].inventoryIndex = numItemsPtInventory;
			storage[inventorySlot] = null;

		}
	}

	public void useItem()
	{
		int inventorySlot = inventoryMenuController.selectedItemIndex;
		int itemID = partyInventory[inventorySlot].itemID;
		if (numItemsPtInventory > 0 && partyInventory[inventorySlot].type == ItemType.usable)
		{
			selectCharacter(partyInventory[inventorySlot]);
			partyInventory[inventorySlot].numInStack--;
			masterList.getItem(itemID).GetComponent<itemData>().totalOwned--;
			masterList.getItem(itemID).GetComponent<itemData>().currentAmount--;
			if (partyInventory[inventorySlot].numInStack == 0)
			{
				partyInventory[inventorySlot].inventoryIndex = -1;
				numItemsPtInventory--;
				for (int i = inventorySlot; i < numItemsPtInventory; i++)
				{
					partyInventory[i] = partyInventory[i + 1];
					partyInventory[i].inventoryIndex = i;
				}
				partyInventory[numItemsPtInventory + 1] = null;
				Destroy(inventory.transform.GetChild(inventorySlot));
			}

			if (inventoryMenuController.isActiveAndEnabled)
			{
				inventoryMenuController.updateList();
			}
		}
	}

	public void useItem(int inventorySlot)
	{
//		if (numItemsPtInventory > 0 && partyInventory[inventorySlot].type == ItemType.usable)
		{
//			selectCharacter(partyInventory[inventorySlot]);
			int itemID = partyInventory[inventorySlot].itemID;
			partyInventory[inventorySlot].numInStack--;
			masterList.getItem(itemID).GetComponent<itemData>().totalOwned--;
			masterList.getItem(itemID).GetComponent<itemData>().currentAmount--;
			if (partyInventory[inventorySlot].numInStack == 0)
			{
				partyInventory[inventorySlot].inventoryIndex = -1;
				numItemsPtInventory--;
				for (int i = inventorySlot; i < numItemsPtInventory; i++)
				{
					partyInventory[i] = partyInventory[i + 1];
					partyInventory[i].inventoryIndex = i;
				}
				partyInventory[numItemsPtInventory + 1] = null;
				Destroy(inventory.transform.GetChild(inventorySlot));
			}

			if (inventoryMenuController.isActiveAndEnabled)
			{
				inventoryMenuController.updateList();
			}
		}
	}

	public void selectCharacter(itemData item)
	{
		
	}

	public void discardItem()
	{
		int inventorySlot = inventoryMenuController.selectedItemIndex;
		int itemID = partyInventory[inventorySlot].itemID;
		if (numItemsPtInventory > 0 && partyInventory[inventorySlot].canDiscard && 
			!partyInventory[inventorySlot].isEquipped)
		{
			if (partyInventory[inventorySlot].numInStack == 1)
			{
				partyInventory[inventorySlot].inventoryIndex = -1;
				numItemsPtInventory--;
				masterList.getItem(itemID).GetComponent<itemData>().totalOwned--;
				masterList.getItem(itemID).GetComponent<itemData>().currentAmount--;

				if (inventorySlot >= numItemsPtInventory && inventoryMenuController.isActiveAndEnabled)
				{
					inventoryMenuController.selectedItemIndex--;
				}
				for (int i = inventorySlot; i < numItemsPtInventory; i++)
				{
					partyInventory[i] = partyInventory[i + 1];
					partyInventory[i].inventoryIndex = i;
				}
				partyInventory[numItemsPtInventory + 1] = null;
				Destroy(inventory.transform.GetChild(inventorySlot));
			}
			else
			{
				selectAmount();
//				partyInventory[inventorySlot].currentAmount -= selectedAmount;
//				numItemsPtInventory--;
//				for (int i = 0; i < numItemsPtInventory; i++)
//				{
//					partyInventory[inventorySlot] = partyInventory[inventorySlot + 1];
//					partyInventory[inventorySlot].numInStack = partyInventory[inventorySlot + 1];
//				}
//				partyInventory[numItemsPtInventory + 1] = null;
			}

			if (inventoryMenuController.isActiveAndEnabled)
			{
				inventoryMenuController.updateList();
			}
		}
	}

	public void sellItem(int itemID, int index, int amount)
	{
		if (numItemsPtInventory > 0 && partyInventory[index].canDiscard && 
			!partyInventory[index].isEquipped)
		{
			if (partyInventory[index].numInStack == amount)
			{
				partyInventory[index].inventoryIndex = -1;
				numItemsPtInventory--;
				masterList.getItem(itemID).GetComponent<itemData>().totalOwned -= amount;
				masterList.getItem(itemID).GetComponent<itemData>().currentAmount -= amount;

				if (index >= numItemsPtInventory && inventoryMenuController.isActiveAndEnabled)
				{
					inventoryMenuController.selectedItemIndex--;
				}
				for (int i = index; i < numItemsPtInventory; i++)
				{
					partyInventory[i] = partyInventory[i + 1];
					partyInventory[i].inventoryIndex = i;
				}
				partyInventory[numItemsPtInventory + 1] = null;
				Destroy(inventory.transform.GetChild(index));
			}
			else if (partyInventory[index].numInStack < amount)
			{
				partyInventory[index].numInStack -= amount;
				masterList.getItem(itemID).GetComponent<itemData>().totalOwned -= amount;
				masterList.getItem(itemID).GetComponent<itemData>().currentAmount -= amount;
			}
			else
			{
				partyInventory[index].inventoryIndex = -1;
				numItemsPtInventory--;
				masterList.getItem(itemID).GetComponent<itemData>().totalOwned -= partyInventory[index].numInStack;
				masterList.getItem(itemID).GetComponent<itemData>().currentAmount -= partyInventory[index].numInStack;
				amount -= partyInventory[index].numInStack;

				if (index >= numItemsPtInventory && inventoryMenuController.isActiveAndEnabled)
				{
					inventoryMenuController.selectedItemIndex--;
				}
				for (int i = index; i < numItemsPtInventory; i++)
				{
					partyInventory[i] = partyInventory[i + 1];
					partyInventory[i].inventoryIndex = i;
				}
				partyInventory[numItemsPtInventory + 1] = null;
				Destroy(inventory.transform.GetChild(index));

				for (int i = 0; i < numItemsPtInventory; i++)
				{
					if (partyInventory[i].itemID == itemID && !partyInventory[i].isEquipped)
					{
						sellItem(itemID, i, amount);
					}
				}
			}

			if (inventoryMenuController.isActiveAndEnabled)
			{
				inventoryMenuController.updateList();
			}
		}
		coinsText.text = coins.ToString();
	}

	public void selectAmount()
	{
		inventoryMenuController.openSelectAmountMenu();
	}

	public void confirmDiscard()
	{
		int inventorySlot = inventoryMenuController.selectedItemIndex;
		int itemID = partyInventory[inventorySlot].itemID;
		partyInventory[inventorySlot].numInStack -= selectedAmount;
		masterList.getItem(itemID).GetComponent<itemData>().totalOwned--;
		masterList.getItem(itemID).GetComponent<itemData>().currentAmount--;

		if (partyInventory[inventorySlot].numInStack == 0)
		{
			partyInventory[inventorySlot].inventoryIndex = -1;
			numItemsPtInventory--;

			if (inventorySlot >= numItemsPtInventory)
			{
				inventoryMenuController.selectedItemIndex--;
			}
			for (int i = inventorySlot; i < numItemsPtInventory; i++)
			{
				partyInventory[i] = partyInventory[i + 1];
				partyInventory[i].inventoryIndex = i;
			}
			partyInventory[numItemsPtInventory + 1] = null;
			Destroy(inventory.transform.GetChild(inventorySlot));
		}
	}

	public void sort()
	{
		
	}

	public void carryLimitAlert()
	{
		alertManager.alertText.text = "You can't carry anymore of that.";
		alertManager.alertDuration = 5f;
	}

	public void inventoryFullAlert()
	{
		alertManager.alertText.text = "Inventory full";
		alertManager.alertDuration = 5f;
	}
}