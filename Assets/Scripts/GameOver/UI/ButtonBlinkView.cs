using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJConstStrings;

public class ButtonBlinkView : MonoBehaviour {

  // Use this for initialization
  void Start () 
  {
    
  }

  // Update is called once per frame
  void Update () 
  {
    
  }

  public void InitAnimation()
  {
    animator = GetComponent<Animator> ();
  }

  public void PlayAnimation()
  {
    animator.SetTrigger (PJGlobal.ButtonMoveTriggerName);
    animator.ResetTrigger (PJGlobal.ButtonStandyByTriggerName);
  }

  public void StopAnimation()
  {
    animator.SetTrigger (PJGlobal.ButtonStandyByTriggerName);
    animator.ResetTrigger (PJGlobal.ButtonMoveTriggerName);
  }

  private Animator animator;

}
