using UnityEngine;

public class MovingObject : MonoBehaviour
{

	public Transform moveTarget;
	public Transform startPoint;
	public Transform endPoint;
	public float moveSpeed;

	private Vector3 currentTarget;

	// Start is called before the first frame update
	void Start()
	{
		currentTarget = endPoint.position;
	}

	// Update is called once per frame
	void Update()
	{
		moveTarget.position = Vector3.MoveTowards(moveTarget.position, currentTarget, moveSpeed * Time.deltaTime);

		if (moveTarget.position == startPoint.position)
		{
			currentTarget = endPoint.position;
		}
		else if (moveTarget.position == endPoint.position)
		{
			currentTarget = startPoint.position;
		}
	}
}
