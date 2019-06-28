using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{

	private Vector3 startPosition;
	private Quaternion startRotation;

	// Start is called before the first frame update
	void Start()
	{
		startPosition = transform.position;
		startRotation = transform.rotation;
	}

	public void ResetObject()
	{
		transform.position = startPosition;
		transform.rotation = startRotation;
	}
}
