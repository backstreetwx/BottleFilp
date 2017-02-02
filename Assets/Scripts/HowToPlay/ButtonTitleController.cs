using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.SceneManagement;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums;
using DataManagement;

namespace HowToPlay{

  public class ButtonTitleController : MonoBehaviour {

    public string NextSceneName = PJGlobal.SceneNameList[SCENE_LIST.TITLE];

    // Use this for initialization
    void Start () 
    {
      this.globalDataManager = GameObject.FindObjectOfType<GlobalDataManager> ();
    }

    // Update is called once per frame
    void Update () 
    {

    }

    public void ChangeToNextScene()
    {
      this.globalDataManager.SetValue<string> (PJGlobal.PrevSceneName, SceneManager.GetActiveScene ().name);
      SceneManager.LoadScene (this.NextSceneName);
    }

    GlobalDataManager globalDataManager;
  }
}