using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Credit to lhk for increment time counter
// http://answers.unity3d.com/questions/22252/incrementing-by-x-per-second.html

public class ControlGame : MonoBehaviour 
{
	public GameObject[] allPlayers;
	public GameObject[] hudAmmoAry;
	public GameObject[] hudBackAry;
	public GameObject[] hudHealthAry;
	public GameObject hudCountdown;
	public GameObject powerupHealthPrefab;
	public GameObject powerupRelicPrefab;
	public GameObject aiPlayer = null;
	public GameObject soundPlayerInvinci;
	public GameObject soundPlayerSpecials;
	
	public AudioClip sndSwap;
	
	public static float currTime = 10.0f;
	public const int MAX_HEALTH = 100;
	public const int START_AMMO = 100;
	public static bool someoneDeclaredSpecialMode = false;
	public static bool specialWasDefense = false;
	
	private static GameObject m_instance;
	private static double realTime = 0.0;
	private const int MAX_HUD_ARY = 2;
	private const double POW_SPWN_TIME = 8.0;
	
	private GUIText[] hudAmmoTextAry;
	private ManagePlayerState[] allPlayerManagers;
	private GUIText hudCountdownText;
	private float[] hudHealthMaxWidths;
	private Rect[] hudHealthCurrRect;
	private bool alreadySwapped = false;
	private Color colTimerNormal;
	private Color colTimerSpecial;
	private double powerupSpawnTimer = POW_SPWN_TIME;
	private ManageAIPlayer aiMangr = null;
	private PlayInvncibleSound sndPlyrInvi;
	private PlaySpecialModeSounds sndPlyrSpcl;
	
	
	
	// Use this for initialization
	void Start () 
	{
		// There can only be one controller
		if(m_instance == null)
		{
			m_instance = gameObject;
		}
		else
		{
			Destroy(gameObject);
		}
		
		// Init private arrays
		hudAmmoTextAry = new GUIText[MAX_HUD_ARY];
		allPlayerManagers = new ManagePlayerState[MAX_HUD_ARY];
		hudHealthMaxWidths = new float[MAX_HUD_ARY];
		hudHealthCurrRect = new Rect[MAX_HUD_ARY];
		for(int i = 0; i< MAX_HUD_ARY; i++)
		{
			hudAmmoTextAry[i] = hudAmmoAry[i].GetComponent<GUIText>();
			hudHealthCurrRect[i] = hudHealthAry[i].GetComponent<GUITexture>().pixelInset;
			hudHealthMaxWidths[i] = hudHealthAry[i].GetComponent<GUITexture>().pixelInset.width;
			allPlayerManagers[i] = allPlayers[i].GetComponent<ManagePlayerState>();
			allPlayerManagers[i].labeledBlack = i>0 ? true : false ;
			allPlayerManagers[i].labelChosen = true;
		}
		
		if(aiPlayer != null)
		{
			aiMangr = aiPlayer.GetComponent<ManageAIPlayer>();
		}
		
		sndPlyrInvi = soundPlayerInvinci.GetComponent<PlayInvncibleSound>();
		sndPlyrSpcl = soundPlayerSpecials.GetComponent<PlaySpecialModeSounds>();
		
		// Setup countdown HUD
		hudCountdownText = hudCountdown.GetComponent<GUIText>();
		hudCountdownText.text = currTime.ToString();
		
		colTimerNormal = hudCountdownText.color;
		colTimerSpecial = new Color(71.0f/255.0f, 245.0f/255.0f, 255.0f/255.0f);
		
		
		// This is causing null reference errors in ManagePlayerState.SwapState()
		
		/*
		float swapAtStartDeterminer = Random.Range(-1.0f, 1.0f);
		if(swapAtStartDeterminer > 0.0f)
		{
			SwapPlayerRoles();
		}
		*/
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		IncrementTime();
		UpdateAmmoHUD();
		UpdateCountdownHUD();
		UpdateHealthHUD();
		
		if(someoneDeclaredSpecialMode)
		{
			someoneDeclaredSpecialMode = false;
			realTime = 0.0;
			currTime = 10.0f;
			hudCountdownText.text = currTime.ToString();
			hudCountdownText.color = colTimerSpecial;
			if(specialWasDefense)
			{
				sndPlyrInvi.StartSound();
			}
			sndPlyrSpcl.PlayStartSound();
		}
	}
	
	// For drawing gizmos
	void OnDrawGizmos()	{}
	
	private void IncrementTime()
	{
		realTime += (double) Time.deltaTime;
		if(realTime > 1.0)
		{
			realTime -= 1.0;
			currTime--;
			if(currTime < 1.0f)
			{
				// To deactivate special mode
				if(hudCountdownText.color == colTimerSpecial)
				{
					hudCountdownText.color = colTimerNormal;
					for(int i = 0; i < MAX_HUD_ARY; i++)
					{
						allPlayerManagers[i].DeactivateSpecialMode();
					}
					sndPlyrInvi.StopSound();
					sndPlyrSpcl.PlayEndSound();
					specialWasDefense = false;
				}
				
				if(!alreadySwapped)
				{
					SwapPlayerRoles();
					alreadySwapped = true;
					audio.clip = sndSwap;
					audio.Play();
				}
			}
			if(currTime < 0.0f)
			{
				currTime = 10.0f;
				alreadySwapped = false;
			}
			
			powerupSpawnTimer--;
			if(powerupSpawnTimer < 0.0)
			{
				powerupSpawnTimer = POW_SPWN_TIME;
				float randPosX = Random.Range(-18.0f, 18.0f);
				float randPosZ = Random.Range(-18.0f, 18.0f);
				Vector3 randomPos = new Vector3(randPosX, 0.0f, randPosZ);
				float powerUpDeterminer = Random.Range(-1.0f, 1.0f);
				
				if(powerUpDeterminer > 0.0f)
				{
					Instantiate(powerupHealthPrefab, randomPos, powerupHealthPrefab.transform.rotation);
					if(aiMangr)
					{
						aiMangr.healthArrived = true;
					}
				}
				else if(powerUpDeterminer <= 0.0f)
				{
					Instantiate(powerupRelicPrefab, randomPos, powerupRelicPrefab.transform.rotation);
					if(aiMangr)
					{
						aiMangr.relicArrived = true;
					}
				}
			}
		}
	}
	
	private void UpdateAmmoHUD()
	{
		for(int i = 0; i < MAX_HUD_ARY; i++)
		{
			if(allPlayerManagers[i] != null)
			{
				hudAmmoTextAry[i].text = allPlayerManagers[i].GetAmmo().ToString();
				if(allPlayerManagers[i].offenseMode)
				{
					hudAmmoTextAry[i].color = Color.red;
				}
				else
				{
					hudAmmoTextAry[i].color = Color.green;
				}
			}
		}
	}
	
	private void UpdateCountdownHUD()
	{
		hudCountdownText.text = currTime.ToString();
	}
	
	private void UpdateHealthHUD()
	{
		for(int i = 0; i < MAX_HUD_ARY; i++)
		{
			if(allPlayerManagers[i] != null)
			{
				int currHealth = allPlayerManagers[i].GetHealth();
				float healthRatio = (float) currHealth / (float) MAX_HEALTH;
				float currBarWidth = healthRatio * hudHealthMaxWidths[i];
				hudHealthAry[i].GetComponent<ControlHealthBar>().SetBarWidth(currBarWidth);
			}
		}
	}
	
	private void SwapPlayerRoles()
	{
		for(int i = 0; i < MAX_HUD_ARY; i++)
		{
			allPlayerManagers[i].SwapState();
		}
	}
}
