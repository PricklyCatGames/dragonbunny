using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class shopPlayerItemController : MonoBehaviour, ISelectHandler
{
	#region variables
	public Image itemImage;
	public Sprite itemSprite;
	public Text itemNameText;
	public Text itemPriceText;
	public int itemID;
	public int inventoryIndex;
	public int itemNumber;
	public string itemName;
	public string itemDescription;
	public int itemPrice;
	public int itemInventoryCount;
//	public int itemCarryLimit;
//	public int itemStackSize;
	public bool isEquipped;
	public int numEquipped;

	public shopMenuController shopMenu;
	#endregion

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void OnSelect(BaseEventData eventData)
	{
		shopMenu.updateDescription(itemNumber);
		shopMenu.selectedItemIndex = itemNumber;
		shopMenu.selectedItemID = itemID;
		shopMenu.playerItem = this;
	}
}