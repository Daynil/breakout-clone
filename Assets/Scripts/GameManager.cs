using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  void Awake()
  {
    DontDestroyOnLoad(this.gameObject);

    if (PreloadInit.otherScene > 0)
    {
      Debug.Log("Loading other scene");
      SceneManager.LoadScene(PreloadInit.otherScene);
    }
  }
}
