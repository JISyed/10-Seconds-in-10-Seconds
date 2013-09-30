using UnityEngine;
using System.Collections;

public class HandleTutorial : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.JoystickButton7))
		{
			Application.LoadLevel("scn_play");
		}
	}
	
	void OnDrawGizmos() {}
}
