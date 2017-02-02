using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums;
using PJCamera;

namespace GamePlay.BG
{
  public class WaveBgController : MonoBehaviour 
  {
    public InfinityRunnerCamera InfinityCamera;
    public Transform[] ImageObjectList;
    public Vector3 MoveVelocity;
    public int CurrentIndex = 0;
    public bool Movable = true;

    // Use this for initialization
    void Start () 
    {
      this.distance = ImageObjectList [1].position - ImageObjectList [0].position;
      this.moveCount = this.distance.magnitude;
    }

    // Update is called once per frame
    void Update () {
      if (!this.Movable)
        return;
      
      if (InfinityCamera.CurrentMotion != CAMERA_MOTION_TYPE.LOOKAT_HORIZON)
        return;

      if (!InfinityCamera.NeedToMove)
        return;
      
      if (this.moveCount < 0) 
      {
        this.moveCount = this.distance.magnitude;
        this.ImageObjectList [this.CurrentIndex].position += this.distance * this.ImageObjectList.Length;
        this.CurrentIndex = (this.CurrentIndex + 1) % this.ImageObjectList.Length;
        return;
      }

      Vector3 _offset = this.MoveVelocity * Time.deltaTime;
      this.moveCount -= _offset.magnitude; 
      foreach (var item in ImageObjectList) 
      {
        item.Translate (_offset);
      }


    }

    public Vector3 distance;
    public float moveCount;
  }
}

