using UnityEngine;
using System.Collections;

public class skillData : MonoBehaviour
{
	#region variables
	public int skillID;
	public string skillName;
	public string skillDescription;
	public bool hasDialogue;
	public string dialogue;
	public int requiredCharaLevel;
	public bool isPhysical;
	public bool isRanged;
	public bool usesWeapon;
	public bool canInterrupt;
	public int MPCost;
	public TargetArea area;
	public float castingTime;
	public float rechargeTime;
	public int numHits;
	public float duration;
//	public int numElements;
	public Elements[] skillElements;
	public int[] elementDamagePercent;
	public int[] elementResistance;
//	public int numAttributes;
	public Attributes[] attribAffected;
	public int[] attribAmount;
//	public int numEffects;
	public Effects[] effects;
	public int[] effectPercent;
	public int[] effectResistance;
	public int damage;
	public int defense;
	public int successRate;
	#endregion

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}