using UnityEngine;

public class StompEnemy : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(ENEMY_TAG))
		{
			other.gameObject.SetActive(false);
		}
	}

	#region Prepared Code
	private const string ENEMY_TAG = "Enemy";
	#endregion
}
