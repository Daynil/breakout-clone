using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

  public Vector2 velocity;
  public Vector2 startForce;

  private Rigidbody2D rb;

  // Start is called be1fore the first frame update
  void Start()
  {
    this.rb = this.GetComponent<Rigidbody2D>();
    this.rb.AddForce(startForce);
  }

  // Update is called once per frame
  void Update()
  {
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.name == "Floor")
    {
      Debug.Log("Game over!");
      this.ResetBall();
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Brick")
    {
      collision.collider.GetComponent<BrickScript>().Damage(1);
    }
  }

  private void ResetBall()
  {
    this.transform.position = new Vector3(0.41f, 2.33f, 0);
    this.rb.velocity = Vector2.zero;
    this.rb.AddForce(startForce);
  }
}
