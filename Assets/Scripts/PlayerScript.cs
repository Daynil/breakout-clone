using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
  public Vector2 moveSpeed;
  public Vector2 velocity;
  public Vector2 startForce;

  public Vector2 directionalForce;

  public Rigidbody2D rb;

  public GameObject ball;
  public Rigidbody2D ballRb;

  // Start is called before the first frame update
  void Start()
  {
    Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    var height = camera.orthographicSize * 2;
    Debug.Log($"aspect: {camera.aspect}");
    Debug.Log($"width: {height * camera.aspect}, height: {height}");

    this.rb = GetComponent<Rigidbody2D>();
    this.ballRb = this.ball.GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    this.rb.MovePosition(rb.position + this.velocity * Time.fixedDeltaTime);
  }

  void OnMove(InputValue value)
  {
    this.velocity = value.Get<Vector2>() * this.moveSpeed;
  }

  void OnFire(InputValue value)
  {
    // If we haven't launched the ball yet, do so.
    if (this.ball.transform.parent != null)
    {
      this.LaunchBall();
      return;
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Ball")
    {
      float playerRadius = this.transform.localScale.x / 2;
      foreach (ContactPoint2D p in collision.contacts)
      {
        float collisionX = p.point.x;
        float playerX = this.transform.position.x;
        float directionalMagnitude = (collisionX - playerX) / playerRadius;
        collision.collider.GetComponent<Rigidbody2D>().AddForce(directionalForce * directionalMagnitude);
      }
    }
  }

  void LaunchBall()
  {
    this.ball.transform.parent = null;
    this.ballRb.isKinematic = false;
    this.ballRb.AddForce(startForce);
  }

}
