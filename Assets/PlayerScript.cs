using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
  public Vector2 moveSpeed;
  public Vector2 velocity;

  public Vector2 directionalForce;

  public Rigidbody2D rb2d;


  // Start is called before the first frame update
  void Start()
  {
    Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    var height = camera.orthographicSize * 2;
    Debug.Log($"aspect: {camera.aspect}");
    Debug.Log($"width: {height * camera.aspect}, height: {height}");
  }

  // Update is called once per frame
  void Update()
  {
    // this.transform.Translate(new Vector3(moveVal.x, 0, 0) * moveSpeed * Time.deltaTime);
    // this.rb2d.MovePosition(rb2d.position + this.moveSpeed * Time.fixedDeltaTime)
  }

  void FixedUpdate()
  {
    this.rb2d.MovePosition(rb2d.position + this.velocity * Time.fixedDeltaTime);
  }

  void OnMove(InputValue value)
  {
    this.velocity = value.Get<Vector2>() * this.moveSpeed;
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

        // Debug.Log($"collision x: {collisionX}");
        // Debug.Log($"player x: {playerX}");
        // Debug.Log($"How far left or right collided: {collisionX - playerX}");
        // Debug.Log($"Ratio left/rightness: {(collisionX - playerX) / playerRadius}");
        // Debug.Log($"local: {p.point}");
      }
    }
  }

}
