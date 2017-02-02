using UnityEngine;
using System.Collections;
using PJCamera;
using Common;
using UnityEngine.Events;
using ConstCollections.PJEnums;

namespace GamePlay.Obstacles
{
  public class InfinityObjectController : MonoBehaviour 
  {
    public INFINITY_MOVE_DIRECTION MoveDirection = INFINITY_MOVE_DIRECTION.RIGHT; 
    public bool HasGeneratedNext;
    public CheckInController CheckInController;
    public Vector3 DistanceOffset;

    // Use this for initialization
    protected virtual void Start () 
    {
      this.InitComponents ();
    }

    // Update is called once per frame
    protected virtual void Update () 
    {
      if (!this.HasGeneratedNext) 
      {
        this.GenerateNext();
      }

      this.CheckDead ();
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
      BottleController _bottle = coll.gameObject.GetComponent<BottleController> ();
      if (_bottle != null) 
      {
        _bottle.EnterObstacle ();
      }
    }

    protected virtual void OnCollisionExit2D(Collision2D coll)
    {
      BottleController _bottle = coll.gameObject.GetComponent<BottleController> ();
      if (_bottle != null) 
      {
        _bottle.EnableThrow = false;
      }
    }

    public void InitComponents()
    {
      if(this.infinityCamera == null)
        this.infinityCamera = GameObject.FindObjectOfType<InfinityRunnerCamera> ();
      if(this.stageController == null)
        this.stageController = GameObject.FindObjectOfType<StageObjectManager> ();
      if (this.CheckInController == null)
        this.CheckInController = GetComponentInChildren<CheckInController> ();
    }

    public void Reset()
    {
      this.InitComponents ();

      this.HasGeneratedNext = false;

      this.CheckInController.Reset ();
    }

    public void Dead()
    {
      this.HasGeneratedNext = false;
    }

    void GenerateNext()
    {
      switch (this.MoveDirection) 
      {
      case INFINITY_MOVE_DIRECTION.RIGHT:
        if (this.transform.position.x < this.infinityCamera.RightChecker.position.x) 
        {
          GameObject _obj = this.stageController.GenerateNext (this.transform.position, this.DistanceOffset, this.MoveDirection);
          InfinityObjectController _controller = _obj.GetComponent<InfinityObjectController> ();
          _controller.Reset ();

          this.HasGeneratedNext = true;
        }
        break;

      case INFINITY_MOVE_DIRECTION.LEFT:
        if (this.transform.position.x > this.infinityCamera.LeftChecker.position.x) 
        {
          GameObject _obj = this.stageController.GenerateNext (this.transform.position, this.DistanceOffset, this.MoveDirection);
          InfinityObjectController _controller = _obj.GetComponent<InfinityObjectController> ();
          _controller.Reset ();

          this.HasGeneratedNext = true;
        }
        break;
      }

    }

    void CheckDead()
    {
      switch (this.MoveDirection) 
      {
      case INFINITY_MOVE_DIRECTION.RIGHT:      
        if (this.transform.position.x < this.infinityCamera.LeftChecker.position.x) 
        {
          this.Dead ();
          this.stageController.Collect (this.gameObject);
        }
        break;

      case INFINITY_MOVE_DIRECTION.LEFT:
        if (this.transform.position.x > this.infinityCamera.RightChecker.position.x) 
        {
          this.Dead ();
          this.stageController.Collect (this.gameObject);
        }
        break;
      }

    }

    InfinityRunnerCamera infinityCamera;
    //GameObjectPool pool;
    StageObjectManager stageController;
  }


}

