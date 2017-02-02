using UnityEngine;
using System.Collections;
using DataManagement;
using GameOver.UI;

namespace GameOver.Controller{

  public class BottleSelectController : MonoBehaviour {

    public ScrollView Scroll;
    public BottleSelectView[] BottleViews;

    // Use this for initialization
    void Start () 
    {
      selectBottleIndex = UserData.Instance.BottleSelectedIndex;
      BottleViews [selectBottleIndex].BeSelected();
      Scroll.MoveToSelectBottle (selectBottleIndex);
    }

    // Update is called once per frame
    void Update () 
    {

    }

    public void SelectBottle(string name)
    {
      for (int i = 0; i < BottleViews.Length; i++) 
      {
        if (name == BottleViews [i].name) 
        {
          UserData.Instance.BottleSelectedIndex = i;
        }
      }
    }

    private int selectBottleIndex;

  }
}