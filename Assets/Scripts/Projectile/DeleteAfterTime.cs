using UnityEngine;
using System.Collections;

public class DeleteAfterTime : MonoBehaviour 
{
	public float countdown;
	
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, countdown);
	}
}
