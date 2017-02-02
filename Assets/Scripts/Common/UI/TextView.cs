using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using ConstCollections.PJEnums;

namespace Common.UI
{
  public class TextView : MonoBehaviour
  {
    protected virtual void Start()
    {
      this.text = GetComponent<Text> ();
    }
      
    protected Text text;
  }
}


