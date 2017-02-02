using UnityEngine;
using DataManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using GameOver.UI;
using Common;

namespace GameOver.Controller{

  [System.Serializable]
  public class  SpendDiamondEvent : UnityEvent<int>
  {
  }

  
  public class DiamondController : MonoBehaviour {

    public Text diamondText;
    public UnlockBottleController UnlockButton;
    public DiamondView DiamondViewText;

    public int MaxDiamond = 200;
    public UnityEvent SetButtonUnlockEvent;
    public UnityEvent SetButtonlockEvent;
    public SpendDiamondEvent DiamondChangeEvent;

    // Use this for initialization
    void Start () 
    {
      diamondNum = UserData.Instance.DiamondNumber;
      SetButtonUnlockEvent.AddListenerWithEditor (UnlockButton.SetUnlockEnable);
      SetButtonlockEvent.AddListenerWithEditor (UnlockButton.SetUnlockDisable);
      DiamondChangeEvent.AddListenerWithEditor (DiamondViewText.UpdateDiamond);

      DiamondViewText.Init (diamondNum);

    }

    // Update is called once per frame
    void Update () 
    {
      
      if (diamondNum >= MaxDiamond) 
      {
        SetButtonUnlockEvent.Invoke ();
      }
      if (diamondNum < MaxDiamond) 
      {
        SetButtonlockEvent.Invoke ();
      }

    }

    public void SpendDiamond()
    {
      UserData.Instance.DiamondNumber -= MaxDiamond;
      UserData.Instance.Save ();
      diamondNum = UserData.Instance.DiamondNumber;
      DiamondChangeEvent.Invoke (diamondNum);
    }

    public void AddDiamond(int number)
    {
      UserData.Instance.DiamondNumber += number;
      UserData.Instance.Save ();
      diamondNum = UserData.Instance.DiamondNumber;
      DiamondChangeEvent.Invoke (diamondNum);
    }

    private int diamondNum = 0;
  }

}