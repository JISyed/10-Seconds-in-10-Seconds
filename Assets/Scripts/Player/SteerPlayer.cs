using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class SteerPlayer : MonoBehaviour 
{
	public float thrustSpeed;
	public float thrustDrag;
	public float rotateSpeed;
	public float rotateDrag;
	
	private ManagePlayerState mps;
	
	// Use this for initialization
	void Start () 
	{
		mps = gameObject.GetComponent<ManagePlayerState>();
		if(!mps)
		{
			print("SteerPlayer: Couldn't get player state!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(thrustDrag < 0)
			thrustDrag = 0;
		if(rotateDrag < 0)
			rotateDrag = 0;
		
		rigidbody.drag = thrustDrag;
		rigidbody.angularDrag = rotateDrag;
		
		// Thrust forward or backward
		if(XCI.GetAxis(XboxAxis.LeftStickY) > 0.2f || Input.GetKey(KeyCode.W))
		{
			rigidbody.AddRelativeForce(Vector3.forward * thrustSpeed * Time.deltaTime);
		}
		if(XCI.GetAxis(XboxAxis.LeftStickY) < -0.2f || Input.GetKey(KeyCode.S))
		{
			rigidbody.AddRelativeForce(Vector3.forward * -thrustSpeed * 0.75f * Time.deltaTime);
		}
		
		// Turn clockwise or counterclockwise
		if(XCI.GetAxis(XboxAxis.RightStickX) > 0.2f || Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody.AddRelativeTorque(Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if(XCI.GetAxis(XboxAxis.RightStickX) < -0.2f || Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody.AddRelativeTorque(Vector3.up * -rotateSpeed * Time.deltaTime);
		}
	}
}
