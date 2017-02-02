using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ConstCollections.PJEnums;
using Common.UI;

namespace Title.UI{

  public class ButtonAudioView : ButtonView {

    public Sprite VoiceONSprite;
    public Sprite VoiceOFFSprite;


    public void Init()
    {
      image = GetComponent<Image> ();
    }

    public void SwitchAudio(bool state)
    {
      if (state) 
      {
        image.sprite = VoiceONSprite;
      }
      else if(!state)
      {
        image.sprite = VoiceOFFSprite;
      }
    }

    private Image image;
  }

}
