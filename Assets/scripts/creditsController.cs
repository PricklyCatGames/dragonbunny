﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class creditsController : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void closeCredits()
	{
		SceneManager.LoadScene(0);
	}
}