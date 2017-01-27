using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class itemData : MonoBehaviour
{
	#region variables
	public int itemID;
	public int inventoryIndex;
	public GameObject itemPrefab;
	public string itemName;
	public Sprite itemImage;
	public string itemDescription;
	public int level;
	public bool isUnique;
	public bool canDiscard;
	public bool isEquipped;
	public Characters charaUsing;
	public int stackLimit;
	public int carryLimit;
	public int numInStack = 0;
	public int currentAmount = 0;
	public int totalOwned = 0;
	public int numEquipped = 0;
	public bool characterSpecific;
	public Characters[] charactersThatCanUse;
	public ItemType type;
	public bool usableInBattle;
	public bool isRanged;
	public float useTimer;
	public ArmourType armourType;
	public ArmourClass armourClass;
	public WeaponPlacement handUsed;
	public EquipmentSlot EquipSlot;
	public int offhandAccPenalty;
	public TargetArea area;
	public Elements[] elements;
	public int[] elementResistances;
	public int[] elementPercents;
	public Attributes[] attributesAffected;
	public int[] attributeAmount;
	public float[] attribEffectDuration;
	public Effects[] effects;
	public int[] effectResistances;
	public int[] effectPercent;
	public float[] effectDuration;
	public int damage;
	public int MPRecovered;
	public float attackSpeed;
	public int defense;
	public int cost;
	public int sellValue;
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