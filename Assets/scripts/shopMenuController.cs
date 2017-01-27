using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shopMenuController : MonoBehaviour
{
	#region variables
	public bool subMenuOpen;
	public GameObject selectMenu;
	public GameObject bgPanel;
	public GameObject buyMenu;
	public Transform buyMenuContent;
	public GameObject sellMenu;
	public Transform sellMenuContent;
	public GameObject equipPanel;
	public GameObject alertPanel;
	public Text alertText;
	string moneyAlert = "You don't have enough money!";
	string inventoryAlert = "Your inventory is full!";
	string carryAlert = "You can't carry anymore of that.";
	string equippedAlert = "That item is equipped.";
	public GameObject shopItemPrefab;
	public GameObject inventoryItemPrefab;
	public GameObject[] shopItems;
	public GameObject[] shopItemDisplay;
	public GameObject[] playerItemList;
	public int numItems;
	public int numItemsAvail;

	public Image itemImage;
	public Sprite itemSprite;
	public Text itemNameText;
	public Text numOwnedText;
	public Text itemDescriptText;

	public Text selectedAmtText;
	public int selectedAmount;
	public int selectedItemIndex;
	public int selectedItemID;
	public int selectedItemPrice;

	public int numCharas;
	public int selectedChara;

	public Color unequippedColor;
	public Color equippedColor;
	public Color improvedStatColor;
	public Color reducedStatColor;

	public gameController gameController;
	public masterListController masterList;
	public inventoryController inventoryController;
	public characterStatusController charaStatusController;
	public equipManager charaEquipManager;
	public shopItemController shopItem;
	public shopPlayerItemController playerItem;
	public itemData itemData;
	#endregion

	// Use this for initialization
	void Start()
	{
		closeMenu();
		selectMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void openBuyMenu()
	{
		subMenuOpen = true;
		selectMenu.SetActive(false);
		bgPanel.SetActive(true);
		buyMenu.SetActive(true);
		selectedItemIndex = 0;
		loadCharaStats();
		loadShopItemList();
	}

	public void openSellMenu()
	{
		subMenuOpen = true;
		selectMenu.SetActive(false);
		bgPanel.SetActive(true);
		sellMenu.SetActive(true);
		selectedItemIndex = 0;
		loadInventoryList();
	}

	public void loadShopItemList()
	{
//		numItems = shopItems.Length;
		shopItemDisplay = new GameObject[numItemsAvail];

		for (int i = 0; i < numItemsAvail; i++)
		{
			shopItemDisplay[i] = Instantiate(shopItemPrefab);
			shopItemDisplay[i].transform.SetParent(buyMenuContent, false);
			shopItem = shopItemDisplay[i].GetComponent<shopItemController>();
			itemData = shopItems[i].GetComponent<itemData>();
			shopItem.shopMenu = this;
			shopItem.name = shopItems[i].name;
			shopItem.itemID = itemData.itemID;
			shopItem.itemNumber = i;
			shopItem.itemName = shopItem.name;
			shopItem.itemNameText.text = shopItem.name;
			shopItem.itemSprite = itemData.itemImage;
			shopItem.itemImage.sprite = shopItem.itemSprite;
			shopItem.itemDescription = itemData.itemDescription;
			shopItem.itemPrice = itemData.cost;
			shopItem.itemPriceText.text = shopItem.itemPrice.ToString();
			shopItem.itemCarryLimit = itemData.carryLimit;
			shopItem.itemStackSize = itemData.stackLimit;
			shopItem.inventoryIndex = itemData.inventoryIndex;
			shopItem.itemInventoryCount = itemData.currentAmount;
			shopItem.numOwned = itemData.totalOwned;
		}

		shopItem = shopItemDisplay[selectedItemIndex].GetComponent<shopItemController>();
		updateDescription(selectedItemIndex);
	}

	public void updateDescription(int itemNumber)
	{
		if (buyMenu.activeInHierarchy)
		{
			itemImage.sprite = shopItem.itemSprite;
			itemNameText.text = shopItem.name;
			numOwnedText.text = "Own: " + shopItem.itemInventoryCount;
			itemDescriptText.text = shopItem.itemDescription;
			selectedAmount = 0;
			selectedItemPrice = shopItem.itemPrice;
			selectedAmtText.text = selectedAmount.ToString();
		}

		if (sellMenu.activeInHierarchy)
		{
			itemImage.sprite = playerItem.itemSprite;
			itemNameText.text = playerItem.name;
			numOwnedText.text = "Own: " + playerItem.itemInventoryCount;
			itemDescriptText.text = playerItem.itemDescription;
			selectedAmount = 0;
			selectedItemPrice = playerItem.itemPrice;
			selectedAmtText.text = selectedAmount.ToString();
		}
	}

	public void increaseAmount()
	{
		int count = 0;

		if (buyMenu.activeInHierarchy)
		{
			int carryLimit = shopItem.itemCarryLimit;
			count = shopItem.itemInventoryCount;
//			Debug.Log("carryLimit= " + carryLimit + ", count= " + count
//				+ ", selectedAmt= " + selectedAmount);

			if ((carryLimit < 0) || (selectedAmount < (carryLimit - count)))
			{
				selectedAmount++;
				selectedAmtText.text = selectedAmount.ToString();
			}
			
			if (count == carryLimit)
			{
				alertPanel.SetActive(true);
				alertText.text = carryAlert;
			}
		}

		if (sellMenu.activeInHierarchy)
		{
			count = playerItem.itemInventoryCount - playerItem.numEquipped;

			if (selectedAmount < count)
			{
				selectedAmount++;
				selectedAmtText.text = selectedAmount.ToString();
			}	
		}
	}

	public void decreaseAmount()
	{
		if (selectedAmount > 0)
		{
			selectedAmount--;
			selectedAmtText.text = selectedAmount.ToString();
		}
	}

	public void buyItem()
	{
		if (buyMenu.activeInHierarchy)
		{
			int cost = selectedItemPrice * selectedAmount;
			if (cost <= inventoryController.coins && inventoryController.numItemsPtInventory <
				inventoryController.partyInventorySize && (shopItem.itemInventoryCount < 
					shopItem.itemCarryLimit || shopItem.itemCarryLimit < 0))
			{
				inventoryController.coins -= cost;
				inventoryController.addItem(selectedItemID, selectedAmount);
				shopItem.itemInventoryCount += selectedAmount;
				shopItem.numOwned += selectedAmount;
				numOwnedText.text = "Own: " + shopItem.itemInventoryCount;
			}
			else if (cost > inventoryController.coins)
			{
				alertPanel.SetActive(true);
				alertText.text = moneyAlert;
			}
			else if (shopItem.itemInventoryCount >= shopItem.itemCarryLimit)
			{
				alertPanel.SetActive(true);
				alertText.text = carryAlert;
			}
			else
			{
				alertPanel.SetActive(true);
				alertText.text = inventoryAlert;
			}
		}
	}

	public void sellItem()
	{
		if (sellMenu.activeInHierarchy)
		{
			bool equipped = playerItem.isEquipped;

			if (!equipped)
			{
				int index = playerItem.inventoryIndex;
				inventoryController.coins += selectedItemPrice * selectedAmount;
				inventoryController.sellItem(selectedItemID, index, selectedAmount);
			}
			else
			{
				alertPanel.SetActive(true);
				alertText.text = equippedAlert;
			}
		}
	}

	public void loadInventoryList()
	{
		numItems = inventoryController.numItemsPtInventory;
		playerItemList = new GameObject[numItems];

		for (int i = 0; i < numItems; i++)
		{
			playerItemList[i] = Instantiate(inventoryItemPrefab);
			playerItemList[i].transform.SetParent(sellMenuContent, false);
			playerItem = playerItemList[i].GetComponent<shopPlayerItemController>();
			itemData = inventoryController.partyInventory[i];
			playerItem.shopMenu = this;
			playerItem.name = itemData.itemName;
			playerItem.itemID = itemData.itemID;
			playerItem.inventoryIndex = i;
			playerItem.itemNumber = i;
			playerItem.itemName = playerItem.name;
			playerItem.itemNameText.text = playerItem.name;
			playerItem.itemSprite = itemData.itemImage;
			playerItem.itemImage.sprite = playerItem.itemSprite;
			playerItem.itemDescription = itemData.itemDescription;
			playerItem.itemPrice = itemData.sellValue;
			playerItem.itemPriceText.text = playerItem.itemPrice.ToString();
			playerItem.itemInventoryCount = itemData.currentAmount;
			playerItem.isEquipped = itemData.isEquipped;
			playerItem.numEquipped = itemData.numEquipped;

			if (playerItem.isEquipped)
			{
				playerItem.itemNameText.color = equippedColor;
			}
		}

		playerItem = playerItemList[selectedItemIndex].GetComponent<shopPlayerItemController>();
		updateDescription(selectedItemIndex);
	}

	public void increaseCharacter()
	{
		if (selectedChara < gameController.availCharas.Count)
		{
			selectedChara++;
		}
		else
		{
			selectedChara = 0;
		}

		loadCharaStats();
	}

	public void decreaseCharacter()
	{
		if (selectedChara > 0)
		{
			selectedChara--;
		}
		else
		{
			selectedChara = gameController.availCharas.Count - 1;
		}

		loadCharaStats();
	}

	public void loadCharaStats()
	{
		
	}

	public void openEquipPanel()
	{
		equipPanel.SetActive(true);
	}

	public void equipCharacter()
	{
		
	}

	public void closeEquipPanel()
	{
		equipPanel.SetActive(false);
	}

	public void openAlert()
	{
		alertPanel.SetActive(true);
	}

	public void dismissAlert()
	{
		alertPanel.SetActive(false);
	}

	public void closeMenu()
	{
		if (buyMenu.activeInHierarchy)
		{
			for (int i = numItemsAvail - 1; i >= 0; i--)
			{
//				Debug.Log("i: " + i + ", destroying: " + buyMenuContent.GetChild(i));
//				Destroy(buyMenuContent.GetChild(i).gameObject);
				Destroy(shopItemDisplay[i]);
			}
//			numItems = 0;
		}

		if (sellMenu.activeInHierarchy)
		{
			for (int i = numItems - 1; i >= 0; i--)
			{
				Destroy(playerItemList[i]);
			}
			numItems = 0;
		}

		subMenuOpen = false;
		selectMenu.SetActive(true);
		equipPanel.SetActive(false);
		alertPanel.SetActive(false);
		buyMenu.SetActive(false);
		sellMenu.SetActive(false);
		bgPanel.SetActive(false);

		numCharas = gameController.availCharas.Count;
		selectedChara = gameController.currentChara;
	}

	public void exit()
	{
		closeMenu();
		selectMenu.SetActive(false);
		gameController.closeStoreMenu();
	}
}