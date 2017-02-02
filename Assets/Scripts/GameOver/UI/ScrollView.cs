using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using PJAudio;
using DataManagement;
using System.Collections.Generic;
using ConstCollections.PJEnums;
using GameOver.UI;
using GameOver.Controller;
using Common;

namespace GameOver{

  public class ScrollView : MonoBehaviour {

    public RectTransform ScrollPanel;
    public Button[] Buttons;
    public RectTransform Center;
    public BottleSelectView[] BottleViews;

    public UnityEvent SetBottleUnSelectedEvent;
    public float TimeForChange = 2.5f;

    // Use this for initialization
    void Start () 
    {
      
      int buttonLength = Buttons.Length;
      distanceAwayFromCenter = new float[buttonLength];
      buttonDistance = (int)Mathf.Abs (Buttons [0].GetComponent<RectTransform> ().anchoredPosition.x - Buttons [1].GetComponent<RectTransform> ().anchoredPosition.x);

      foreach(BottleSelectView bottleView in BottleViews)
      {
        SetBottleUnSelectedEvent.AddListenerWithEditor (bottleView.UnSelect);
      }
      
    }

    // Update is called once per frame
    void Update () 
    {
      
      for (int i = 0; i < Buttons.Length; i++) 
      {
        distanceAwayFromCenter [i] = Mathf.Abs (Center.transform.position.x - Buttons[i].transform.position.x);
      }

      float minDistance = Mathf.Min (distanceAwayFromCenter);

      for (int j = 0; j < distanceAwayFromCenter.Length; j++) 
      {
        if (Mathf.Approximately(minDistance , distanceAwayFromCenter [j])) 
        {
          minButtonNum = j;
        }
      }

      if (!IsDragging&&!isMovingToButton) 
      {
        MoveToButton (minButtonNum * -buttonDistance);
      }

      if (isMovingToButton) 
      {
        MoveToButton (buttonNum * -buttonDistance);

        //When the distance between the Selected button and target location 
        //is smaller than half of the length of two buttons
        //release the isMovingToButton lock and it will be close to 
        // the target location cause now it is the closest button forward to the middle
        if (Mathf.Abs(ScrollPanel.anchoredPosition.x - (buttonNum * -buttonDistance)) < buttonDistance/2)
          isMovingToButton = false;
      }
    }

    public void StartDrag()
    {
      IsDragging = true;
    }

    public void EndDrag()
    {
      IsDragging = false;
    }

    public void IsMovingToUnlockButton(int _buttonNum)
    {
      
      isMovingToButton = true;
      buttonNum = _buttonNum;

    }

    public void SelectBottle(string name)
    {
      for (int i = 0; i < Buttons.Length; i++) 
      {
        if (Buttons [i].gameObject.name == name) 
        {
          buttonNum = i;

          IsMovingToUnlockButton (buttonNum);
        }
      }
    }

    public void SetBottleUnSelected()
    {
      SetBottleUnSelectedEvent.Invoke ();
    }

    public void MoveToSelectBottle(int index)
    {
      selectBottleIndex = index;

      IsMovingToUnlockButton (selectBottleIndex);
    }

    void MoveToButton(int position)
    {
      
      float newX = Mathf.Lerp (ScrollPanel.anchoredPosition.x, position, Time.deltaTime * TimeForChange);
      Vector2 newPosition = new Vector2 (newX,ScrollPanel.anchoredPosition.y);

      ScrollPanel.anchoredPosition = newPosition;
    }

    private float[] distanceAwayFromCenter;
    private bool IsDragging = false;
    private int buttonDistance;
    private int minButtonNum;

    private bool isMovingToButton = false;
    private int buttonNum = 0;
    private int selectBottleIndex;

  }
}
