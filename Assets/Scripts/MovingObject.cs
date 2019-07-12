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
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

#region Prepared Code
	void Reset()
	{
		Collider2D c = GetComponentInChildren<Collider2D>();
		if (c != null)
		{
			moveTarget = c.transform;
		}

		startPoint = transform.Find("Start");
		endPoint = transform.Find("End");
	}
#endregion
}
