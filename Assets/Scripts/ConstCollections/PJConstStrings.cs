using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ConstCollections.PJEnums;

namespace ConstCollections.PJConstStrings
{
  public struct PJGlobal
  {
    #region PUBLIC_STATIC_MEMBER
    public static readonly string PrevSceneName = "PrevSceneName";
    public static readonly string PlayScore = "PlayScore";
    public static readonly string ButtonMoveTriggerName = "ButtonMove";
    public static readonly string ButtonStandyByTriggerName = "ButtonStandby"; 

    public static readonly Dictionary<SCENE_LIST, string> SceneNameList = InitSceneNameList();
    #endregion

    #region PPRIVATE_STATIC_METHOD
    static Dictionary<SCENE_LIST, string> InitSceneNameList()  
    {
      Dictionary<SCENE_LIST, string> _nameList = new Dictionary<SCENE_LIST, string> ();
      _nameList [SCENE_LIST.TITLE] = "title";
      _nameList [SCENE_LIST.HOW_TO_PLAY] = "how_to_play";
      _nameList [SCENE_LIST.GAMEPLAY] = "gameplay";
      _nameList [SCENE_LIST.GAMEOVER] = "gameover";
      return _nameList;
    }
    #endregion
  }
}
