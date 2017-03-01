using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace Skills
{
	public class skill_Main : MonoBehaviour
	{
		#region Skill Node Organization
		public Dictionary<string, skill_Node> ankaliaSkillNodes = new Dictionary<string, skill_Node>();
		public Dictionary<string, skill_Node> bressaSkillNodes = new Dictionary<string, skill_Node>();
		public Dictionary<string, skill_Node> yakutSkillNodes = new Dictionary<string, skill_Node>();

		public Dictionary<string, Dictionary<string, skill_Node>> allSkillNodes = new Dictionary<string, Dictionary<string, skill_Node>>();
		#endregion

		#region Character Switch
		public CanvasGroup[] characterNodeTrees = new CanvasGroup[3];

		public Text characterName;
		public string[] characterNames = {"Ankalia", "Bressa", "Yakut"};

		public int currIndex = 0;
		#endregion

		void Awake()
		{
			// Check to see if we have a saved skill set already. If not, then intialize.
			initializeSkillTrails();
		}

		public void initializeSkillTrails()
		{
			#region Create Nodes [Ankalia]
			var luminesce_ankalia = new skill_Node("Luminesce", true, true, 0, "No idea...", false, "", 0, "Light");
			var light = new skill_Node("Light", true, false, 5, "No idea...", false, "", 0, "Lux");
			var lux = new skill_Node("Lux", false, false, 10, "No idea...", false, "", 0, "");

			var flame_ankalia = new skill_Node("Flame", true, true, 0, "No idea...", false, "", 0, "Fire");
			var smolder_ankalia = new skill_Node("Smolder", true, false, 5, "No idea...", false, "", 0, "");

			var hp_5_ankalia = new skill_Node("HP+5%", true, false, 5, "No idea...", false, "", 0, "MP+5%");
			var mp_5 = new skill_Node("MP+5%", false, false, 7, "No idea...", false, "", 0, "Unkown_0");
			var status_unknown_0 = new skill_Node("Unkown_0", false, false, 10, "No idea...", false, "", 0, "");

			var agil_2 = new skill_Node("Agility+2", true, false, 6, "No idea...", false, "", 0, "Zen+1");
			var zen_1 = new skill_Node("Zen+1", false, false, 9, "No idea...", false, "", 0, "Unkown_1");
			var status_unknown_1 = new skill_Node("Unkown_1", false, false, 10, "No idea...", false, "", 0, "");
			#endregion

			#region Create Nodes [Bressa]
			var glow = new skill_Node("Glow", true, true, 0, "No idea...", false, "", 0, "Luminesce");
			var luminesce_bressa = new skill_Node("Luminesce", true, false, 3, "No idea...", false, "", 0, "");

			var first_aid = new skill_Node("FirstAid", true, true, 0, "No idea...", false, "", 0, "Mend");
			var mend = new skill_Node("Mend", true, false, 7, "No idea...", false, "", 0, "");

			var flame_bressa = new skill_Node("Flame", true, true, 0, "No idea...", false, "", 0, "Fire");
			var smolder_bressa = new skill_Node("Smolder", true, false, 5, "No idea...", false, "", 0, "");

			var hp_5_bressa = new skill_Node("HP+5%", true, false, 5, "No idea...", false, "", 0, "Staves1");
			var staves_1 = new skill_Node("Staves1", false, false, 5, "No idea...", false, "", 0, "");
			#endregion

			#region Create Nodes [Yakut]
			var defender = new skill_Node("Defender", true, true, 0, "No idea...", false, "", 0, "Protector");
			var protector = new skill_Node("Protector", true, false, 5, "No idea...", false, "", 0, "Guardian");
			var guardian = new skill_Node("Guardian", false, false, 10, "No idea...", false, "", 0, "");

			var static_yakut = new skill_Node("Static", true, true, 0, "No idea...", false, "", 0, "Shock");
			var shock = new skill_Node("Shock", true, false, 7, "No idea...", false, "", 0, "");

			var hp_5_yakut = new skill_Node("HP+5%", true, false, 5, "No idea...", false, "", 0, "STR+3");
			var str_3 = new skill_Node("STR+3", false, false, 7, "No idea...", false, "", 0, "");

			var sparring_practice = new skill_Node("SparringPractice", true, false, 3, "No idea...", false, "", 0, "");
			#endregion

			#region Adding Nodes [Ankalia]
			ankaliaSkillNodes.Add("Luminesce", luminesce_ankalia);
			ankaliaSkillNodes.Add("Light", light);
			ankaliaSkillNodes.Add("Lux", lux);

			ankaliaSkillNodes.Add("Flame", flame_ankalia);
			ankaliaSkillNodes.Add("Smolder", smolder_ankalia);

			ankaliaSkillNodes.Add("HP+5%", hp_5_ankalia);
			ankaliaSkillNodes.Add("MP+5%", mp_5);
			ankaliaSkillNodes.Add("Unkown_0", status_unknown_0);

			ankaliaSkillNodes.Add("Agility+2", agil_2);
			ankaliaSkillNodes.Add("Zen+1", zen_1);
			ankaliaSkillNodes.Add("Unkown_1", status_unknown_1);
			#endregion

			#region Adding Nodes [Bressa]
			bressaSkillNodes.Add("Glow", glow);
			bressaSkillNodes.Add("Luminesce", luminesce_bressa);

			bressaSkillNodes.Add("FirstAid", first_aid);
			bressaSkillNodes.Add("Mend", mend);

			bressaSkillNodes.Add("Flame", flame_bressa);
			bressaSkillNodes.Add("Smolder", smolder_bressa);

			bressaSkillNodes.Add("HP+5%", hp_5_bressa);
			bressaSkillNodes.Add("Staves1", staves_1);
			#endregion

			#region Adding Nodes [Yakut]
			yakutSkillNodes.Add("Defender", defender);
			yakutSkillNodes.Add("Protector", protector);
			yakutSkillNodes.Add("Guardian", guardian);

			yakutSkillNodes.Add("Static", static_yakut);
			yakutSkillNodes.Add("Shock", shock);

			yakutSkillNodes.Add("HP+5%", hp_5_yakut);
			yakutSkillNodes.Add("STR+3", str_3);

			yakutSkillNodes.Add("SparringPractice", sparring_practice);
			#endregion

			#region Adding All Nodes [* Characters]
			allSkillNodes.Add("Ankalia", ankaliaSkillNodes);
			allSkillNodes.Add("Bressa", bressaSkillNodes);
			allSkillNodes.Add("Yakut", yakutSkillNodes);
			#endregion
		}

		/// <summary>
		/// Purchases the skill node and unlocks the next one.
		/// </summary>
		/// <param name="skillNode">Skill node GameObject.</param>
		public void purchaseSkillNode(GameObject skillNode)
		{			
			var skillName = skillNode.name;
			var charName = skillNode.transform.parent.parent.name.Split('_')[1];
			var charSkillManager = GameObject.Find(charName + "SkillManager").GetComponent<characterSkillManager>();

			print(charName + " " + skillName);
			var currSkillNode = allSkillNodes[charName][skillName];

			if(charSkillManager.mpPoints >= currSkillNode.skillCost)
			{
				if(currSkillNode.nextSkill != "")
				{
					this.checkSingleSkillLock(charSkillManager, currSkillNode);
				}

				// Perform alterations to character and perform necc. checks
				charSkillManager.mpPoints -= currSkillNode.skillCost;
				currSkillNode.skillObtained = true;

			}
		}

		/// <summary>
		/// Checks the single skill lock.
		/// </summary>
		/// <param name="charSkillManager">Char skill manager.</param>
		/// <param name="skillNode">Skill node.</param>
		public void checkSingleSkillLock(characterSkillManager charSkillManager, skill_Node currSkillNode)
		{
			var nextSkill_Button = GameObject.Find(currSkillNode.nextSkill).GetComponent<Button>();
			var nextSkillNode = allSkillNodes[charSkillManager.name][nextSkill_Button.name];

			if(charSkillManager.charLevel >= nextSkillNode.requiredCharaLevel)
			{
				nextSkill_Button.interactable = true;
				nextSkillNode.skillAccessible = true;
			}
			else
			{
				// Possibly have some different color or text that displays on a tooltip that
				// specifies the the character level is not reachable
			}
		}

		/// <summary>
		/// Checks all of the skills locked and unlocks it if the current character level is high enough.
		/// </summary>
		/// <param name="charLevel">Char level.</param>
		/// <param name="charName">Char name.</param>
		public void checkAllSkillLocks(characterSkillManager charSkillManager)
		{
			foreach(KeyValuePair<string, skill_Node> kv in allSkillNodes[charSkillManager.name])
			{
				if(!kv.Value.skillObtained && kv.Value.skillAccessible)
				{
					if(charSkillManager.charLevel >= kv.Value.requiredCharaLevel)
					{
						GameObject.Find(kv.Key).GetComponent<Button>().interactable = true;
					}
				}
			}
		}

		// Assigned to buttons in UI, right is +1, left is -1
		public void DisplayCharacterNodeTree(int number)
		{
			var prevNodeTree = characterNodeTrees[currIndex];
			currIndex += number;

			if(currIndex < 0)
			{
				currIndex += characterNames.Length;
			}
			else
			{
				currIndex = currIndex % characterNames.Length;
			}

			var currNodeTree = characterNodeTrees[currIndex];

			currNodeTree.alpha = 1;
			currNodeTree.blocksRaycasts = true;
			currNodeTree.interactable = true;

			prevNodeTree.alpha = 0;
			prevNodeTree.blocksRaycasts = false;
			prevNodeTree.interactable = false;

			characterName.text = characterNames[currIndex];
		}
	}
}

