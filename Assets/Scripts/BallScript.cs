using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

  public Vector2 velocity;

  private Rigidbody2D rb;

  void Start()
  {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.name == "Floor")
    {
      Debug.Log("Game over!");
      AppGrid.gameManager.ResetBall();
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Brick")
    {
      collision.collider.GetComponent<BrickScript>().Damage(1);
    }
  }
}
