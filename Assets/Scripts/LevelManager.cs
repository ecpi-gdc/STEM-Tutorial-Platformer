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
		player = FindObjectOfType<PlayerController>();
		respawnObjects = FindObjectsOfType<ResetOnRespawn>();
		SetCoins(0);
		SetHealth(startingHealth);
	}

	public void AddCoins(int amount)
	{
		SetCoins(coinCount + amount);
		coinAudio.Play();
	}

	public void AddHealth(int amount)
	{
		SetHealth(healthCount + amount);
		heartAudio.Play();
	}

	public void DamagePlayer(int amount)
	{
		if (!player.IsInvincible())
		{
			SetHealth(healthCount - amount);
			player.TakeDamage();
		}
	}

	public void RespawnPlayer()
	{
		StartCoroutine(RespawnPlayerCoroutine());
	}

	private IEnumerator RespawnPlayerCoroutine()
	{
		deathAudio.Play();
		player.gameObject.SetActive(false);
		yield return new WaitForSeconds(respawnDelay);
		player.transform.position = player.respawnPoint.position;
		player.gameObject.SetActive(true);

		foreach (ResetOnRespawn reset in respawnObjects)
		{
			reset.ResetObject();
		}

		SetCoins(0);
		SetHealth(startingHealth);
	}

	private void SetCoins(int coins)
	{
		coinCount = coins;
		coinText.text = coinCount.ToString();
	}

	private void SetHealth(int health)
	{
		healthCount = health;
		healthText.text = healthCount.ToString();

		if (healthCount <= 0)
		{
			RespawnPlayer();
		}
	}
}
