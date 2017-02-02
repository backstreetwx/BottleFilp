using UnityEngine;
using UnityEngine.Events;
using DataManagement;
using System.Collections;
using GameOver.UI;

namespace GameOver.Controller{
  
  public class HighScoreController : MonoBehaviour {

    public HighScoreView HighScore;

    // Use this for initialization
    void Start () 
    {
      highScore = UserData.Instance.HighScore;

      HighScore.Init (highScore);
    }

    // Update is called once per frame
    void Update () 
    {

    }

    public void UpdateHighScore()
    {
      highScore = UserData.Instance.HighScore;
      HighScore.UpdateScore (highScore);
    }

    private int highScore;
  }

}