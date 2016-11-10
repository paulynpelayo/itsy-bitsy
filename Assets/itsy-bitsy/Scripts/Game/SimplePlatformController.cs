using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	//public Transform groundCheck;

	private bool grounded = true;
	private Rigidbody2D rigidbody;

	Vector2 startPos;

	// Use this for initialization
	void Awake () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
		startPos = this.transform.localPosition;

	}

	// Update is called once per frame
	void Update () 
	{
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		if (h * rigidbody.velocity.x < maxSpeed)
			rigidbody.AddForce(Vector2.right * h * moveForce);

		if (Mathf.Abs (rigidbody.velocity.x) > maxSpeed)
			rigidbody.velocity = new Vector2(Mathf.Sign (rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (jump)
		{
			rigidbody.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}