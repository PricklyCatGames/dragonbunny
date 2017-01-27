using UnityEngine;
using System.Collections;

public enum Seasons {Winter, Spring, Summer, Autumn};

public enum Characters {Ankalia, Bressa, Yakut, Masamba,
						Eb, chara6, chara7, Styx, UnicornFrog, Vendax};

public enum NPCType {town, shop, quest, teacher, other};

public enum questType {story, collection, kill, delivery, escort};

public enum ItemType {usable, quest, armour, weapon, keyItem, misc};

public enum ArmourType {head, chest, hands, legs,
						feet, accessory};

public enum ArmourClass {light, medium, heavy, special};

public enum WeaponPlacement {mainHand, offHand, either, twoHanded};

public enum EquipmentSlot {mainWpn, subWpn, head, chest, 
						hands, legs, feet, accessory};

public enum TargetArea {singleTarget, party, wholeArea};

public enum Elements {none, fire, lightning, wind, ice, 
						light, water, earth, dark};

public enum Attributes {strength, intelligence, dexterity, 
					agility, endurance, luck, clarity, zen,
					accuracy, parry, block};

public enum Effects {blind, confuse, paralyze, stun, poison,
					haste, slow, sleep, berserk, rabies, zombie, 
					charm, doom, regen, MPRegen};

public class dataContainer : MonoBehaviour
{
	#region variables
	public float jackassMeter;
	#endregion

	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}