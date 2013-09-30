using UnityEngine;
using System.Collections;

public class HandleMenu : MonoBehaviour 
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
			Application.LoadLevel("scn_tutorial");
		}
	}
	
	// For gizmos
	void OnDrawGizmos() {}
}
