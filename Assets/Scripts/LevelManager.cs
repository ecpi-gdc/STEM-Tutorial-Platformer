using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{

	public float respawnDelay;
	public Text coinText;

	private PlayerController player;
	private ResetOnRespawn[] respawnObjects;
	private int coinCount;

	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<PlayerController>();
		respawnObjects = FindObjectsOfType<ResetOnRespawn>();
		SetCoins(0);
	}

	public void AddCoins(int amount)
	{
		SetCoins(coinCount + amount);
	}

	public void RespawnPlayer()
	{
		StartCoroutine(RespawnPlayerCoroutine());
	}

	private IEnumerator RespawnPlayerCoroutine()
	{
		player.gameObject.SetActive(false);
		yield return new WaitForSeconds(respawnDelay);
		player.transform.position = player.respawnPoint.position;
		player.gameObject.SetActive(true);

		foreach (var reset in respawnObjects)
		{
			reset.ResetObject();
		}

		SetCoins(0);
	}

	private void SetCoins(int coins)
	{
		coinCount = coins;
		coinText.text = coinCount.ToString();
	}
}
