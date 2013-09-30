using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour 
{
	public GameObject bulletPrefab;
	public GameObject shieldPrefab;
	public GameObject specialBulletPrefab;
	private ManagePlayerState mps;
	
	// Use this for initialization
	void Start () 
	{
		mps = gameObject.GetComponent<ManagePlayerState>();
		if(!mps)
		{
			print("ShootBullet: Couldn't get player state!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton5))
		{
			if(mps.offenseMode)
			{
				if(mps.GetAmmo() > 0)
				{
					GameObject newBullet;
					if(mps.isInSpecialMode())
					{
						newBullet = Instantiate(specialBulletPrefab, 
											    transform.position + transform.forward * 3.2f, 
							                    transform.rotation) as GameObject;
					}
					else
					{
						newBullet = Instantiate(bulletPrefab, 
											    transform.position + transform.forward * 1.6f, 
							                    transform.rotation) as GameObject;
					}
					newBullet.transform.forward = transform.forward;
					mps.AmmoDec();
					mps.bulletsFired++;
					if(mps.bulletsFired > 50)
					{
						mps.bulletsFired = 0;
						//mps.LoseApproval();
					}
				}
				
			}
			else
			{
				if(! (mps.isInSpecialMode()) )
				{
					GameObject newShield = Instantiate(shieldPrefab, 
								                       transform.position, 
								                       transform.rotation) as GameObject;
					newShield.GetComponent<KeepShieldInFront>().playerTransform = transform;
					mps.AmmoInc();
				}	
			}
			
		}
	}
}
