using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using ConstCollections.PJEnums;
using ConstCollections.PJConstStrings;
using DataManagement;

namespace GameOver.Controller{

  public class RestartButtonController : MonoBehaviour {

    public string NextSceneName = PJGlobal.SceneNameList[SCENE_LIST.GAMEPLAY];

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