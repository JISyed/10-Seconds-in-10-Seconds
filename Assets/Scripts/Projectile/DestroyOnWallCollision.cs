using UnityEngine;
using System.Collections;

public class DestroyOnWallCollision : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
