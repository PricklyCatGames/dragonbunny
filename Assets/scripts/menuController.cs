using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuController : MonoBehaviour
{
	#region variables
	public bool submenuOpen;

	public Text announceText;
	public Text alertText;
	public GameObject charaStatusMenu;
//	public GameObject statsPanel;
//	public GameObject skillChartButton;
//	public GameObject skillChart;
//	public GameObject partyButton;
	public GameObject partyMenu;
	public GameObject partyList;
	public GameObject partyListContent;
	public Image[] partyMemberImages;
	public GameObject partyPosPanel;
	public Image[] partyOrderImages;
	public Transform[] memberForwardPositions;
	public Transform[] memberBackPositions;
	public GameObject inventory;
	public GameObject calendar;
	public GameObject questLog;
	public GameObject journal;
	public GameObject addEntryButton;
	public GameObject map;
	public GameObject achievementList;
	public GameObject saveMenu;
	public GameObject optionsMenu;

	gameController gameController;
	inventoryController inventoryController;
	public inventoryMenuController inventoryMenuController;
	public characterMenuController characterMenuController;
	characterStatusController charaStatus;
	#endregion

	// Use this for initialization
	void Start()
	{
		gameController = GameObject.Find("gameController").GetComponent<gameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void openStatusMenu()
	{
		closeMenu();
		charaStatusMenu.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 60)menuDepth = " + gameController.menuDepth);

		characterMenuController.numCharas = gameController.availCharas.Count;
		characterMenuController.charaPreviews = gameController.availCharas.ToArray();
		characterMenuController.selectedChara = gameController.currentChara;
	}

	public void openPartyMenu()
	{
		closeMenu();
		partyMenu.SetActive(true);
		partyList.SetActive(false);
		partyPosPanel.SetActive(false);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 75)menuDepth = " + gameController.menuDepth);
	}

	public void openPartyList()
	{
		partyList.SetActive(true);
		partyPosPanel.SetActive(false);
		gameController.menuDepth = 3;

		for (int i = 0; i < gameController.numCharasInParty; i++)
		{
//			partyMemberPos[i].sprite = 
//				gameController.currentParty[i].GetComponent<characterStatusController>().charaSprite;
		}
	}

	public void openPartyPosPanel()
	{
		partyList.SetActive(false);
		partyPosPanel.SetActive(true);
		gameController.menuDepth = 3;
	}

	public void openInventory()
	{
		closeMenu();
		inventory.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 102)menuDepth = " + gameController.menuDepth);
	}

	public void openCalendar()
	{
		closeMenu();
		calendar.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 111)menuDepth = " + gameController.menuDepth);
	}

	public void openQuestLog()
	{
		closeMenu();
		questLog.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 120)menuDepth = " + gameController.menuDepth);
	}

	public void openJournal()
	{
		closeMenu();
		journal.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 129)menuDepth = " + gameController.menuDepth);
	}

	public void openMap()
	{
		closeMenu();
		map.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 138)menuDepth = " + gameController.menuDepth);
	}

	public void openAchievementList()
	{
		closeMenu();
		achievementList.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 147)menuDepth = " + gameController.menuDepth);
	}

	public void saveGame()
	{
		PlayerPrefs.Save();
	}

	public void openOptionsMenu()
	{
		closeMenu();
		optionsMenu.SetActive(true);
		submenuOpen = true;
		gameController.menuDepth = 2;
//		Debug.Log("(menuController 161)menuDepth = " + gameController.menuDepth);
	}

	public void closeMenu()
	{
		announceText.text = "";
		alertText.text = "";
		if (charaStatusMenu.activeInHierarchy)
		{
			characterMenuController.reset();
		}
		charaStatusMenu.SetActive(false);
		partyMenu.SetActive(false);
		partyPosPanel.SetActive(false);
		inventory.SetActive(false);
		if (calendar.activeInHierarchy)
		{
			calendar.GetComponent<calendarController>().reset();
		}
		calendar.SetActive(false);
		questLog.SetActive(false);
		journal.SetActive(false);
		addEntryButton.SetActive(false);
		map.SetActive(false);
		achievementList.SetActive(false);
		optionsMenu.SetActive(false);
		submenuOpen = false;
		gameController.menuDepth = 1;
//		Debug.Log("(menuController 188)menuDepth = " + gameController.menuDepth);
	}

	public void closeMenu(int depth)
	{
		announceText.text = "";
		alertText.text = "";

		switch (depth)
		{
			case 2:
			{
//				if (charaStatusMenu.activeInHierarchy)
//				{
//					characterMenuController.reset();
//				}
//				charaStatusMenu.SetActive(false);
//				partyMenu.SetActive(false);
//				inventory.SetActive(false);
//				if (calendar.activeInHierarchy)
//				{
//					calendar.GetComponent<calendarController>().reset();
//				}
//				calendar.SetActive(false);
//				questLog.SetActive(false);
//				journal.SetActive(false);
//				map.SetActive(false);
//				achievementList.SetActive(false);
//				optionsMenu.SetActive(false);
				closeMenu();
				gameController.menuDepth = 1;
//				Debug.Log("(menuController 218)menuDepth = " + gameController.menuDepth);
				break;
			}
			case 3:
			{
				addEntryButton.SetActive(false);
				partyList.SetActive(false);
				partyPosPanel.SetActive(false);
				characterMenuController.reset();
				inventoryMenuController.closeMenu();
				gameController.menuDepth = 2;
//				Debug.Log("(menuController 230)menuDepth = " + gameController.menuDepth);
				break;
			}
			case 4:
			{
				gameController.menuDepth = 3;
//				Debug.Log("menuDepth = " + gameController.menuDepth);
				break;
			}
			case 5:
			{
				gameController.menuDepth = 4;
				break;
			}
			case 6:
			{
				gameController.menuDepth = 5;
				break;
			}
		}
	}

	public void exitGame()
	{
		Input.ResetInputAxes();
		SceneManager.LoadScene(0);
		Destroy(GameObject.FindGameObjectWithTag("data"));
	}

//	public void quitGame()
//	{
//		Input.ResetInputAxes();
//		PlayerPrefs.Save();
//		Application.Quit();
//	}
}