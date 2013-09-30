using UnityEngine;
using System.Collections;

public class DefinePowerup : MonoBehaviour 
{
	public enum ePowerupType
	{
		Health,
		Relic
	}
	
	public ePowerupType thePowerupType = ePowerupType.Health;
	public GameObject soundDropper;
	public AudioClip powerupSound;
	
	// Use this for initialization
	void Start () 
	{
		DropSound sndDpr = soundDropper.GetComponent<DropSound>();
		sndDpr.soundToPlay = powerupSound;
	}
	
	
	public ePowerupType GetTypeOfPowerup()
	{
		return thePowerupType;
	}
	
	public void PlayPowerupSound()
	{
		Instantiate(soundDropper);
	}
	
		
	
}
