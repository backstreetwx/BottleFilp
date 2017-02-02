using UnityEngine;
using System.Collections;
using Common;
using ConstCollections.PJEnums;

namespace GamePlay.Obstacles
{
  public class StageObjectManager : SingletonObject<StageObjectManager> 
  {
    public GameObjectPool Pool;
    public float BaseX;
    public float BaseY;
    public GameObject[] PrefabList;
    public int[] ProbabilityList;

    [ReadOnly]
    public int ProbabilitySumMax = 100;


    // Use this for initialization
    void Start () 
    {
      if(this.Pool == null)
        this.Pool = GameObject.FindObjectOfType<GameObjectPool> ();

      // Check Sum of Probability
      /*
      int _probabilitySum = 0;
      foreach (var _pro in ProbabilityList) {
        _probabilitySum += _pro;
        if (_probabilitySum > ProbabilitySumMax)
          throw new System.Exception ("Sum of ProbabilityList overflow");
      }*/
    }
     
    public GameObject GenerateNext(Vector3 currentPos, Vector3 currentDistanceOffset, INFINITY_MOVE_DIRECTION moveDirection)
    {
      int? _index = null;
      int _randomInt = Random.Range (0, this.ProbabilitySumMax);
      Debug.Log ("_randomInt =" + _randomInt);
      int _probabilityStart = 0;
      int _probabilityEnd = 0;

      for (int i = 0; i < this.ProbabilityList.Length; i++) 
      {
        _probabilityStart = _probabilityEnd;
        _probabilityEnd = _probabilityStart + this.ProbabilityList[i];

        if (_randomInt >= _probabilityStart && _randomInt < _probabilityEnd) {
          _index = i;
          break;
        }
      }

      InfinityObjectController _infinityObj = this.PrefabList [_index.Value].GetComponent<InfinityObjectController>();

      Vector3 _nextPos = Vector3.zero;
      switch (moveDirection) {
      case INFINITY_MOVE_DIRECTION.RIGHT:
        _nextPos = new Vector3 (currentPos.x + currentDistanceOffset.x + _infinityObj.DistanceOffset.x, this.BaseY, currentPos.z);
        _nextPos.y += _infinityObj.DistanceOffset.y;
        break;
      case INFINITY_MOVE_DIRECTION.LEFT:
        _nextPos = new Vector3 (currentPos.x - currentDistanceOffset.x - _infinityObj.DistanceOffset.x, this.BaseY, currentPos.z);
        _nextPos.y += _infinityObj.DistanceOffset.y;
        break;

      }



      return this.Pool.FindGameObjectFromCache (this.PrefabList[_index.Value], _nextPos, Quaternion.identity, this.gameObject);
    }

    public void Collect(GameObject obj)
    {
      this.Pool.CollectGameObject (obj);
    }


  }
}

