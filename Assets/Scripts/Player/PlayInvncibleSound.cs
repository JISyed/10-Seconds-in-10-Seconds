using UnityEngine;
using System.Collections;

public class PlayInvncibleSound : MonoBehaviour 
{
	private bool wasSoundStarted = false;
	private bool isStopping = false;
	
	
	void Start ()
	{
		audio.loop = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(wasSoundStarted)
		{
			wasSoundStarted = false;
			audio.Play();
		}
		
		if(isStopping)
		{
			isStopping = false;
			wasSoundStarted = false;
			audio.Stop();
		}
	}
	
	public void StartSound()
	{
		wasSoundStarted = true;
	}
	
	public void StopSound()
	{
		isStopping = true;
	}
}
