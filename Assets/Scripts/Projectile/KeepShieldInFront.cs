using UnityEngine;
using System.Collections;

public class KeepShieldInFront : MonoBehaviour 
{
	[HideInInspector] public Transform playerTransform;
	public AudioClip shieldSound;
	
	// Use this for initialization
	void Start ()
	{
		gameObject.AddComponent<AudioSource>();
		audio.clip = shieldSound;
		if(shieldSound != null)
		{
			audio.Play();
		}
		else
		{
			Debug.LogWarning(gameObject.name + ": shieldSound not assigned!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(playerTransform)
		{
			transform.position = playerTransform.position;
			transform.rotation = playerTransform.rotation;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Bullet"))
		{
			Destroy(other.gameObject);
		}
		if(other.gameObject.CompareTag("BulletSpecial"))
		{
			Destroy(other.gameObject);
		}
	}
}
