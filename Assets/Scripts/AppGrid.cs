using UnityEngine;

static class AppGrid
{
  public static GameManager gameManager;

  static AppGrid()
  {
    GameObject _app = GameObject.Find("__app");

    gameManager = _app.GetComponent<GameManager>();
  }

}
