using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement;
using ConstCollections.PJConstStrings;


public class ScoreView : MonoBehaviour {


  // Use this for initialization
  void Start () 
  {
    scoreText = GetComponent<Text> ();
    this.globalDataManager = GameObject.FindObjectOfType<GlobalDataManager> ();
    int _score = this.globalDataManager.GetValue<int> (PJGlobal.PlayScore);

    scoreText.text = _score.ToString ();
  }

  // Update is called once per frame
  void Update () 
  {

  }

  private Text scoreText;
  GlobalDataManager globalDataManager;
}
