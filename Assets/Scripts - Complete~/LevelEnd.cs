using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour
{

#region Prepared Code
	public Transform endPoint;
	public Animator endAnim;
	public Animator flagAnim;
	public float walkDelay = 2;

	private PlayerController player;

	// Start is called before the first frame update
	void Start()
	{
		endAnim.SetBool("open", false);
		player = FindObjectOfType<PlayerController>();
	}

	public void Trigger()
	{
		StartCoroutine(LevelEndCoroutine());
	}

	private IEnumerator LevelEndCoroutine()
	{

		flagAnim.SetBool("active", true);

		player.UpdateAnimator(0, true);
		player.enabled = false;
		player.transform.localScale = new Vector2(1, 1);

		Rigidbody2D body = player.GetComponent<Rigidbody2D>();
		body.velocity = new Vector2(0, body.velocity.y);

		yield return new WaitForSeconds(walkDelay);

		body.velocity = new Vector2(player.moveSpeed, body.velocity.y);

		player.UpdateAnimator(1, true);
		endAnim.SetBool("open", true);

		Transform t = player.transform;
		while (t.position.x < endPoint.position.x)
		{
			yield return null;
		}

		body.velocity = new Vector2(0, 0);
		player.gameObject.SetActive(false);

	}
#endregion

}
