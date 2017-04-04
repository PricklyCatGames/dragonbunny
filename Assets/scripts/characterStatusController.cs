using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class characterStatusController : MonoBehaviour
{
	#region variables
	public string charaName;
	public Canvas charaCanvas;
	public Text charaNameText;
	public GameObject dialoguePanel;
	public Text dialogueText;
	public Sprite charaSprite;
	public bool isEnemy;
	public bool isAvailable;
	public bool inParty;
	public bool isDead;
	public bool isControllable;
	public bool inFrontLine;
	public bool isRightHanded;
	public int maxHP;
	public int currentHP;
	public Slider HPSlider;
	public Text damageText;
	public float damageTextTimeout = 2f;
	public float damageTextTimer;
	public Color damageColor;
	public Color healColor;
	public int maxMP;
	public int currentMP;
	public int level;
	public int currentExp;
	public int expToNextLvl;
	public int skillPoints;
	public int maxSkillPoints;

	public int maxAttributeValue;
	public int size;
	public int strength;
	public int intelligence;
	public int dexterity;
	public int agility;
	public int endurance;
	public int luck;
	public int clarity; //magic focus
	public int zen;  //physical focus
	public int parry;
	public int block;

	public int baseAttack;
	public int equipAttack;
	public int baseDefense;
	public int equipDefense;
	public int currentAtk;
	public int currentDef;
	public int magicAtk;
	public int magicDef;
	public int baseAccuracy;
	public int accuracy;
	public int magicAcc;
	public int rangedAtk;
	public int rangedAcc;
	public int baseEva;
	public int evasion;
	public int[] battleSquares;
	public int attackSpeed;
	public float actionTimer;
	public float baseActionDelay;
	public float critHitRate;

	public bool poisoned;
	public bool silenced;
	public bool stunned;
	public bool asleep;
	public bool confused;
	public bool rabid;
	public bool zombie;
	public bool berserk;
	public bool charmed;
	public bool doomed;

	public bool regened;
	public bool MPRegened;

	public int defaultAction;
	public List<skillData> availSkills = new List<skillData>();
	public List<float> skillUseRate = new List<float>();
	public List<float> skillTimers = new List<float>();
	public equipManager equipment;
	#endregion

	// Use this for initialization
	void Start()
	{
		damageText.text = "";
		equipAttack = equipment.totalAttack;
		equipDefense = equipment.totalDefense;

		currentAtk = Mathf.RoundToInt((baseAttack + strength + equipAttack) * 0.8f);
		currentDef = Mathf.RoundToInt((baseDefense + endurance + equipDefense) * 0.8f);

		dialoguePanel.SetActive(false);
		HPSlider.maxValue = maxHP;
		HPSlider.value = currentHP;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (actionTimer > 0)
		{
			actionTimer -= Time.smoothDeltaTime;
		}

		for (int i = 0; i < skillTimers.Count; i++)
		{
			if (skillTimers[i] > 0)
			{
				skillTimers[i] -= Time.smoothDeltaTime;
			}
			else if (skillTimers[i] < 0)
			{
				skillTimers[i] = 0;
			}
		}

		if (damageTextTimer > 0)
		{
			damageTextTimer -= Time.smoothDeltaTime;
//			Debug.Log("dmgText: " + damageText + ", damageTextTimer: "
//				+ damageTextTimer);
		}
		else if (damageTextTimer < 0)
		{
			damageTextTimer = 0;
			damageText.text = "";
//			Debug.Log("dmgText: " + damageText + ", damageTextTimer: "
//				+ damageTextTimer);
		}
	}

	public void updateStats()
	{
		equipAttack = equipment.totalAttack;
		equipDefense = equipment.totalDefense;

		currentAtk = Mathf.RoundToInt((baseAttack + strength + equipAttack) * 0.8f);
		currentDef = Mathf.RoundToInt((baseDefense + endurance + equipDefense) * 0.8f);


		HPSlider.maxValue = maxHP;
		HPSlider.value = currentHP;
	}

	public void useSkill(skillData skill)
	{
		
	}
	
	public void calculateDamage(skillData skill, GameObject attacker)
	{
		int damage = skill.damage;
		characterStatusController atkrStatus = attacker.GetComponent<characterStatusController>();
		damageText.color = healColor;
//		Debug.Log("chara: " + charaName + ", skill: " + skill.skillName +
//			", base skill dmg: " + damage);

		if (damage > 0)
		{
			damage += atkrStatus.currentAtk - currentDef;
			damageText.color = damageColor;
		}

//		Debug.Log("skill dmg: " + damage + ", currHP: " + currentHP +
//			", newHP: " + (currentHP - damage));
		currentHP -= damage;
		damageText.text = Mathf.Abs(damage).ToString();
		damageTextTimer = damageTextTimeout;
//		Debug.Log("dmgText: " + damageText + ", damageTextTimer: "
//			+ damageTextTimer);

		if (currentHP > maxHP)
		{
			currentHP = maxHP;
		}

		if (currentHP < 0)
		{
			currentHP = 0;
			GameObject.Find("gameController").GetComponent<battleController>().killCharacter(this.gameObject);
		}
		HPSlider.value = currentHP;
	}

	public void calculateDamage(itemData item)
	{
		int damage = item.damage;
		int MPRecovered = item.MPRecovered;
//		Debug.Log("chara: " + charaName + ", item: " + item.itemName +
//			", base item dmg: " + damage);
		damageText.color = healColor;

		if (damage > 0)
		{
			damage -= currentDef;
			damageText.color = damageColor;
		}

//		Debug.Log("item dmg: " + damage + ", currHP: " + currentHP +
//			", newHP: " + (currentHP - damage));
		currentHP -= damage;
		currentMP += MPRecovered;
		damageText.text = Mathf.Abs(damage).ToString();
		damageTextTimer = damageTextTimeout;
//		Debug.Log("dmgText: " + damageText + ", damageTextTimer: "
//			+ damageTextTimer);

		if (currentHP > maxHP)
		{
			currentHP = maxHP;
		}

		if (currentMP > maxMP)
		{
			currentMP = maxMP;
		}

		if (currentHP < 0)
		{
			currentHP = 0;
			GameObject.Find("gameController").GetComponent<battleController>().killCharacter(this.gameObject);
		}
		HPSlider.value = currentHP;
	}

	public void levelUp()
	{
		maxHP = Mathf.RoundToInt(maxHP * 1.05f);
		currentHP = Mathf.RoundToInt(currentHP * 1.05f);
		maxMP = Mathf.RoundToInt(maxMP * 1.05f);
		currentMP = Mathf.RoundToInt(currentMP * 1.05f);
		expToNextLvl = Mathf.RoundToInt(expToNextLvl * 1.05f);
		
		/*
		if (level % 4 == 0)
//		 maxAttributeValue;
		strength++;
		intelligence++;
		dexterity++;
		agility++;
		endurance++;
		luck++;
		clarity++; //magic focus
		zen++;  //physical focus
		parry++;
		block++;

		if (level % 3 == 0)
		baseAttack++;
		baseDefense++;
		magicAtk++;
		magicDef++;
		baseAccuracy++;
		magicAcc++;
		rangedAtk++;
		rangedAcc++;
		baseEva++;
		critHitRate++;
		//*/

		updateStats();
	}
}