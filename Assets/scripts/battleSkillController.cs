using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class battleSkillController : MonoBehaviour, ISelectHandler
{
	#region variables
	public battleController battleController;

	public Image skillImage;
	public Sprite skillSprite;
	public Text skillNameText;
	public Text skillTimerText;
	public int skillID;
	public int skillIndex;
	public int MPCost;
	public string skillName;
	public float rechargeTime;
	public float castingTime;
	public float skillTimer;
	#endregion

	// Use this for initialization
	void Start()
	{
		skillTimerText.text = skillTimer.ToString("F");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (skillTimer > 0)
		{
			skillTimer -= Time.smoothDeltaTime;
			skillTimerText.text = skillTimer.ToString("F");
		}
		else if (skillTimer < 0)
		{
			skillTimer = 0;
			skillTimerText.text = skillTimer.ToString("F");
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
//		battleController.skillIndex = skillIndex;
		battleController.selectedSkill = skillIndex;
	}
}