using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
  public Vector2 moveSpeed;
  public Vector2 velocity;

  public Rigidbody2D rb2d;


  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Started");
    GameObject camera = GameObject.Find("Main Camera");
    Debug.Log(camera.GetComponent<Camera>().pixelRect);
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

  public void OnMove(InputValue value)
  {
    // moveVal = value.Get<Vector2>();
    this.velocity = value.Get<Vector2>() * this.moveSpeed;
  }

}
