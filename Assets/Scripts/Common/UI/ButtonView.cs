using UnityEngine;
using System.Collections;
using PJAudio;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Common.UI
{
  [RequireComponent(typeof(Button))]
  public class ButtonView : MonoBehaviour 
  {
    public string GroupName;
    public Image ActivedImage;
    public bool EnableActivedImage;

    // Use this for initialization
    protected virtual void Start () 
    {
      this.sePlayer = FindObjectOfType<SEPlayer> ();
      this.button = GetComponent<Button> ();

      if (this.EnableActivedImage && this.ActivedImage == null) 
      {
        Image[] _images = this.transform.parent.GetComponentsInChildren<Image> ();
        Image _buttonImages = this.GetComponent<Image> ();
        foreach (var item in _images) 
        {
          if (item != _buttonImages)
            this.ActivedImage = item;
        }
      }

    }

    public void PlaySE(AudioClip clip)
    {
      if (!this.button.IsInteractable ())
        return;
      
      this.sePlayer.Play (clip);
    }

    public void PlaySEs(AudioClip[] clip)
    {
    }

    public void AddOnClick(UnityAction action)
    {
      #if UNITY_EDITOR
      UnityEditor.Events.UnityEventTools.AddPersistentListener(this.button.onClick, action);
      #else
      this.button.onClick.AddListener (action);
      #endif 
    }

    public void ToggleActiveGameObject(GameObject gameObject)
    {
      gameObject.SetActive (!gameObject.activeSelf);
    }

    protected SEPlayer sePlayer;
    protected Button button;
  }
}