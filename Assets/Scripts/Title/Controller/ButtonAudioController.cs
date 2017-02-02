using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using DataManagement;
using ConstCollections.PJEnums;
using Title.UI;
using Common;

namespace Title.Controller{
 
  public class ButtonAudioController : MonoBehaviour {

    public ButtonAudioView ButtonAudio;

    // Use this for initialization
    void Start () 
    {
      voiceState = UserData.Instance.AudioActive;
      InitButtonImage (voiceState);
    }


    public void ToggleAudio()
    {
      bool _audioActiveNext = !UserData.Instance.AudioActive;
      UserData.Instance.AudioActive = _audioActiveNext;
      ButtonAudio.SwitchAudio (_audioActiveNext);
    }

    void InitButtonImage(bool state)
    {
      ButtonAudio.Init ();
      ButtonAudio.SwitchAudio (state);
    }

    private bool voiceState;
  }
}
