using UnityEngine;
using System.Collections;

public class PlaySpecialModeSounds : MonoBehaviour 
{
	public AudioClip sndSpecialStart;
	public AudioClip sndSpecialEnd;
	
	private bool isToPlayStart = false;
	private bool isToPlayEnd = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(isToPlayStart)
		{
			isToPlayStart = false;
			audio.clip = sndSpecialStart;
			audio.Play();
		}
		
		if(isToPlayEnd)
		{
			isToPlayEnd = false;
			audio.clip = sndSpecialEnd;
			audio.Play();
		}
	}
	
	public void PlayStartSound()
	{
		isToPlayStart = true;
	}
	
	public void PlayEndSound()
	{
		isToPlayEnd = true;
	}
}
