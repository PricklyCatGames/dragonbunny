using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class inventoryMenuController : MonoBehaviour
{
	#region variables
	public float prefabHeight = 50;
	public Transform initialKeyItemPos;
	public Transform initialItemPos;
	Vector2 itemPos;
	public GameObject keyItemPrefab;
	public GameObject keyItemList;
	public GameObject keyItemContent;
	public Image keyItemImage;
	public Text keyItemName;
	public Text keyItemDescription;
	public GameObject inventoryItemPrefab;
	Text[] itemText = new Text[2];
	public GameObject inventoryPanel;
	public GameObject inventoryContent;
	public GameObject storagePanel;
	public GameObject storageContent;
	public GameObject descriptionPanel;
	public Image itemImage;
	public Text itemName;
	public Text itemDescription;
	public GameObject itemMenu;
	public GameObject characterSelectMenu;
	public GameObject selectAmountMenu;
	public Text selectAmountText;
	public int selectedAmount;
	public int selectedItemIndex;
	public GameObject[] keyItems;
	public int numKeyItems;
	public int partyInventorySize = 40;
	public GameObject[] partyInventory;
//	public int[] inventoryStackSizes;
	public int numItemsPtInventory;
	public int storageSize = 40;
	public GameObject[] storage;
//	public int[] storageStackSizes;
	public int numItemsStorage;

	public masterListController masterList;
	public gameController gameController;
	public inventoryController inventoryController;
	inventoryItemController inventoryItemController;
	#endregion

	// Use this for initialization
	void Start()
	{
		numKeyItems = inventoryController.keyItemsObtained;
		numItemsPtInventory = inventoryController.numItemsPtInventory;
		numItemsStorage = inventoryController.numItemsStorage;
		keyItems = new GameObject[numKeyItems];
		partyInventory = new GameObject[numItemsPtInventory];
		storage = new GameObject[numItemsStorage];
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void openKeyItemList()
	{
		closeMenu();
		keyItemList.SetActive(true);
		numKeyItems = inventoryController.keyItemsObtained;
		keyItems = new GameObject[numKeyItems];
		itemPos = initialKeyItemPos.position;

		for (int i = 0; i < numKeyItems; i++)
		{
			keyItems[i] = Instantiate(keyItemPrefab) as GameObject;
			keyItems[i].name = inventoryController.keyItems[i].itemName;
			inventoryItemController = keyItems[i].GetComponent<inventoryItemController>();
			keyItems[i].transform.SetParent(keyItemContent.transform, false);
			keyItems[i].transform.position = itemPos;
			keyItems[i].GetComponentInChildren<Text>().text = keyItems[i].name;
			inventoryItemController.inventoryMenu = this;
			inventoryItemController.itemID = inventoryController.keyItems[i].itemID;
			inventoryItemController.isKeyItem = true;
			inventoryItemController.itemNumber = i;
			inventoryItemController.itemName = keyItems[i].name;
			inventoryItemController.itemSprite = inventoryController.keyItems[i].itemImage;
			inventoryItemController.itemNameText.text = inventoryItemController.itemName;
			inventoryItemController.itemDescription = inventoryController.keyItems[i].itemDescription;
//			Debug.Log("itemPos.y = " + itemPos.y + ", prefabHeight = " + prefabHeight + ", newY = "
//					+ (itemPos.y - prefabHeight));
			itemPos = new Vector2(itemPos.x, itemPos.y - prefabHeight);
		}

		keyItemImage.sprite = keyItems[selectedItemIndex].GetComponent<inventoryItemController>().itemSprite;
		keyItemName.text = keyItems[selectedItemIndex].name;
		keyItemDescription.text = keyItems[selectedItemIndex].GetComponent<inventoryItemController>().itemDescription;
	}

	public void openItemList()
	{
		closeMenu();
		inventoryPanel.SetActive(true);
		descriptionPanel.SetActive(true);
		itemMenu.SetActive(true);
		numItemsPtInventory = inventoryController.numItemsPtInventory;
		partyInventory = new GameObject[numItemsPtInventory];
		itemPos = initialItemPos.position;

		for (int i = 0; i < numItemsPtInventory; i++)
		{
			partyInventory[i] = Instantiate(inventoryItemPrefab) as GameObject;
			inventoryItemController = partyInventory[i].GetComponent<inventoryItemController>();
			partyInventory[i].name = inventoryController.partyInventory[i].itemName;
			partyInventory[i].transform.SetParent(inventoryContent.transform, false);
			partyInventory[i].transform.position = itemPos;
			inventoryItemController.inventoryMenu = this;
			inventoryItemController.itemID = inventoryController.partyInventory[i].itemID;
			inventoryItemController.inventoryIndex = i;
			inventoryItemController.itemNumber = i;
			inventoryItemController.itemName = partyInventory[i].name;
			inventoryItemController.itemDescription = inventoryController.partyInventory[i].itemDescription;
			inventoryItemController.itemCount = inventoryController.partyInventory[i].currentAmount;
			inventoryItemController.itemSprite = inventoryController.partyInventory[i].itemImage;
			inventoryItemController.itemImage.sprite = inventoryItemController.itemSprite;
			itemText = partyInventory[i].GetComponentsInChildren<Text>();
			itemText[0].text = partyInventory[i].name;
			if (inventoryItemController.itemCount > 1)
			{
				itemText[1].text = inventoryItemController.itemCount.ToString();
			}
			else
			{
				itemText[1].text = "";
			}
//			Debug.Log("itemPos.y = " + itemPos.y + ", prefabHeight = " + prefabHeight + ", newY = "
//					+ (itemPos.y - prefabHeight));
			itemPos = new Vector2(itemPos.x, itemPos.y - prefabHeight);
		}

		if (numItemsPtInventory > 0)
		{
			itemImage.sprite = partyInventory[selectedItemIndex].GetComponent<inventoryItemController>().itemSprite;
			itemName.text = partyInventory[selectedItemIndex].name;
			itemDescription.text = partyInventory[selectedItemIndex].GetComponent<inventoryItemController>().itemDescription;
		}
		else
		{
			itemName.text = "No items to display.";
			itemDescription.text = "";
		}
	}

	public void openStorageList()
	{
		closeMenu();
		storagePanel.SetActive(true);
		descriptionPanel.SetActive(true);
		numItemsStorage = inventoryController.numItemsStorage;
		storage = new GameObject[numItemsStorage];
		itemPos = initialItemPos.position;

		for (int i = 0; i < numItemsStorage; i++)
		{
			storage[i] = Instantiate(inventoryItemPrefab) as GameObject;
			storage[i].name = inventoryController.storage[i].itemName;
			inventoryItemController = storage[i].GetComponent<inventoryItemController>();
			storage[i].transform.SetParent(storageContent.transform, false);
			storage[i].transform.position = itemPos;
			inventoryItemController.inventoryMenu = this;
			inventoryItemController.itemID = inventoryController.storage[i].itemID;
			inventoryItemController.inventoryIndex = -1;
			inventoryItemController.itemNumber = i;
			inventoryItemController.itemName = storage[i].name;
			inventoryItemController.itemDescription = inventoryController.storage[i].itemDescription;
			inventoryItemController.itemCount = inventoryController.storage[i].currentAmount;
			inventoryItemController.itemSprite = inventoryController.storage[i].itemImage;
			inventoryItemController.itemImage.sprite = inventoryItemController.itemSprite;
			itemText = storage[i].GetComponentsInChildren<Text>();
			itemText[0].text = storage[i].name;
			if (inventoryItemController.itemCount > 1)
			{
				itemText[1].text = inventoryItemController.itemCount.ToString();
			}
			else
			{
				itemText[1].text = "";
			}
			itemPos = new Vector2(itemPos.x, itemPos.y - prefabHeight);
		}

		if (numItemsStorage > 0)
		{
			itemImage.sprite = storage[selectedItemIndex].GetComponent<inventoryItemController>().itemSprite;
			itemName.text = storage[selectedItemIndex].name;
			itemDescription.text = storage[selectedItemIndex].GetComponent<inventoryItemController>().itemDescription;
		}
		else
		{
			itemName.text = "No items to display.";
			itemDescription.text = "";
		}
	}

	public void updateDescription(int itemNumber)
	{
		if (keyItemList.activeInHierarchy)
		{
			keyItemImage.sprite = keyItems[itemNumber].GetComponent<inventoryItemController>().itemSprite;
			keyItemName.text = keyItems[itemNumber].name;
			keyItemDescription.text = keyItems[itemNumber].GetComponent<inventoryItemController>().itemDescription;
		}

		if (inventoryPanel.activeInHierarchy)
		{
			itemImage.sprite = partyInventory[itemNumber].GetComponent<inventoryItemController>().itemSprite;
			if (partyInventory[itemNumber].GetComponent<inventoryItemController>().itemCount < 2)
			{
				itemName.text = partyInventory[itemNumber].name;
			}
			else
			{
				itemName.text = partyInventory[itemNumber].name + " (" + 
					partyInventory[itemNumber].GetComponent<inventoryItemController>().itemCount + ")";
			}
			itemDescription.text = partyInventory[itemNumber].GetComponent<inventoryItemController>().itemDescription;
			selectedAmount = 1;
			selectAmountText.text = selectedAmount.ToString();
		}

		if (storagePanel.activeInHierarchy)
		{
			itemImage.sprite = storage[itemNumber].GetComponent<inventoryItemController>().itemSprite;
			if (storage[itemNumber].GetComponent<inventoryItemController>().itemCount < 2)
			{
				itemName.text = storage[itemNumber].name;
			}
			else
			{
				itemName.text = storage[itemNumber].name + " (" + 
					storage[itemNumber].GetComponent<inventoryItemController>().itemCount + ")";
			}
			itemDescription.text = storage[itemNumber].GetComponent<inventoryItemController>().itemDescription;
			selectedAmount = 1;
			selectAmountText.text = selectedAmount.ToString();
		}
	}

	public void updateList()
	{
		if (keyItemList.activeInHierarchy)
		{
			openKeyItemList();
		}

		if (inventoryPanel.activeInHierarchy)
		{
			openItemList();
		}

		if (storagePanel.activeInHierarchy)
		{
			openStorageList();
		}
	}

	public void openCharaSelectMenu()
	{
		characterSelectMenu.SetActive(true);
	}

	public void closeCharaSelectMenu()
	{
		characterSelectMenu.SetActive(false);
	}

	public void openSelectAmountMenu()
	{
		selectAmountMenu.SetActive(true);
		selectedAmount = 1;
	}

	public void increaseSelectedAmt()
	{
		int itemCount = 1;
		if (inventoryPanel.activeInHierarchy)
		{
			itemCount = partyInventory[selectedItemIndex].GetComponent<inventoryItemController>().itemCount;
		}
		else if (storagePanel.activeInHierarchy)
		{
			itemCount = storage[selectedItemIndex].GetComponent<inventoryItemController>().itemCount;
		}

		if (selectedAmount < itemCount)
		{
			selectedAmount++;
			selectAmountText.text = selectedAmount.ToString();
		}
	}

	public void decreaseSelectedAmt()
	{
		if (selectedAmount > 0)
		{
			selectedAmount--;
			selectAmountText.text = selectedAmount.ToString();
		}
	}

	public void confirmSelectAmount()
	{
		inventoryController.selectedAmount = selectedAmount;
		inventoryController.confirmDiscard();
		selectAmountMenu.SetActive(false);
		updateList();
	}

	public void cancelSelectAmount()
	{
		inventoryController.selectedAmount = 0;
		selectAmountMenu.SetActive(false);
	}

	public void closeMenu()
	{
		selectedItemIndex = 0;
		if (keyItems != null)
		{
			for (int i = 0; i < numKeyItems; i++)
			{
				Destroy(keyItems[i]);
			}
		}
		if (partyInventory != null)
		{
			for (int i = 0; i < numItemsPtInventory; i++)
			{
				Destroy(partyInventory[i]);
			}
		}
		if (storage != null)
		{
			for (int i = 0; i < numItemsStorage; i++)
			{
				Destroy(storage[i]);
			}
		}
		keyItems = null;
		partyInventory = null;
		storage = null;

		keyItemList.SetActive(false);
		inventoryPanel.SetActive(false);
		storagePanel.SetActive(false);
		descriptionPanel.SetActive(false);
		itemMenu.SetActive(false);
	}
}