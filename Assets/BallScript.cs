using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

  public Vector2 velocity;
  public Vector2 moveSpeed;

  private Rigidbody2D rb2d;

  // Start is called be1fore the first frame update
  void Start()
  {
    this.rb2d = this.GetComponent<Rigidbody2D>();
    Debug.Log(rb2d);
    this.rb2d.AddForce(new Vector2(100, -100));
  }

  // Update is called once per frame
  void Update()
  {
    // this.transform.Translate(new Vector3(moveVal.x, moveVal.y, 0) * moveSpeed * Time.deltaTime);
  }

  void FixedUpdate()
  {

  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    // if (other.name == "Player")
    // {
    //   Debug.Log($"Collided with: {other.name}");
    //   moveVal.y = -moveVal.y;
    // }

    if (other.name == "Floor")
    {
      Debug.Log("Game over!");
      this.ResetBall();
    }

    // Debug.Log($"Collided tag: {other.tag}");

    // if (other.tag == "Wall")
    // {
    //   moveVal.x = -moveVal.x;
    // }

    // if (other.name == "Ceiling")
    // {
    //   moveVal.y = -moveVal.y;
    // }

  }

  private void ResetBall()
  {
    this.transform.position = new Vector3(0.41f, 2.33f, 0);
  }
}
