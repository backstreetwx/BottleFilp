using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums;

namespace GamePlay.Obstacles
{
  public class StickController : InfinityObjectController 
  {
    public bool AutoRotate = true;
    [Range(10.0F, 360.0F)]
    public float RotateSpeed = 50.0F;
    public ROTATE_DIRECTION RotateDirection = ROTATE_DIRECTION.RIGHT;
    public OBSTACLE_TYPE Type = OBSTACLE_TYPE.STICK;

    protected virtual void OnDisable()
    {
      this.enableRotate = false;
      this.AutoRotate = true;
    }

    // Use this for initialization
    protected override void Start () 
    {
      base.Start ();

      this.rb2D = GetComponent<Rigidbody2D> ();
    }
      
    protected virtual void FixedUpdate()
    {
      if (this.enableRotate) 
      {      
        this.rb2D.MoveRotation (this.rb2D.rotation + this.RotateSpeed * (float)this.RotateDirection * Time.fixedDeltaTime);
      }
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
      base.OnCollisionEnter2D (coll);
      this.enableRotate = this.AutoRotate;
    }

    Rigidbody2D rb2D;
    bool enableRotate;
  }

  public enum ROTATE_DIRECTION
  {
    LEFT = 1,
    RIGHT = -1
  }
}
