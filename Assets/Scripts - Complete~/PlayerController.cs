using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public Transform respawnPoint;
	public float moveSpeed;
	public float jumpSpeed;
	public float knockbackLength;
	public float invincibilityLength;
	public Vector2 knockbackForce;
	public bool canMove;
	public AudioSource jumpAudio;
	public AudioSource hitAudio;
	public AudioSource checkpointAudio;

	private LevelManager levelManager;
	private Rigidbody2D body;
	private float knockbackTime;
	private float invincibilityTime;
	private StompEnemy stomper;

	// Start is called before the first frame update
	void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
		body = GetComponent<Rigidbody2D>();
		stomper = GetComponentInChildren<StompEnemy>();
		canMove = true;
	}

	// Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		bool jumping = Input.GetButtonDown("Jump");
		bool isGrounded = CheckGrounded();

		float speed = moveSpeed;
		Vector2 newVelocity = new Vector2(0, body.velocity.y);
		float direction = transform.localScale.x;

		if (canMove && knockbackTime <= 0)
		{
			if (horizontal < 0)
			{
				newVelocity.x = -speed;
				direction = -1;
			}
			else if (horizontal > 0)
			{
				newVelocity.x = speed;
				direction = 1;
			}

			if (jumping && isGrounded)
			{
				newVelocity.y = jumpSpeed;
				jumpAudio.Play();
			}
		}

		if (knockbackTime > 0)
		{
			knockbackTime = knockbackTime - Time.deltaTime;
			
			if (direction > 0)
			{
				newVelocity.x = -knockbackForce.x;
			}
			else
			{
				newVelocity.x = knockbackForce.x;
			}

			newVelocity.y = knockbackForce.y;

		}

		if (invincibilityTime > 0)
		{
			invincibilityTime = invincibilityTime - Time.deltaTime;
		}

		stomper.gameObject.SetActive(newVelocity.y < 0);
		
		body.velocity = newVelocity;
		transform.localScale = new Vector2(direction, 1);

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

		HurtPlayer hurt = other.GetComponent<HurtPlayer>();
		if (hurt != null && hurt.IsActive)
		{
			levelManager.DamagePlayer(hurt.damageAmount);
		}

		Checkpoint check = other.GetComponent<Checkpoint>();
		if (check != null)
		{
			check.Trigger();
			respawnPoint = check.transform;
			checkpointAudio.Play();
		}

		LevelEnd end = other.GetComponent<LevelEnd>();
		if (end != null)
		{
			end.Trigger();
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

	public void TakeDamage()
	{
		knockbackTime = knockbackLength;
		invincibilityTime = invincibilityLength;
		hitAudio.Play();
	}

	public bool IsInvincible()
	{
		return invincibilityTime > 0;
	}

#region Prepared Code
	private const string PLATFORM_TAG = "MovingPlatform";
	private const string DEATHBOX_TAG = "DeathBox";
	private bool lastIsGrounded;
	private float groundCheckRadius = 0.05f;

	private bool CheckGrounded()
	{
		Bounds b = GetComponent<Collider2D>().bounds;
		lastIsGrounded = Physics2D.OverlapCircle(new Vector2(b.center.x, b.min.y), groundCheckRadius, LayerMask.GetMask("Ground"));
		return lastIsGrounded;
	}

	void OnDrawGizmosSelected()
	{
		Bounds b = GetComponent<Collider2D>().bounds;
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(new Vector3(b.center.x, b.min.y, 0f), groundCheckRadius);
	}

	private void UpdateAnimator()
	{
		UpdateAnimator(Mathf.Abs(body.velocity.x), lastIsGrounded);
	}

	public void UpdateAnimator(float speed, bool grounded)
	{
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("speed", speed);
		anim.SetBool("grounded", grounded);
	}
#endregion
}
