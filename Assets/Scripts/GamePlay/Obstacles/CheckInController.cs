using UnityEngine;
using System.Collections;
using PJAudio;
using UnityEngine.Events;

namespace GamePlay
{
  [System.Serializable]
  public class CheckInTriggerEnterEvent : UnityEvent<CheckInController>
  {
  }

  public class CheckInController : MonoBehaviour 
  {
    public AudioClip CheckInClip;
    public bool HasChecked;
    public int Score = 1;

    public CheckInTriggerEnterEvent OnCheckIn;

    // Use this for initialization
    void Start () 
    {
      this.InitComponents ();
    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
      if (this.HasChecked)
        return;
      
      BottleController _bottle = other.GetComponent<BottleController> ();
      if (_bottle != null) 
      {
        this.HasChecked = true;
        this.audioManager.SEPlayer.Play (this.CheckInClip);
        this.gamePlayManager.Score += this.Score;

        this.OnCheckIn.Invoke (this);
      }
    }

    public void InitComponents()
    {
      if(this.audioManager == null)
        this.audioManager = GameObject.FindObjectOfType<AudioManager> ();

      if (this.gamePlayManager == null)
        this.gamePlayManager = GameObject.FindObjectOfType<GamePlayManager> ();
    }

    public void Reset()
    {
      this.InitComponents ();
      this.HasChecked = false;
      this.Score = 1;
    }

    AudioManager audioManager;
    GamePlayManager gamePlayManager;
  }
}

