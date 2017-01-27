using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class itemBattleController : MonoBehaviour, ISelectHandler
{
	#region variables
	public battleController battleController;

	public Image itemImage;
	public Sprite itemSprite;
	public Text itemNameText;
	public Text itemCountText;
	public int itemID;
	public int inventoryIndex;
	public int itemNumber;
	public string itemName;
	public int itemCount;
	public float useTimer;
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
		battleController.selectedItem = inventoryIndex;
		battleController.selectedItemNum = itemNumber;
	}
}