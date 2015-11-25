using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  private float moveForce = 100f;
  private float maxSpeed = 5f;

  // Use this for initialization
  void Start() {
  
  }
  
  // Update is called once per frame
  void Update() {
  
  }

  // Fizicks
  void FixedUpdate() {
    float h = Input.GetAxis("Horizontal");

    if(h * this.GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
      this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

    if(Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
      this.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(this.GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
  }
}
