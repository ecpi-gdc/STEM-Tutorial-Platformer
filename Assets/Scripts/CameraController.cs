using UnityEngine;

public class CameraController : MonoBehaviour
{

	private Transform player;

	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<PlayerController>().transform;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = player.position.x;
		transform.position = pos;
	}
}
