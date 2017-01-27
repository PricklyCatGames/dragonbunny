using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class battleTargetController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
	#region variables
	public battleController battleController;

	public GameObject targetCone;
//	public Image targetImage;
//	public Sprite targetSprite;
	public Text targetNameText;
	public int targetIndex;
	public string targetName;
	public int targetMaxHP;
	public int targetHP;
	public bool isEnemy;
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
		battleController.selectedTarget = targetIndex;
		targetCone.SetActive(true);
	}

	public void OnDeselect (BaseEventData data)
	{
		targetCone.SetActive(false);
	}
}