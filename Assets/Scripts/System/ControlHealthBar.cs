using UnityEngine;
using System.Collections;

public class ControlHealthBar : MonoBehaviour 
{
	private GUITexture theHealthBar;
	
	// Use this for initialization
	void Start () 
	{
		theHealthBar = gameObject.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void SetBarWidth(float newWidth)
	{
		Rect hbr = theHealthBar.pixelInset;
		hbr.Set(hbr.x, hbr.y, newWidth, hbr.height);
		theHealthBar.pixelInset = hbr;
	}
}

