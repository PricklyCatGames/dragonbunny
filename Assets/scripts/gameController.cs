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
	public Transform spawnPoint;

	public GameObject menu;
	public GameObject storeMenu;

	public Text announceText;
	public Text alertText;
	public GameObject addEntryButton;

//	dataContainer data;
	public menuController menuController;
	masterListController masterList;
	timeController timeController;
	weatherController weatherController;
	public playerController playerController;
	cameraController cameraController;
	inventoryController inventoryController;
	mapController mapController;
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
		weatherController = GetComponent<weatherController>();
		cameraController = GameObject.Find("Camera").GetComponent<cameraController>();
		mapController = GetComponent<mapController>();
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
				}

				if (cancelButton)
				{
					if (menuController.submenuOpen)
					{
						menuController.closeMenu();
					}
					else
					{
						closeMenu();
						closeStoreMenu();
						playerController.isControllable = true;
						cameraController.isControllable = true;
					}
				}
			}
		}
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
//		storeMenu.SetActive(false);
	}

	public void openMenu()
	{
		menu.SetActive(true);
		timeController.currentTimeScale = timeController.menuTimeScale;
	}

	public void closeMenu()
	{
		menu.SetActive(false);
		timeController.currentTimeScale = timeController.normalTimeScale;
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