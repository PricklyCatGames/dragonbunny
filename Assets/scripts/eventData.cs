using UnityEngine;
using System.Collections;

public class eventData : MonoBehaviour
{
	#region variables
//	public enum EventType {story = 0, recurring = 1, quest = 2, playerCaused = 3};
//	public enum Characters {Ankalia = 0, Bressa = 1, Yakut = 2, Masamba = 3,
//						Eb = 4, chara6 = 5, chara7 = 6, Vendax = 7};
	public int eventID = 0;
	public string eventName;
	public EventType type;
	public Vector3 location;
	public string[] dialogue;
	public bool hasJournalEntry;
	public string journalText;
	public bool hasAnnouncement;
	public string announcementText;
	public float announcementDuration;
	public bool hasQuest;
	public GameObject quest;
	public bool seasonal;
	public bool availAtNight;
	public bool eventSensitive;
	public int eventRequiredID;
	public bool requiresSpecificChara;
	public Characters[] charactersRequired;
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