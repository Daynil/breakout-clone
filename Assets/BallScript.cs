using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

  public Vector2 velocity;
  public Vector2 startForce;

  private Rigidbody2D rb2d;

  // Start is called be1fore the first frame update
  void Start()
  {
    this.rb2d = this.GetComponent<Rigidbody2D>();
    this.rb2d.AddForce(startForce);
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.name == "Floor")
    {
      Debug.Log("Game over!");
      this.ResetBall();
    }
  }

  private void ResetBall()
  {
    this.transform.position = new Vector3(0.41f, 2.33f, 0);
  }
}
