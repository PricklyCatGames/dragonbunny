using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour
{
	#region variables
	public int enemyID;
	public GameObject[] enemyParty;
	public itemData[] drops;
	public float[] dropRates;
	public int coinsDropped;
	public int EXPValue;
	public int skillPointValue;
	public bool isQuestTarget;
	public bool inBattle;

	public GameObject target;
	public bool usingSkill;
	public float actionTimer;
	public float baseActionDelay;
	public skillData[] availSkills;
	public float[] skillUseRate;
	public float[] skillTimers;

	public characterStatusController charaStatus;
	public battleController battleController;
	#endregion

	// Use this for initialization
	void Start()
	{
		actionTimer = 0f;
		int numSkills = charaStatus.availSkills.Count;

		availSkills = new skillData[numSkills];
		skillUseRate = new float[numSkills];
		skillTimers = new float[numSkills];

		availSkills = charaStatus.availSkills.ToArray();
		skillUseRate = charaStatus.skillUseRate.ToArray();
		skillTimers = charaStatus.skillTimers.ToArray();

		battleController = GameObject.Find("gameController").GetComponent<battleController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (inBattle)
		{
			for (int i = 0; i < skillTimers.Length; i++)
			{
				if (skillTimers[i] > 0)
				{
					skillTimers[i] -= Time.smoothDeltaTime;
				}
			}

			if (actionTimer > 0)
			{
				actionTimer -= Time.smoothDeltaTime;
			}
			else
			{
//				usingSkill = false;
				chooseAction();
			}
		}
		else
		{
			wander();
		}
	}

	public void chooseAction()
	{
		int selectedSkill = 0;
		float random = Random.Range(1, 100f);

		for (int i = 0; i < availSkills.Length; i++)
		{
			if (random <= skillUseRate[i])
			{
				selectedSkill = i;
				break;
			}
			else
			{
				random -= skillUseRate[i];
			}
		}
		useSkill(selectedSkill);
	}

	public void useSkill(int skillIndex)
	{
		if (skillIndex == 1)
		{
			charaStatus.useSkill(availSkills[1]);
		}
		else
		{
			if (availSkills[skillIndex].area == TargetArea.singleTarget ||
				availSkills[skillIndex].area == TargetArea.party)
			{
				 chooseTarget();
			}

	//		charaStatus.useSkill(target, skillIndex);
	//		usingSkill = true;
			if (availSkills[skillIndex].area == TargetArea.singleTarget)
			{
				target.GetComponent<characterStatusController>().calculateDamage(availSkills[skillIndex], this.gameObject);
			}
		}
		actionTimer = baseActionDelay + availSkills[skillIndex].castingTime;
		skillTimers[skillIndex] = availSkills[skillIndex].rechargeTime;
	}

	public void chooseTarget()
	{
//		GameObject target = null;

		if (!charaStatus.confused)
		{
			if (battleController.playerParty.Length > 1)
			{
				target = battleController.playerParty[0];
			}
			else
			{
				int random = Random.Range(0, battleController.playerParty.Length);
				target = battleController.playerParty[random];
			}
		}
		else
		{
			int numTargets = battleController.playerParty.Length + 
				battleController.enemyParty.Count;

			int random = Random.Range(0, numTargets);

			if (random < battleController.playerParty.Length)
			{
				target = battleController.playerParty[random];
			}
			else
			{
				random -= battleController.playerParty.Length;
				target = battleController.enemyParty[random];
			}
		}

//		return target;
	}

	public void wander()
	{
		
	}
}