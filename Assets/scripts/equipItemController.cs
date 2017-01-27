using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class equipItemController : MonoBehaviour, ISelectHandler
{
	#region variables
	public Image itemImage;
	public Sprite itemSprite;
	public Text itemNameText;
	public int itemID;
	public int inventoryIndex;
	public int itemNumber;
	public string itemName;
	public bool isEquipped;
	public EquipmentSlot EquipSlot;

	public characterMenuController charaMenu;
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
		charaMenu.EquipSlot = EquipSlot;
		charaMenu.selectedEquipItem = itemNumber;
	}
}