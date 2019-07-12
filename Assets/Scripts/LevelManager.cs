using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{

	public int startingHealth;
	public float respawnDelay;
	public Text coinText;
	public Text healthText;
	public AudioSource coinAudio;
	public AudioSource heartAudio;
	public AudioSource deathAudio;

	private PlayerController player;
	private ResetOnRespawn[] respawnObjects;
	private int coinCount;
	private int healthCount;

	// Start is called before the first frame update
	void Start()
	{

	}

	public void AddCoins(int amount)
	{
		
	}

	public void AddHealth(int amount)
	{
		
	}

	private void SetCoins(int coins)
	{
		
	}

	private void SetHealth(int health)
	{
		
	}

	public void DamagePlayer(int amount)
	{
		
	}

	public void RespawnPlayer()
	{
		
	}

	private IEnumerator RespawnPlayerCoroutine()
	{
		yield break;
	}
}
