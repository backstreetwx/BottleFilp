using UnityEngine;
using UnityEditor;
using Common;

namespace GamePlay.Obstacles.Editor
{
  [CustomEditor(typeof(StageObjectManager))]
  public class StageObjectManagerEditor : UnityEditor.Editor 
  {
    void OnEnable()
    {
      this.script = (StageObjectManager)target;
    }

    public override void OnInspectorGUI()
    {
      DrawDefaultInspector ();

      if (Application.isPlaying)
        return;

      if (script.Pool == null)
        script.Pool = GameObject.FindObjectOfType<GameObjectPool> ();

      if(script.PrefabList == null || script.PrefabList.Length == 0)
        throw new System.Exception ("PrefabList.Length == 0");
        

      if (script.ProbabilityList.Length != script.PrefabList.Length) 
      {
        throw new System.Exception ("ProbabilityList.Length != PrefabList.Length");
      }

      // Check Sum of Probability
      int _probabilitySum = 0;
      foreach (var _pro in script.ProbabilityList) {
        _probabilitySum += _pro;
        if (_probabilitySum > script.ProbabilitySumMax)
          throw new System.Exception ("Sum of ProbabilityList overflow");
      }

      if(_probabilitySum < script.ProbabilitySumMax)
        throw new System.Exception (string.Format("Sum of ProbabilityList smaller than ProbabilitySumMax({0})", script.ProbabilitySumMax));

      /*
      if (GUILayout.Button ("Match ProbabilityList with PrefabList")) 
      {
        
      }
      */
    }

    StageObjectManager script;
  }
}

