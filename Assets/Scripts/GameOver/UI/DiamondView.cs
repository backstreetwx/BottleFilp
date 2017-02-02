using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using System.Collections;

namespace GameOver.UI{

  public class DiamondView : MonoBehaviour {


    // Use this for initialization
    void Start () 
    {
      
    }

    // Update is called once per frame
    void Update () 
    {
      
    }

    public void Init(int diamondNum)
    {
      this.diamondViewText = GetComponent<Text> ();

      this.diamondNum = diamondNum;
      this.diamondViewText.text = this.diamondNum.ToString ();
    }

    public void UpdateDiamond(int diamondNum)
    {
      this.diamondNum = diamondNum;
      this.diamondViewText.text = this.diamondNum.ToString ();
    }

    private Text diamondViewText;
    private int diamondNum;
   
  }
}