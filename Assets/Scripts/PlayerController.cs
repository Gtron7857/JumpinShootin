using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  private float moveForce = 400f;
  private float airMoveMod = .05f;
  private float maxSpeed = 7f;
  private float jumpForce = 700f;
  private float velocityDecay = .8f;
  private bool jump = false;
  private bool grounded = false;
  private Transform groundCheck;
  private Rigidbody2D body;

  // Use this for initialization
  void Start() {
    groundCheck = transform.Find("Ground");
    body = this.GetComponent<Rigidbody2D>();
  }
  
  // Update is called once per frame
  void Update() {
    grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

    if(Input.GetButtonDown("Jump") && grounded)
      jump = true;
  }

  // Fizicks
  void FixedUpdate() {
    float h = Input.GetAxis("Horizontal");
    float moveMod = 1f;
    float maxMod = 1f;

    if(!grounded)
      moveMod *= airMoveMod;

    if(h * body.velocity.x < maxSpeed)
      body.AddForce(Vector2.right * h * moveForce * moveMod);

    if(Mathf.Abs(body.velocity.x) > maxSpeed)
      body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);

    if(grounded && h == 0) {
      body.velocity = body.velocity * velocityDecay;
    }

    if(jump) {
      body.AddForce(new Vector2(0f, jumpForce));
      jump = false;
    }

    body.MoveRotation(0);
  }
}
