using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

  public int maxHealth;
  [HideInInspector] public int health;
  public bool indestructable;
  SpriteRenderer sr;
  public int pointsPerHealth;

  // Start is called before the first frame update
  void Start()
  {
    this.sr = this.GetComponent<SpriteRenderer>();
    this.SetColor();
  }

  private void SetColor()
  {
    if (this.indestructable)
    {
      this.sr.color = Color.black;
      return;
    }

    if (this.health == 3)
    {
      this.sr.color = new Color(0, 255, 0);
    }
    else if (this.health == 2)
    {
      this.sr.color = new Color(0, 150, 150);
    }
    else
    {
      this.sr.color = new Color(255, 0, 0);
    }
  }

  public void Damage(int amount)
  {
    if (this.indestructable) return;

    this.health = this.health - amount;
    if (this.health <= 0)
    {
      AppGrid.gameManager.ScorePoints(this.gameObject);
    }
    else
    {
      this.SetColor();
    }
  }
}
