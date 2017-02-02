using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using System.Collections;

namespace GameOver.UI{

  public class HighScoreView : MonoBehaviour {

   
    // Use this for initialization
    void Start () 
    {
      
    }

    // Update is called once per frame
    void Update () 
    {
      
    }

    public void Init(int score)
    {
      highScoreText = GetComponent<Text> ();
      highScore = score;
      highScoreText.text = highScore.ToString ();
    }

    public void UpdateScore(int score)
    {
      highScore = score;
      highScoreText.text = highScore.ToString ();
    }

    private Text highScoreText;
    private int highScore;

  }
 
}