using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class inventoryItemController : MonoBehaviour, ISelectHandler
{
	#region variables
	public Image itemImage;
	public Sprite itemSprite;
	public Text itemNameText;
	public Text itemCountText;
	public int itemID;
	public int inventoryIndex;
	public bool isKeyItem;
	public bool canUse;
	public bool canDiscard;
	public int itemNumber;
	public string itemName;
	public string itemDescription;
	public int itemCount;

	public inventoryMenuController inventoryMenu;
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
//		Debug.Log("onselect()");
		inventoryMenu.updateDescription(itemNumber);
		inventoryMenu.selectedItemIndex = itemNumber;
	}
}