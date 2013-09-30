using UnityEngine;
using System.Collections;

public class MoveProjectile : MonoBehaviour 
{
	public float bulletSpeed;
	public AudioClip bulletSound;
	
	// Use this for initialization
	void Start ()
	{
		gameObject.AddComponent<AudioSource>();
		audio.clip = bulletSound;
		if(bulletSound != null)
		{
			audio.Play();
		}
		else
		{
			Debug.LogWarning(gameObject.name + ": bulletSound not assigned!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
	}
}
