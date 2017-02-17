using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
	public class skill_Node
	{
		#region variables
		//public int skillPath { get; set; } // This will be which branch this is apart of
		public bool skillAccessible { get; set; } // Will determine if the player can click or not
		public bool skillObtained { get; set; } // Will determine if the player has access to this ability
		//public int skillID { get; set; }
		public string skillName { get; set; }
		public int skillCost { get; set; }
		public string skillDescription { get; set; }
		public bool hasDialogue { get; set; }
		public string dialogue { get; set; }
		public int requiredCharaLevel { get; set; }
		public string nextSkill { get; set; }
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Skills.skill_Nodes"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="access">If set to <c>true</c> access.</param>
		/// <param name="obtain">If set to <c>true</c> obtain.</param>
		/// <param name="name">Name.</param>
		/// <param name="cost">Cost.</param>
		/// <param name="descr">Descr.</param>
		/// <param name="allowDial">If set to <c>true</c> allow dialogue.</param>
		/// <param name="dial">Dialogue.</param>
		/// <param name="reqLvl">Req lvl.</param>
		/// <param name="nextS">Next skill.</param>
		public skill_Node(/*int path, int id,*/string name, bool access, bool obtain, int cost, string descr, bool allowDial, 
							string dial, int reqLvl, string nextS)
		{
			//skillPath = path;
			skillAccessible = access;
			skillObtained = obtain;
			//skillID = id;
			skillCost = cost;
			skillName = name;
			skillDescription = descr;
			hasDialogue = allowDial;
			dialogue = dial;
			requiredCharaLevel = reqLvl;
			nextSkill = nextS;
		}
	}
}

