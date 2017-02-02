using UnityEngine;
using System.Collections;
using Common;
using GamePlay.BG;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DataManagement;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums;
using PJAudio;
using PJAdvertisememt;

namespace GamePlay
{
  [System.Serializable]
  public class  ScoreChangedEvent : UnityEvent<int>
  {
  }

  public class GamePlayManager : SingletonObject<GamePlayManager> 
  {
    public string NextSceneName;
    public WaveBgController[] WaveBgControllerList;
    public BottleController Bottle;
    public GameObject TutorialPrefab;

    public ScoreChangedEvent OnScoreChanged;
    public int Score 
    {
      get
      { 
        return this.score;
      }
      set
      {
        this.score = value;
        OnScoreChanged.Invoke (this.score);
      }
    }

    // Use this for initialization
    void Start () 
    {
      Debug.Log ("High Score : " + UserData.Instance.HighScore);
      this.adManager = GameObject.FindObjectOfType<AdManager> ();
      this.globalDataManager = GameObject.FindObjectOfType<GlobalDataManager> ();
      string _prevSceneName = this.globalDataManager.GetValue<string> (PJGlobal.PrevSceneName);
      if (_prevSceneName == PJGlobal.SceneNameList [SCENE_LIST.TITLE]) 
      {
        GameObject.Instantiate (TutorialPrefab);
      }

      this.audioManager = GameObject.FindObjectOfType<AudioManager> ();
      this.audioManager.BGMPlayer.PlayInitially ();
    }

    // Update is called once per frame
    void Update () {

    }

    public void BottleDropped()
    {
      foreach (var item in WaveBgControllerList) 
      {
        item.Movable = false;
      }

      Bottle.gameObject.SetActive (false);

    }

    public void ChangeToNextScene()
    {
      UserData.Instance.HighScore = this.Score;
      UserData.Instance.DiamondNumber += this.Score;
      this.globalDataManager.SetValue<int> (PJGlobal.PlayScore,this.Score);
      this.audioManager.BGMPlayer.Stop ();
      this.adManager.ShowInterstitial (this.LoadScene);
    }

    void LoadScene(string msg)
    {
      Debug.Log (msg);
      SceneManager.LoadScene (this.NextSceneName);
    }

    int score;

    GlobalDataManager globalDataManager;
    AudioManager audioManager;
    AdManager adManager;
  }
}

