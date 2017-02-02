using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using ConstCollections.PJEnums;
using Common;
using GameOver.UI;

namespace GameOver.Controller {

  [System.Serializable]
  public class SelectedEvent : UnityEvent<string> {}

  [System.Serializable]
  public class SetSelectIndexEvent : UnityEvent<string> {}


  public class BottleView : MonoBehaviour {

    public Sprite ImageSprite;
    public BOTTLE_STATE State;
    public BottleSelectController BottleSelect;

    public ScrollView scroll;

    public SelectedEvent SelectEvent;
    public SetSelectIndexEvent SetSelectEvent;

    // Use this for initialization
    void Start () 
    {
      buttonImage = GetComponent<Image> ();
      button = GetComponent<Button> ();
      SelectEvent.AddListenerWithEditor (scroll.SelectBottle);
      SetSelectEvent.AddListenerWithEditor (BottleSelect.SelectBottle);
      bottleSelect = GetComponentInChildren<BottleSelectView> ();

    }

    // Update is called once per frame
    void Update () 
    {
      if (State == BOTTLE_STATE.LOCKED) 
      {
        button.interactable = false;
      }

      if (State == BOTTLE_STATE.UNLOCKED) 
      {
        button.interactable = true;

        buttonImage.sprite = ImageSprite;
      }
    }

    public void SetUnlock()
    {
      buttonImage.sprite = ImageSprite;
      State = BOTTLE_STATE.UNLOCKED;
    }

    public void SelectBottle()
    {
      if (State == BOTTLE_STATE.UNLOCKED) 
      {
        SelectEvent.Invoke (gameObject.name);
        SetSelectEvent.Invoke (bottleSelect.name);
      }
    }

    private Image buttonImage;
    private Button button;
    private BottleSelectView bottleSelect;

  }
}
