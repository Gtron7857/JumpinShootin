using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	
	[SerializeField] // There are a few tags like this that effect how Unity serializes fields.  This one lets private fields be edited in the inspector.
	private float moveForce = 400f;
	[SerializeField] // Exposing properties in the inspector lets you change the values of said properties for individual instances.
	private float airMoveMod = .05f;
	[SerializeField] // In situations where you assign a value to a property exposed in the inspector, the value is treated like a default
	private float maxSpeed = 7f;
	[SerializeField]
	private float jumpForce = 700f;
	[SerializeField]
	private float velocityDecay = .8f;

	// Since this is just a control variable, there's no reason to expose it in the inspector
	private bool isJumping = false;
	private bool isGrounded = false;
	public Transform groundCheck;
	private Rigidbody2D body;

	// Use this for initialization
	void Start ()
	{
		groundCheck = transform.Find("Ground");
		body = this.GetComponent<Rigidbody2D> ();
	}
  
	// Update is called once per frame
	void Update ()
	{
		isGrounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));  

		if (Input.GetButtonDown ("Jump") && isGrounded)
			isJumping = true;
	}

	// Fizicks
	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");
		float moveMod = 1f;
		float maxMod = 1f;

		if (!isGrounded)
			moveMod *= airMoveMod;

		if (h * body.velocity.x < maxSpeed)
			body.AddForce (Vector2.right * h * moveForce * moveMod);

		if (Mathf.Abs (body.velocity.x) > maxSpeed)
			body.velocity = new Vector2 (Mathf.Sign (body.velocity.x) * maxSpeed, body.velocity.y);

		if (isGrounded && h == 0) {
			body.velocity = body.velocity * velocityDecay;
		}

		if (isJumping) {
			body.AddForce (new Vector2 (0f, jumpForce));
			isJumping = false;
		}

		body.MoveRotation (0);
	}

	// Colliders behave and call different methods depending on whether or not they're set as triggers.
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col.name); // Print things to the console with Debug.Log().  Debug also has Warning() and Error() methods.
		switch(col.name){
		case "KillTrigger": Application.LoadLevel(Application.loadedLevel); break;
		}
	}
}
