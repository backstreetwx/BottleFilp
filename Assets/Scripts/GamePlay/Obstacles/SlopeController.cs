using UnityEngine;
using System.Collections;
using Common;
using PJAudio;
using ConstCollections.PJEnums;

namespace GamePlay.Obstacles
{
  public class SlopeController : InfinityObjectController 
  {
    public AudioClip CheckInClip;
    public OBSTACLE_TYPE Type = OBSTACLE_TYPE.SLOPE;

    // Use this for initialization
    protected override void Start ()
    {
      base.Start ();

      if(this.audioManager == null)
        this.audioManager = GameObject.FindObjectOfType<AudioManager> ();
      
      base.CheckInController.OnCheckIn.AddListenerWithEditor (this.PlaySEOnCheckIn);
    }

    void PlaySEOnCheckIn(MonoBehaviour mono)
    {
      this.audioManager.SEPlayer.Play (CheckInClip);
    }

    AudioManager audioManager;
  }
}

