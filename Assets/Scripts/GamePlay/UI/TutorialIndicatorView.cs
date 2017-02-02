using UnityEngine;
using System.Collections;

namespace GamePlay.UI
{
  public class TutorialIndicatorView : MonoBehaviour 
  {
    public void DestoryObject()
    {
      GameObject.Destroy(this.gameObject);
    }
  }
}

