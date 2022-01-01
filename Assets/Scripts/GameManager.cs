using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  public int level = 1;

  public GameObject player;
  public List<GameObject> balls;

  public GameObject brickPrefab;

  void Awake()
  {
    DontDestroyOnLoad(this.gameObject);

    SceneManager.sceneLoaded += OnSceneLoaded;

    if (PreloadInit.otherScene > 0)
    {
      Debug.Log("Loading other scene");
      SceneManager.LoadScene(PreloadInit.otherScene);
    }
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    if (scene.buildIndex == 1)
    {
      this.InitializeLevel();
    }
  }

  void InitializeLevel()
  {
    this.player = GameObject.Find("Player");
    this.balls = new List<GameObject>();
    this.balls.Add(GameObject.Find("Ball"));

    this.ResetBall();
    this.LoadBricks();
  }

  public void ResetBall()
  {
    this.balls[0].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    this.balls[0].GetComponent<Rigidbody2D>().isKinematic = true;
    this.balls[0].transform.parent = this.player.transform;

    this.balls[0].transform.position = this.player.transform.position + new Vector3(0, this.player.transform.localScale.y, 0);
  }

  public void LoadBricks()
  {
    string[] rows = System.IO.File.ReadAllLines($"./Assets/Levels/level{this.level}.txt");
    foreach (string row in rows)
    {
      for (int col = 0; col < row.Length; col++)
      {
        string brickChar = row[col].ToString();

        // Blank slot
        if (brickChar == "x") continue;

        GameObject brick = Instantiate(this.brickPrefab, new Vector3(1 + col, 3, 0), Quaternion.identity);

        BrickScript brickScript = brick.gameObject.GetComponent<BrickScript>();
        if (brickChar == "0")
        {
          brickScript.indestructable = true;
        }
        else
        {
          brickScript.health = Int32.Parse(brickChar);
        }
      }
    }
  }
}
