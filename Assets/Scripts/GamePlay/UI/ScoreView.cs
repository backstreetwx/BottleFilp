using UnityEngine;
using System.Collections;
using Common.UI;
using Common;

namespace GamePlay.UI
{
  public class ScoreView : TextView {

    // Use this for initialization
    protected override void Start () {
      base.Start ();

      this.gamePlayManager = GameObject.FindObjectOfType<GamePlayManager> ();
      this.gamePlayManager.OnScoreChanged.AddListenerWithEditor (this.UpdateText);
    }

    void UpdateText(int score)
    {
      this.text.text = score.ToString();
    }

    GamePlayManager gamePlayManager;
  }
}

