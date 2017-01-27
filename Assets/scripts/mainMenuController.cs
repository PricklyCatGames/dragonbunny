using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class mainMenuController : MonoBehaviour
{
	#region variables
//	Text titleText;
	GameObject playButton;
	GameObject loadButton;
	GameObject playMenu;
	GameObject optionsButton;
	GameObject optionsMenu;
	GameObject backButton;
	GameObject exitButton;

	Scene main;

	int numSavedGames;
	#endregion

	// Use this for initialization
	void Start()
	{
		numSavedGames = PlayerPrefs.GetInt("numSavedGames", 0);

//		titleText = GameObject.Find("TitleText").GetComponent<Text>();
		playButton = GameObject.Find("PlayButton") as GameObject;
		loadButton = GameObject.Find("LoadButton") as GameObject;
		loadButton.SetActive(false);
		playMenu = GameObject.Find("PlayMenu") as GameObject;
		playMenu.SetActive(false);
		optionsButton = GameObject.Find("OptionsButton") as GameObject;
		optionsMenu = GameObject.Find("OptionsMenu") as GameObject;
		optionsMenu.SetActive(false);
		backButton = GameObject.Find("BackButton") as GameObject;
		backButton.SetActive(false);
		exitButton = GameObject.Find("ExitButton") as GameObject;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OpenPlayMenu()
	{
		playButton.SetActive(false);
		optionsButton.SetActive(false);
		playMenu.SetActive(true);
		backButton.SetActive(true);
		exitButton.SetActive(false);

		if (numSavedGames > 0)
		{
			loadButton.SetActive(true);
		}

//		SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
//		main = SceneManager.GetSceneByName("main");
	}

	public void ClosePlayMenu()
	{
		playButton.SetActive(true);
		optionsButton.SetActive(true);
		playMenu.SetActive(false);
		loadButton.SetActive(false);
		backButton.SetActive(false);
		exitButton.SetActive(true);
	}

	public void StartNewGame()
	{
//		SceneManager.SetActiveScene(main);
//		SceneManager.UnloadScene(0);
		SceneManager.LoadScene(1);
		Input.ResetInputAxes();
	}

	public void LoadGame()
	{
		
	}

	public void OpenOptionsMenu()
	{
//		titleText.enabled = false;

		playButton.SetActive(false);
		optionsButton.SetActive(false);
		optionsMenu.SetActive(true);
		backButton.SetActive(true);
		exitButton.SetActive(false);
	}

	public void CloseOptionsMenu()
	{
//		titleText.enabled = true;

		playButton.SetActive(true);
		optionsButton.SetActive(true);
		optionsMenu.SetActive(false);
		backButton.SetActive(false);
		exitButton.SetActive(true);
	}

	public void openCredits()
	{
		SceneManager.LoadScene(2);
	}

	public void exitGame()
	{
		Input.ResetInputAxes();
		PlayerPrefs.Save();
		Application.Quit();
	}
}