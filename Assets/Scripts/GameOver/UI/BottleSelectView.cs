using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace GameOver.UI{
  
  public class BottleSelectView : MonoBehaviour {

    public Sprite MarkSprite;
    public Sprite UIMaskSprite;
    public bool IsSelected;

    // Use this for initialization
    void Start () 
    {
      selfImage = gameObject.GetComponent<Image> ();
    }

    // Update is called once per frame
    void Update () 
    {
      if (!IsSelected) 
      {
        selfImage.sprite = UIMaskSprite;
      }
      if (IsSelected) 
      {
        selfImage.sprite = MarkSprite;
      }
    }

    public void BeSelected()
    {
      IsSelected = true;
    }

    public void UnSelect()
    {
      IsSelected = false;
    }

    private Image selfImage;
  }

}