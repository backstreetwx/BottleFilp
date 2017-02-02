using UnityEngine;
using System.Collections;
using DataManagement;
using System.Collections.Generic;
using ConstCollections.PJEnums;
using GameOver.UI;
using GameOver.Controller;

namespace GameOver{
  
  public class BottleStateListController : MonoBehaviour {

    public BottleView[] BottleViews; 

    // Use this for initialization
    void Start () 
    {

      bottleStateList = new Dictionary<int, BOTTLE_STATE>();

      Dictionary<int, BOTTLE_STATE> bottleStateListTemp = new Dictionary<int, BOTTLE_STATE>();
      bottleStateListTemp = UserData.Instance.BottleStateList;

      for(int i = 0; i < BottleViews.Length; i++)
      {
        bottleStateList.Add (i,bottleStateListTemp[i]);
      }

      for (int i = 0; i < BottleViews.Length; i++) 
      {
        if (bottleStateList [i] == BOTTLE_STATE.NONE) 
        {
          bottleStateList [i] = BOTTLE_STATE.LOCKED;
        }
        BottleViews [i].State = bottleStateList [i];
      }

      UserData.Instance.BottleStateList = bottleStateList;
    }

    // Update is called once per frame
    void Update () 
    {

    }

    private Dictionary<int, BOTTLE_STATE> bottleStateList;

  }

}