using UnityEngine;
using System.Collections;

public class ManageSpiritState : MonoBehaviour 
{
	private int spiritIndex = 0;
	private Vector3 angleDisplacement = Vector3.zero;
	private bool indexSet = false;
	private bool angleSet = false;
	[HideInInspector] public Transform playerTransform;
	public Material matSpiritNormal;
	public Material matSpiritSeconded;
	public Material matSpiritSpecial;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Keep spirit at certain angle and position from player
		transform.position = playerTransform.position + angleDisplacement * 2.0f;
	}
	
	public void GlowNormally()
	{
		gameObject.renderer.material = matSpiritNormal;
	}
	
	public void GlowAfterSeconding()
	{
		gameObject.renderer.material = matSpiritSeconded;
	}
	
	public void GlowInSpecialMode()
	{
		gameObject.renderer.material = matSpiritSpecial;
	}
	
	// ------ Accessors --------- //
	
	public int GetSpiritIndex()
	{
		return spiritIndex;
	}
	
	public void SetSpiritIndex(int newIdx)
	{
		if(!indexSet)
		{
			spiritIndex = newIdx;
			indexSet = true;
		}
		else
		{
			print("ManageSpiritState: Index already set!");
		}
	}
	
	public Vector3 GetAngleDisp()
	{
		return angleDisplacement;
	}
	
	public void SetAngleDisp(Vector3 newAngleDisp)
	{
		if(!angleSet)
		{
			angleDisplacement = newAngleDisp;
			angleSet = true;
		}
		else
		{
			print("ManageSpiritState: Angle already set!");
		}
	}
}
