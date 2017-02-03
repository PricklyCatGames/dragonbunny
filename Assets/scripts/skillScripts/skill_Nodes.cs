using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class skill_Nodes
	{
		#region variables
		public int skillPath { get; set; } // This will be which branch this is apart of
		public bool skillAccessible { get; set; } // Will determine if the player can click or not
		public bool skillObtained { get; set; } // Will determine if the player has access to this ability
		public int skillID { get; set; }
		public string skillName { get; set; }
		public int skillCost { get; set; }
		public string skillDescription { get; set; }
		public bool hasDialogue { get; set; }
		public string dialogue { get; set; }
		public int requiredCharaLevel { get; set; }

		// Needed for displaying
		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; } // Is Z needed if we will display on Canvas?

		// Needed to connect the nodes together
		public float connect_from { get; set; }
		public float connect_to { get; set; }
		#endregion

		public skill_Nodes(int path, int id, bool access, bool obtain, string name, int cost, string descr, bool allowDial, 
							string dial, int reqLvl, float aX, float aY, float cFrom, float cTo)
		{
			skillPath = path;
			skillAccessible = access;
			skillObtained = obtain;
			skillID = id;
			skillCost = cost;
			skillName = name;
			skillDescription = descr;
			hasDialogue = allowDial;
			dialogue = dial;
			requiredCharaLevel = reqLvl;

			x = aX;
			y = aY;
			connect_from = cFrom;
			connect_to = cTo;
		}
	}
}

