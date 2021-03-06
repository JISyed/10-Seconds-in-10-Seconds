﻿using UnityEngine;
using System.Collections;

public class PersistantCtrlr : MonoBehaviour 
{
	[HideInInspector] public bool lWhitePlayerWon = false;
	private static bool alreadyExists = false;
	
	// Constructor
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Use this for initialization
	void Start () 
	{
		// There can only be one PersistantCtrlr
		if(alreadyExists)
		{
			Destroy(gameObject);
		}
		else
		{
			alreadyExists = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Quit upon pressing escape
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	// For Gizmos
	void OnDrawGizmos() {}
}
