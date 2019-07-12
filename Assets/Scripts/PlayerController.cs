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
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
	}

	public void TakeDamage()
	{
		
	}

	public bool IsInvincible()
	{
		return false;
	}

#region Prepared Code
	private const string PLATFORM_TAG = "MovingPlatform";
	private const string DEATHBOX_TAG = "DeathBox";
	private bool lastIsGrounded;
	private float groundCheckRadius = 0.05f;

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
