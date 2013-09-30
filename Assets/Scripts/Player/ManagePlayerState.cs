using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Credit to ivkoni for vector rotation
// http://forum.unity3d.com/threads/33215-Vector-rotation

public class ManagePlayerState : MonoBehaviour 
{
	public GameObject spiritPrefab;
	public GameObject preLabel;
	public bool offenseMode = true;
	public Material matOffense;
	public Material matDefense;
	public Material matOffenseSpecial;
	public Material matDefenseSpecial;
	public Material matWhiteL;
	public Material matBlackL;
	public bool labeledBlack = false;
	public bool labelChosen = false;
	public bool isAI = false;
	[HideInInspector] public int bulletsFired = 0;
	
	private List<GameObject> listOfSpirits;
	private int health = ControlGame.MAX_HEALTH;
	private int approvalPoints = 0;
	private int ammo = 0;
	private bool specialModeSet = false;
	private bool inSpecialMode = false;
	private ManageAIPlayer aiMangr;
	
	// Use this for initialization
	void Start () 
	{
		if(offenseMode)
		{
			ammo = ControlGame.START_AMMO;
			gameObject.renderer.material = matOffense;
		}
		else
		{
			ammo = 0;
			gameObject.renderer.material = matDefense;
		}
		
		listOfSpirits = new List<GameObject>();
		
		for(int i = 0; i < 10; i++)
		{
			GameObject newSpirit = Instantiate(spiritPrefab, 
											   transform.position, 
											   transform.rotation) as GameObject;
			listOfSpirits.Add(newSpirit);
			Quaternion newAngle = Quaternion.AngleAxis(36*i, Vector3.up);
			Vector3 newAngeVec = newAngle * Vector3.forward;
			newSpirit.transform.position = transform.position + newAngeVec * 2.0f;
			ManageSpiritState mss = newSpirit.GetComponent<ManageSpiritState>();
			mss.SetSpiritIndex(i);
			mss.SetAngleDisp(newAngeVec);
			mss.playerTransform = transform;
		}
		
		if(isAI)
		{
			aiMangr = gameObject.GetComponent<ManageAIPlayer>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Cheat for switching mode.
		//*
		if(Input.GetKeyUp(KeyCode.O))
		{
			SwapState();
		}
		// Cheating to decrease health
		if(Input.GetKeyUp(KeyCode.I))
		{
			HealthDec();
			
		}
		if(Input.GetKeyUp(KeyCode.U))
		{
			HealthInc();
		}
		//*/
		
		// Choose between the player black and player white
		if(labelChosen)
		{
			labelChosen = false;
			if(labeledBlack)
			{
				preLabel.renderer.material = matBlackL;
			}
			else
			{
				preLabel.renderer.material = matWhiteL;
			}
		}
		
		if(health == 0)
		{
			OnKill();
		}
		
		if(approvalPoints == 10)
		{
			if(!specialModeSet)
			{
				specialModeSet = true;
				InitiateSpecialMode();
			}
		}
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Bullet"))
		{
			Destroy(other.gameObject);
			
			if( !(!offenseMode && inSpecialMode) )
			{
				health -= 2;
				if(health < 0)
				{
					health = 0;
				}
			}
		}
		if(other.gameObject.CompareTag("BulletSpecial"))
		{
			Destroy(other.gameObject);
			
			if( !(!offenseMode && inSpecialMode) )
			{
				health -= 2;
				if(health < 0)
				{
					health = 0;
				}
			}
		}
		
		if(other.gameObject.CompareTag("Player"))
		{
			ManagePlayerState otherPSM = other.gameObject.GetComponent<ManagePlayerState>();
			if(otherPSM.isInSpecialMode())
			{
				health -= 20;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Powerup"))
		{
			DefinePowerup pow = other.gameObject.GetComponent<DefinePowerup>();
			DefinePowerup.ePowerupType pt = pow.GetTypeOfPowerup();
			if(pt == DefinePowerup.ePowerupType.Health)
			{
				health += 25;
				if(health > 100)
				{
					health = 100;
				}
				
				if(!inSpecialMode)
				{
					LoseApproval();
				}
				
				if(isAI)
				{
					aiMangr.gotPowerup = true;
				}
			}
			else if (pt == DefinePowerup.ePowerupType.Relic)
			{
				if(!inSpecialMode)
				{
					health -= 10;
					if(health < 0)
					{
						health = 0;
					}
					GainApproval();
				}
				
				if(isAI)
				{
					aiMangr.gotPowerup = true;
				}
			}
			pow.PlayPowerupSound();
			Destroy(other.gameObject);
		}
	}
	
	public void SwapState()	
	{
		offenseMode = !offenseMode;
		
		if(isAI)
		{
			aiMangr.SwapAIState();
		}
		
		// Starting offense mode
		if(offenseMode)
		{
			gameObject.renderer.material = matOffense;
		}
		
		// Starting defense mode
		else
		{
			gameObject.renderer.material = matDefense;
			ammo = 0;
		}
	}
	
	public void OnKill()
	{
		GameObject persistantCtrlr = GameObject.FindWithTag("PerstCtrlr");
		PersistantCtrlr pCtrlr = persistantCtrlr.GetComponent<PersistantCtrlr>();
		
		if(!pCtrlr)
		{
			Debug.LogWarning("ManagePlayerState: Persistant Controller Does Not Exist");
		}
		
		if(labeledBlack)
		{
			pCtrlr.lWhitePlayerWon = true;
		}
		else
		{
			pCtrlr.lWhitePlayerWon = false;
		}
		
		Application.LoadLevel("scn_gameOver");
	}
	
	public void InitiateSpecialMode()
	{
		inSpecialMode = true;
		ammo = 100;
		if(offenseMode)
		{
			gameObject.renderer.material = matOffenseSpecial;
		}
		else
		{
			gameObject.renderer.material = matDefenseSpecial;
			ControlGame.specialWasDefense = true;
		}
		foreach(GameObject s in listOfSpirits)
		{
			s.GetComponent<ManageSpiritState>().GlowInSpecialMode();
		}
		ControlGame.someoneDeclaredSpecialMode = true;
	}
	
	public void DeactivateSpecialMode()
	{
		inSpecialMode = false;
		approvalPoints = 0;
		if(offenseMode)
		{
			gameObject.renderer.material = matOffense;
		}
		else
		{
			gameObject.renderer.material = matDefense;
		}
		foreach(GameObject s in listOfSpirits)
		{
			s.GetComponent<ManageSpiritState>().GlowNormally();
		}
	}
	
	// ----- Accessors ----- //
	
	public int GetHealth()
	{
		return health;
	}
	
	public void HealthDec()
	{
		health--;
		if(health < 0)
		{
			health = 0;
		}
	}
	
	public void HealthInc()
	{
		health++;
		if(health > ControlGame.MAX_HEALTH)
		{
			health = ControlGame.MAX_HEALTH;
		}
	}
	
	public int GetApprovalPoints()
	{
		return approvalPoints;
	}
	
	public void GainApproval()
	{
		approvalPoints++;
		if(approvalPoints > 10)
		{
			approvalPoints = 10;
		}
		
		int i = 1;
		foreach(GameObject s in listOfSpirits)
		{
			if(i <= approvalPoints)
			{
				s.GetComponent<ManageSpiritState>().GlowAfterSeconding();
			}
			i++;
		}
	}
	
	public void LoseApproval()
	{
		approvalPoints--;
		if(approvalPoints < 0)
		{
			approvalPoints = 0;
		}
		
		int i = 1;
		listOfSpirits.Reverse();
		foreach(GameObject s in listOfSpirits)
		{
			if(i <= 10 - approvalPoints)
			{
				s.GetComponent<ManageSpiritState>().GlowNormally();
			}
			i++;
		}
		listOfSpirits.Reverse();
	}
	
	public int GetAmmo()
	{
		return ammo;
	}
	
	public void AmmoInc()
	{
		ammo++;
	}
	
	public void AmmoDec()
	{
		ammo--;
		if(ammo < 0)
		{
			ammo = 0;
		}
	}
	
	public bool isInSpecialMode()
	{
		return inSpecialMode;
	}
}
