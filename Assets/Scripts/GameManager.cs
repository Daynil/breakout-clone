using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct MapDimens
{
  public float width;
  public float height;
}

public class GameManager : MonoBehaviour
{

  public int level = 1;

  public GameObject player;
  public List<GameObject> balls;

  public GameObject brickPrefab;

  public MapDimens mapDimens;
  public float wallWidth;
  public float ceilingHeight;
  public float brickPadding;

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

    Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    this.mapDimens.height = camera.orthographicSize * 2;
    this.mapDimens.width = mapDimens.height * camera.aspect;

    GameObject wall = GameObject.Find("Wall");
    this.wallWidth = wall.transform.localScale.x;
    // Ceiling has same height as wall's width
    this.ceilingHeight = wall.transform.localScale.x;

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
    float brickWidth = this.brickPrefab.transform.localScale.x;
    float brickHeight = this.brickPrefab.transform.localScale.y;

    float firstBrickY = (this.mapDimens.height / 2) - this.ceilingHeight - (brickHeight / 2) - this.brickPadding;
    float brickTotalYSpacing = brickHeight + this.brickPadding;

    string[] rows = System.IO.File.ReadAllLines($"./Assets/Levels/level{this.level}.txt");

    float brickTotalXSpacing = brickWidth + this.brickPadding;

    int totalCols = rows[0].Length;
    float totalSpaceOccupied = totalCols * brickTotalXSpacing;
    float levelWidthAvailable = this.mapDimens.width - (this.wallWidth * 2);
    float levelPadding = (levelWidthAvailable - totalSpaceOccupied) / 2;

    float firstBrickX = (-this.mapDimens.width / 2) + (this.wallWidth * 2) + (brickWidth / 2) + levelPadding;

    for (int row = 0; row < rows.Length; row++)
    {
      for (int col = 0; col < rows[row].Length; col++)
      {
        string brickChar = rows[row][col].ToString();

        // Blank slot
        if (brickChar == "x") continue;

        Vector3 brickPosition = new Vector3(firstBrickX + (brickTotalXSpacing * col),
                                            firstBrickY - (brickTotalYSpacing * row),
                                            0);
        GameObject brick = Instantiate(this.brickPrefab, brickPosition, Quaternion.identity);

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
