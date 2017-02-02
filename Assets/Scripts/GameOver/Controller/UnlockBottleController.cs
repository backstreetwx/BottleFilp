using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using ConstCollections.PJEnums;
using DataManagement;
using Common;
using ConstCollections.PJConstStrings;

namespace GameOver.Controller{

  [System.Serializable]
  public class MoveToUnlockButtonEvent : UnityEvent<int> {}


  public class UnlockBottleController : MonoBehaviour {

    public BottleView[] BottleViews;
    public ScrollView Scroll;

    public MoveToUnlockButtonEvent MoveToUnlockEvent;

    // Use this for initialization
    void Start () 
    {
      button = GetComponent<Button> ();
      button.interactable = false;

      buttonBlink = GetComponentInChildren<ButtonBlinkView> ();
      buttonBlink.InitAnimation ();

      MoveToUnlockEvent.AddListenerWithEditor (Scroll.IsMovingToUnlockButton);
    }

    // Update is called once per frame
    void Update () 
    {

    }

    public void SetUnlockEnable()
    {
      button.interactable = true;

      buttonBlink.PlayAnimation ();
    }

    public void SetUnlockDisable()
    {
      button.interactable = false;

      buttonBlink.StopAnimation ();
    }

    public void SetBottleUnlock()
    {
      
      List<BottleView> bottleList = new List<BottleView> ();
      for (int i = 0; i < BottleViews.Length; i++) 
      {
        if (BottleViews [i].State == BOTTLE_STATE.LOCKED) 
        {
          bottleList.Add (BottleViews [i]);
        }
      }

      if (bottleList.Count > 0) 
      {
        Debug.Log ("bottle list" + bottleList.Count);
        int _random = Random.Range (0,bottleList.Count-1);
        Debug.Log ("Random unlock num" + _random);
        bottleList [_random].SetUnlock ();

        Debug.Log ("Bottle list name" + bottleList [_random].gameObject.name);

        for (int i = 0; i < BottleViews.Length; i++) 
        {
          if (BottleViews [i] == bottleList [_random]) 
          {
            Dictionary<int, BOTTLE_STATE> _list = new Dictionary<int, BOTTLE_STATE>();
            _list.Add (i,BOTTLE_STATE.UNLOCKED);
            UserData.Instance.BottleStateList = _list;
            Debug.Log ("Bottle controller i" + BottleViews [i].gameObject.name + " bottleList [random] name " + bottleList [_random].gameObject.name);
            MoveToUnlockEvent.Invoke (i);
          }
        }
      }
    }

    private Button button;

    private ButtonBlinkView buttonBlink;
  }
}