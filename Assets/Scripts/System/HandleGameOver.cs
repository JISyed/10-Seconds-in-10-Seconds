using UnityEngine;
using System.Collections;

public class HandleGameOver : MonoBehaviour 
{
	public GameObject blackWinLabel;
	public GameObject whiteWinLabel;
	
	private Vector3 blackWinLabelPos;
	private Vector3 whiteWinLabelPos;
	
	// Use this for initialization
	void Start () 
	{
		blackWinLabelPos = new Vector3(0.39f, 0.5f, 0.0f);
		whiteWinLabelPos = new Vector3(0.38f, 0.5f, 0.0f);
		
		GameObject persistantCtrlr = GameObject.FindWithTag("PerstCtrlr");
		if(persistantCtrlr)
		{
			PersistantCtrlr pCtrlr = persistantCtrlr.GetComponent<PersistantCtrlr>();
			
			if(pCtrlr.lWhitePlayerWon)
			{
				Instantiate(whiteWinLabel, whiteWinLabelPos, Quaternion.identity);
			}
			else
			{
				Instantiate(blackWinLabel, blackWinLabelPos, Quaternion.identity);
			}
		}
		else
		{
			Debug.LogError("HandleGameOver: No Persistent Controller Found!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.JoystickButton7))
		{
			Application.LoadLevel("scn_menu");
		}
	}
	
	void OnDrawGizmos() {}
}
