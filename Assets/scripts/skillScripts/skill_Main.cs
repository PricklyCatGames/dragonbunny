using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class skill_Main : MonoBehaviour
	{
		#region variables
		public Dictionary<string, skill_Trails> skillTrails = new Dictionary<string, skill_Trails>();
		#endregion

		void Awake()
		{
			initializeSkillTrails();
		}

		public void initializeSkillTrails()
		{
			List<skill_Nodes> ankaliaSkills = new List<skill_Nodes>();
			#region Create Nodes
			var luminesce = new skill_Nodes(0, 0, true, false, "Luminesce", 0, "No idea...", false, "", 0, 0.0f, 0.0f, 0.0f, 0.0f);
			var light = new skill_Nodes(0, 1, false, false, "Light", 5, "No idea...", false, "", 0, 0.0f, 58.8f, 0.0f, 0.0f);
			var lux = new skill_Nodes(0, 2, false, false, "Lux", 10, "No idea...", false, "", 0, 0.0f, 114.4f, 0.0f, 0.0f);
			#endregion

			#region Adding Nodes
			ankaliaSkills.Add(luminesce);
			ankaliaSkills.Add(light);
			ankaliaSkills.Add(lux);
			#endregion

			#region Create Node GOs
			foreach(var skillNode in ankaliaSkills)
			{
				var node = Instantiate(Resources.Load("SkillNode_", typeof(GameObject))) as GameObject;
				node.transform.SetParent(GameObject.Find("Skill_Map").transform, false);
				node.name = node.name.Remove(node.name.IndexOf("("));
				node.name += skillNode.skillName;

				node.GetComponent<skill_NodeGameObject>().path = skillNode.skillPath;
				node.GetComponent<skill_NodeGameObject>().skillID = skillNode.skillID;
				node.GetComponentInChildren<Text>().text = skillNode.skillName;
				node.GetComponentInChildren<Button>().interactable = skillNode.skillAccessible;

				node.GetComponent<RectTransform>().anchoredPosition = new Vector2(skillNode.x, skillNode.y);

				node.GetComponentInChildren<Button>().onClick.AddListener(() => purchaseSkillNode(node));;
			}
			#endregion

			#region Create Trails
			var ankaliaSkillTrail = new skill_Trails("ankalia", ankaliaSkills);
			#endregion

			this.skillTrails.Add("ankalia", ankaliaSkillTrail);
		}

		public void purchaseSkillNode(GameObject skillNode)
		{
			var skillNodeGO = skillNode.GetComponent<skill_NodeGameObject>();
			var charName = "ankalia"; // TODO Figure out how to get this from within game
			var charTrail = skillTrails[charName];
			var currSkillNode = charTrail.skillTrails[skillNodeGO.path][skillNodeGO.skillID];
			Debug.Log("Selected " + currSkillNode.skillName);

			charTrail.obtainSkillNode(currSkillNode);
			charTrail.unlockNextSkillNode(currSkillNode);
		}
	}
}

