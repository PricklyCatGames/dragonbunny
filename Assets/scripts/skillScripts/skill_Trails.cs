using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class skill_Trails
	{
		#region variables
		public string character { get; set; }
		public List<skill_Nodes> skillNodes { get; set; }
		public Dictionary<int, Dictionary<int, skill_Nodes>> skillTrails = new Dictionary<int, Dictionary<int, skill_Nodes>>();
		#endregion

		public skill_Trails(string aChar, List<skill_Nodes> char_nodes)
		{
			character = aChar;
			skillNodes = char_nodes;

			int temp = 0;
			var temp_dict = new Dictionary<int, skill_Nodes>();
			Debug.Log("START");
			foreach(var cn in char_nodes)
			{
				if(temp != cn.skillPath)
				{
					skillTrails.Add(cn.skillPath, temp_dict);
					++temp;
					temp_dict = new Dictionary<int, skill_Nodes>();
				}
				Debug.Log(cn.skillID + " " + cn.skillName);
				temp_dict.Add(cn.skillID, cn);
			}

			skillTrails.Add(temp, temp_dict);
		}

		public void obtainSkillNode(skill_Nodes currSkillNode)
		{
			var temp_dict = skillTrails[currSkillNode.skillPath];
			temp_dict[currSkillNode.skillID].skillObtained = true;
		}

		public void unlockNextSkillNode(skill_Nodes currSkillNode)
		{
			var temp_dict = skillTrails[currSkillNode.skillPath];
			if((currSkillNode.skillID + 1) < temp_dict.Count)
			{
				temp_dict[currSkillNode.skillID + 1].skillAccessible = true;
				var temp_go = GameObject.Find("SkillNode_" + temp_dict[currSkillNode.skillID + 1].skillName);
				temp_go.GetComponentInChildren<Button>().interactable = true;
			}
		}
	}
}

