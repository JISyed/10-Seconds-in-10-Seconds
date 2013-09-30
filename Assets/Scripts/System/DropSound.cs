using UnityEngine;
using System.Collections;

public class DropSound : MonoBehaviour 
{
	public AudioClip soundToPlay;
	public float volumeRatio = 1.0f;
	
	// Use this for initialization
	void Start () 
	{
		audio.volume = Mathf.Clamp(volumeRatio , 0.0f, 1.0f);
		audio.clip = soundToPlay;
		if(audio.clip != null)
		{
			audio.Play();
		}
		else
		{
			Debug.LogWarning(gameObject.name + ": soundToPlay not assigned!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!audio.isPlaying)
		{
			Destroy(gameObject);
		}
	}
	
	void OnDrawGizmos() {}
}
