using UnityEngine;
using System.Collections;
using Skills;

public class characterSkillManager : MonoBehaviour {

	#region variables
	public string name = "";
	public int mpPoints = 0;
	public int charLevel = 0;
	public float exp = 0.0f;
	public skill_Main mainSkill;
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void levelUp()
	{
		++charLevel;
		mainSkill.checkAllSkillLocks(this);
	}
}
