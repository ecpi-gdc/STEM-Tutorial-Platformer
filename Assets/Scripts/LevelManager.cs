using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

	public float respawnDelay;

	private PlayerController player;
	private ResetOnRespawn[] respawnObjects;

	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<PlayerController>();
		respawnObjects = FindObjectsOfType<ResetOnRespawn>();
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
	}
}
