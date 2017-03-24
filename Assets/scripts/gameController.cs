using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour
{
	#region variables
	public GameObject[] objects;
	public regionData[] regions;
//	public GameObject[] charaPrefabs;
//	public GameObject[] availCharas;
//	[SerializeField]
	public List<GameObject> availCharas = new List<GameObject>();
	public int maxPartySize = 3;
	public GameObject[] currentParty;
	public int numCharasInParty;
	public GameObject currentCharacter;
	public Canvas currCharaCanvas;
	public Transform spawnPoint;

	public GameObject menu;
	public GameObject storeMenu;
	public int menuDepth;

	public Text announceText;
	public Text alertText;
	public GameObject addEntryButton;

//	dataContainer data;
	masterListController masterList;
	public menuController menuController;
	public shopMenuController shopMenuController;
	public calendarController calendarController;
	timeController timeController;
//	weatherController weatherController;
	public playerController playerController;
	cameraController cameraController;
	inventoryController inventoryController;
	public inventoryMenuController inventoryMenuController;
//	mapController mapController;
	battleController battleController;
//	objectController objectController;
//	itemController itemController;
//	enemyController enemyController;

	public int currentChara;
	public int currentRegion;

	bool menuButton;
	bool cancelButton;

	public bool paused;
	public bool battleMode;
	#endregion

	// Use this for initialization
	void Start()
	{
//		data = GameObject.FindGameObjectWithTag("data").GetComponent<dataContainer>();
		masterList = GetComponentInChildren<masterListController>();
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();
//		weatherController = GetComponent<weatherController>();
		cameraController = GameObject.Find("Camera").GetComponent<cameraController>();
//		mapController = GetComponent<mapController>();
		battleController = GetComponent<battleController>();
		currentParty = new GameObject[maxPartySize];
		loadCharas();
//		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
//		inventoryController = GetComponent<inventoryController>();
//		loadInventory();
//
//		objectController = GameObject.Find("objectController").GetComponent<objectController>();
//		itemController = GameObject.Find("itemController").GetComponent<itemController>();
//		enemyController = GameObject.Find("enemyController").GetComponent<enemyController>();
	
		menu.SetActive(false);
		playerController.isControllable = true;
		cameraController.isControllable = true;
		menuDepth = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		menuButton = Input.GetButtonDown("Menu");
		cancelButton = Input.GetButtonDown("Cancel");
		if (!battleMode)
		{
			if (playerController.isTalking)
			{
				if (cancelButton)
				{
//					BroadcastMessage("silence");
//					playerController.isTalking = false;
				}
			}
			else
			{
				if (menuButton)
				{
					openMenu();
					playerController.isControllable = false;
					cameraController.isControllable = false;
//					Debug.Log("(gameController 106)menuDepth = " + menuDepth);
//					menuDepth = 1;
				}

				if (cancelButton)
				{
//					Debug.Log("(gameController 112)menuDepth = " + menuDepth);
					switch (menuDepth)
					{
						case 1:
						{
							closeMenu();
							closeStoreMenu();
							playerController.isControllable = true;
							cameraController.isControllable = true;
							break;
						}
						case 2:
						{
							menuController.closeMenu(menuDepth);
							shopMenuController.cancelTransaction();
							menuDepth = 1;
							break;
						}
						case 3:
						{
							menuController.closeMenu(menuDepth);
							calendarController.closeNotes();
							shopMenuController.closeEquipPanel();
							shopMenuController.dismissAlert();
							inventoryMenuController.closeCharaSelectMenu();
							inventoryMenuController.cancelSelectAmount();
							menuDepth = 2;
							break;
						}
						case 4:
						{
							menuController.closeMenu(menuDepth);
							calendarController.cancelNote();
							calendarController.cancelReminder();
							shopMenuController.dismissAlert();
							menuDepth = 3;
							break;
						}
						case 5:
						{
							menuController.closeMenu(menuDepth);
							menuDepth = 4;
							break;
						}
					}

//					if (menuController.submenuOpen)
//					{
//						menuController.closeMenu();
//					}
//					else
//					{
//						closeMenu();
//						closeStoreMenu();
//						playerController.isControllable = true;
//						cameraController.isControllable = true;
//					}
				}
			}
		}

//		if (menuDepth < 0)
//		{
//			Debug.Log("menuDepth = " + menuDepth);
//			menuDepth = 0;
//		}
	}

	public void loadCharas()
	{
		int i = 0;
		foreach (GameObject chara in masterList.characterList)
		{
			if (chara.GetComponent<characterStatusController>().isAvailable)
			{
				currentCharacter = Instantiate(chara) as GameObject;
				currentCharacter.transform.SetParent(this.transform.Find("availCharas"), false);
				currentCharacter.GetComponent<playerController>().enabled = false;
				currentCharacter.GetComponent<Collider>().enabled = false;
				availCharas.Add(currentCharacter);

				if (chara.GetComponent<characterStatusController>().inParty)
				{
					currentParty[i] = currentCharacter;
					i++;
				}
			}
		}
		numCharasInParty = i;

		currentCharacter = availCharas[currentChara];
		currentCharacter.transform.position = spawnPoint.position;
		currentCharacter.GetComponent<playerController>().enabled = true;
		currentCharacter.GetComponent<Collider>().enabled = true;
		playerController = currentCharacter.GetComponent<playerController>();
		currCharaCanvas = currentCharacter.GetComponent<characterStatusController>().charaCanvas;
		cameraController.followTarget = currentCharacter.transform;
		cameraController.normalPos = currentCharacter.transform.Find("normalCameraPos").transform;
	}

	public void loadEvent(int eventID)
	{
		
	}

	public void startBattle(GameObject enemy)
	{
		createSavePoint();
		battleMode = true;
		playerController.isControllable = false;
		cameraController.isControllable = false;
		timeController.currentTimeScale = timeController.battleTimeScale;
		battleController.setupBattle(enemy);
	}

	public void createSavePoint()
	{
		
	}

	public void endBattle()
	{
		if (paused)
		{
			PauseGame();
		}
		battleMode = false;
		playerController.isControllable = true;
		cameraController.isControllable = true;
		timeController.currentTimeScale = timeController.normalTimeScale;
	}

	public void loseBattle()
	{
		PauseGame();
	}

	public void closeStoreMenu()
	{
		timeController.currentTimeScale = timeController.normalTimeScale;
		currCharaCanvas.enabled = true;
//		storeMenu.SetActive(false);
	}

	public void openMenu()
	{
		menu.SetActive(true);
		timeController.currentTimeScale = timeController.menuTimeScale;
		currCharaCanvas.enabled = false;
		menuDepth = 1;
//		Debug.Log("(gameController 258)menuDepth = " + menuDepth);
	}

	public void closeMenu()
	{
		menuController.closeMenu();
		menu.SetActive(false);
		timeController.currentTimeScale = timeController.normalTimeScale;
		currCharaCanvas.enabled = true;
		menuDepth = 0;
//		Debug.Log("(gameController 267)menuDepth = " + menuDepth);
	}

	public void PauseGame()
	{
		if (!paused)
		{
			paused = true;
			Input.ResetInputAxes();
			timeController.currentTimeScale = timeController.pausedTimeScale;
			playerController.isControllable = false;
			cameraController.isControllable = false;
//			timeScale = Time.timeScale;
//			Time.timeScale = 0.000000001f;
//			Cursor.visible = true;
//			resumeButton.SetActive(true);
//			restartButton.SetActive(true);
//			exitButton.SetActive(true);
		}
		else
		{
			paused = false;
			Input.ResetInputAxes();
			timeController.currentTimeScale = timeController.normalTimeScale;
			playerController.isControllable = true;
			cameraController.isControllable = true;
//			audio.clip = buttonBack;
//			audio.Play();
//			Time.timeScale = timeScale;
//			resumeButton.SetActive(false);
//			restartButton.SetActive(false);
//			exitButton.SetActive(false);
//			Cursor.visible = false;
//			AudioListener.pause = false;
		}
	}

	public void restart()
	{
		SceneManager.LoadScene(1);
		Input.ResetInputAxes();
	}
}