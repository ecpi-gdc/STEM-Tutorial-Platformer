using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public Transform respawnPoint;
	public float moveSpeed;
	public float jumpSpeed;
	public bool canMove;

	private LevelManager levelManager;
	private Rigidbody2D body;

	// Start is called before the first frame update
	void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
		body = GetComponent<Rigidbody2D>();
		canMove = true;
	}

	// Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		bool jumping = Input.GetButtonDown("Jump");
		bool isGrounded = CheckGrounded();

		float speed = moveSpeed;
		Vector2 newVelocity = new Vector2();
		Vector2 newScale = new Vector2(1, 1);

		if (horizontal < 0)
		{
			newVelocity.x = -speed;
			newScale.x = -1;
		}
		else if (horizontal > 0)
		{
			newVelocity.x = speed;
			newScale.x = 1;
		}
		else
		{
			newVelocity.x = 0;
			newScale.x = transform.localScale.x;
		}

		if (jumping && isGrounded)
		{
			newVelocity.y = jumpSpeed;
		}
		else
		{
			newVelocity.y = body.velocity.y;
		}

		body.velocity = newVelocity;
		transform.localScale = newScale;

		UpdateAnimator();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag(DEATHBOX_TAG))
		{
			levelManager.RespawnPlayer();
		}

		Coin coin = other.GetComponent<Coin>();
		if (coin != null)
		{
			levelManager.AddCoins(coin.coinAmount);
			coin.gameObject.SetActive(false);
		}

		Heart heart = other.GetComponent<Heart>();
		if (heart != null)
		{
			levelManager.AddHealth(heart.healthAmount);
			heart.gameObject.SetActive(false);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag(PLATFORM_TAG))
		{
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other) 
	{
		if (other.gameObject.CompareTag(PLATFORM_TAG)) 
		{
			transform.parent = null;
		}
	}

#region Prepared Code
	private const string PLATFORM_TAG = "MovingPlatform";
	private const string DEATHBOX_TAG = "DeathBox";
	private bool lastIsGrounded;
	private float groundCheckRadius = 0.05f;

	private bool CheckGrounded() {
		Bounds b = GetComponent<Collider2D>().bounds;
		lastIsGrounded = Physics2D.OverlapCircle(new Vector2(b.center.x, b.min.y), groundCheckRadius, LayerMask.GetMask("Ground"));
		return lastIsGrounded;
	}

	void OnDrawGizmosSelected() {
		Bounds b = GetComponent<Collider2D>().bounds;
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(new Vector3(b.center.x, b.min.y, 0f), groundCheckRadius);
	}

	private void UpdateAnimator() {
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("speed", Mathf.Abs(body.velocity.x));
		anim.SetBool("grounded", lastIsGrounded);
	}
#endregion
}
