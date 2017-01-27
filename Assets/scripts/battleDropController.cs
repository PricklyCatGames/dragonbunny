using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class battleDropController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
	#region variables
	public battleController battleController;

//	public GameObject dropItemCone;
	public Image dropItemImage;
	public Sprite dropItemSprite;
	public Text dropItemNameText;
	public int dropItemID;
	public int dropItemIndex;
	public string dropItemName;
//	public int dropItemMaxHP;
//	public int dropItemHP;
	public bool isSelected;
	public Color selectedColor;
	public Color unselectedColor;
	#endregion

	// Use this for initialization
	void Start()
	{
		dropItemNameText.color = unselectedColor;
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void OnSelect(BaseEventData eventData)
	{
		isSelected = !isSelected;

		if (isSelected)
		{
			dropItemNameText.color = selectedColor;
//			battleController.dropsClaimed.Add(dropItemID);
		}
		else
		{
			dropItemNameText.color = unselectedColor;
//			battleController.dropsClaimed.Remove(dropItemID);
		}

//		battleController.selectedDropItem = dropItemIndex;
//		dropItemCone.SetActive(true);
	}

	public void OnDeselect (BaseEventData data)
	{
//		dropItemCone.SetActive(false);
	}
}