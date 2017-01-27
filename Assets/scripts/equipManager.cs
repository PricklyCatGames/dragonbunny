using UnityEngine;
using System.Collections;

public class equipManager : MonoBehaviour
{
	#region variables
	public Characters character;
	public GameObject mainWeapon;
	public GameObject subWeapon;
	public GameObject head;
	public GameObject chest;
	public GameObject hands;
	public GameObject legs;
	public GameObject feet;
	public GameObject accessory;
	public int totalDefense;
	public int totalAttack;

	public ArmourClass armourClass;
	public EquipmentSlot EquipSlot;
	
//	public gameController gameController;
	public masterListController masterList;
	public GameObject partyInventory;
	public inventoryController inventoryController;
	public characterMenuController charaMenuController;
	#endregion

	// Use this for initialization
	void Start()
	{
		masterList = GameObject.Find("MasterList").GetComponent<masterListController>();
		partyInventory = GameObject.Find("partyInventory");
		inventoryController = GameObject.Find("gameController").GetComponent<inventoryController>();

		if (mainWeapon)
		{
			mainWeapon = findInInventory(mainWeapon.name);
			totalDefense += mainWeapon.GetComponent<itemData>().defense;
			totalAttack += mainWeapon.GetComponent<itemData>().damage;
		}
		if (subWeapon)
		{
			subWeapon = findInInventory(subWeapon.name);
			totalDefense += subWeapon.GetComponent<itemData>().defense;
			totalAttack += subWeapon.GetComponent<itemData>().damage;
		}
		if (head)
		{
			head = findInInventory(head.name);
			totalDefense += head.GetComponent<itemData>().defense;
			totalAttack += head.GetComponent<itemData>().damage;
		}
		if (chest)
		{
			chest = findInInventory(chest.name);
			totalDefense += chest.GetComponent<itemData>().defense;
			totalAttack += chest.GetComponent<itemData>().damage;
		}
		if (hands)
		{
			hands = findInInventory(hands.name);
			totalDefense += hands.GetComponent<itemData>().defense;
			totalAttack += hands.GetComponent<itemData>().damage;
		}
		if (legs)
		{
			legs = findInInventory(legs.name);
			totalDefense += legs.GetComponent<itemData>().defense;
			totalAttack += legs.GetComponent<itemData>().damage;
		}
		if (feet)
		{
			feet = findInInventory(feet.name);
			totalDefense += feet.GetComponent<itemData>().defense;
			totalAttack += feet.GetComponent<itemData>().damage;
		}
		if (accessory)
		{
			accessory = findInInventory(accessory.name);
			totalDefense += accessory.GetComponent<itemData>().defense;
			totalAttack += accessory.GetComponent<itemData>().damage;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public GameObject findInInventory(string itemName)
	{
		GameObject equipItem = null;
		for (int i = 0; i < partyInventory.transform.childCount; i++)
		{
			GameObject child = partyInventory.transform.GetChild(i).gameObject;
			if (child.name == itemName && child.GetComponent<itemData>().isEquipped &&
				child.GetComponent<itemData>().charaUsing == character)
			{
				equipItem = child;
			}
		}
		return equipItem;
	}

	public void equip(int inventoryIndex, EquipmentSlot EquipSlot)
	{
		switch (EquipSlot)
		{
			case EquipmentSlot.mainWpn:
			{
				if (!mainWeapon)
				{
					mainWeapon = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = mainWeapon.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.mainWpn);
					mainWeapon = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = mainWeapon.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.subWpn:
			{
				if (!subWeapon)
				{
					subWeapon = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = subWeapon.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.subWpn);
					subWeapon = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = subWeapon.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.head:
			{
				if (!head)
				{
					head = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = head.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.head);
					head = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = head.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.chest:
			{
				if (!chest)
				{
					chest = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = chest.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.chest);
					chest = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = chest.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.hands:
			{
				if (!hands)
				{
					hands = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = hands.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.hands);
					hands = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = hands.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.legs:
			{
				if (!legs)
				{
					legs = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = legs.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.legs);
					legs = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = legs.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.feet:
			{
				if (!feet)
				{
					feet = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = feet.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.feet);
					feet = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = feet.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
			case EquipmentSlot.accessory:
			{
				if (!accessory)
				{
					accessory = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = accessory.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				else
				{
					unequip(EquipmentSlot.accessory);
					accessory = partyInventory.transform.GetChild(inventoryIndex).gameObject;
					itemData itemData = accessory.GetComponent<itemData>();
					totalDefense += itemData.defense;
					totalAttack += itemData.damage;
					itemData.isEquipped = true;
					itemData.charaUsing = character;
					masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped++;
				}
				break;
			}
		}


	}

	public void unequip(EquipmentSlot slot)
	{
		switch (slot)
		{
			case EquipmentSlot.mainWpn:
			{
				itemData itemData = mainWeapon.GetComponent<itemData>();
//				int itemIndex = mainWeapon.GetComponent<itemData>().inventoryIndex;
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				charaMenuController.markUnequipped(itemIndex);
				mainWeapon.GetComponent<itemData>().isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				mainWeapon = null;
				break;
			}
			case EquipmentSlot.subWpn:
			{
				itemData itemData = subWeapon.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[subWeapon.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				subWeapon = null;
				break;
			}
			case EquipmentSlot.head:
			{
				itemData itemData = head.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[head.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				head = null;
				break;
			}
			case EquipmentSlot.chest:
			{
				itemData itemData = chest.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[chest.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				chest = null;
				break;
			}
			case EquipmentSlot.hands:
			{
				itemData itemData = hands.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[hands.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				hands = null;
				break;
			}
			case EquipmentSlot.legs:
			{
				itemData itemData = legs.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[legs.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				legs = null;
				break;
			}
			case EquipmentSlot.feet:
			{
				itemData itemData = feet.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[feet.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				feet = null;
				break;
			}
			case EquipmentSlot.accessory:
			{
				itemData itemData = accessory.GetComponent<itemData>();
				totalDefense -= itemData.defense;
				totalAttack -= itemData.damage;
//				inventoryController.partyInventory[accessory.GetComponent<itemData>().inventoryIndex].isEquipped = false;
				itemData.isEquipped = false;
				masterList.getItem(itemData.itemID).GetComponent<itemData>().numEquipped--;
				accessory = null;
				break;
			}
		}

		charaMenuController.listEquip();
		charaMenuController.displayCurrentEquip();
	}

	public void equipItem(EquipmentSlot slot, GameObject item)
	{
		
	}

	public void unequipItem(EquipmentSlot slot, GameObject item)
	{
		
	}
}