using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using ConstCollections.PJEnums;
using PJAudio;
using DataManagement;

namespace GamePlay
{
  public class BottleController : MonoBehaviour 
  {
    public Transform CenterOfMass;

    public float RotateStartSpeed = 10F;
    public float RotateAcc = 1.0F;
    public float RotateCounter = 0.0F;

    public Vector2 ForceDirection = new Vector2(1.0F, 1.0F);

    public bool StopDummyRotate;

    [Range(0.1F, 800.0f)]
    public float ForcePower = 20.0F;
    public float TorquePower = 20.0F;

    public bool EnableThrow = false;

    [Range(0.0F, 0.6F)]
    public float ThrowDumper = 0.1F;

    public int SpriteIndex = 0;
    public Sprite[] SpriteList;

    public UnityEvent OnThrow;
    public UnityEvent OnDrop;

    // Use this for initialization
    void Start () 
    {
      rb2D = GetComponent<Rigidbody2D> ();
      this.rb2D.centerOfMass = CenterOfMass.localPosition;
      this.StopDummyRotate = true;
      this.InitSprite ();
    }

    void FixedUpdate()
    {
      if (this.StopDummyRotate)
        return;

      if (Mathf.Approximately (this.rb2D.rotation, this.nextTargetAngle))
      {
        return;
      }

      float _t = Mathf.InverseLerp(this.rb2D.rotation, this.nextTargetAngle, this.rb2D.rotation + this.rotateSpeed * Time.fixedDeltaTime);
      float _nextAngle = Mathf.Lerp (this.rb2D.rotation, this.nextTargetAngle, _t);
      this.rb2D.MoveRotation (_nextAngle);

      this.rotateSpeed += RotateAcc;// * Time.fixedDeltaTime;
    }

    public void InitSprite()
    {
      SpriteRenderer _spriteRendder = GetComponent<SpriteRenderer> ();
      this.SpriteIndex = UserData.Instance.BottleSelectedIndex;
      _spriteRendder.sprite = this.SpriteList [this.SpriteIndex];
    }

    public void Throw()
    {
      if (!this.EnableThrow)
        return;

      this.rb2D.velocity *= this.ThrowDumper;
      this.rb2D.AddForce (this.ForcePower * this.ForceDirection.normalized, ForceMode2D.Impulse);
//      this.rb2D.AddTorque (this.TorquePower, ForceMode2D.Impulse);

      this.rotateSpeed = this.RotateStartSpeed;
      this.nextTargetAngle = this.rb2D.rotation - 360.0F;
      this.StopDummyRotate = false;
      this.EnableThrow = false;

      this.OnThrow.Invoke ();
    }

    public void EnterObstacle()
    {
      this.StopDummyRotate = true;
      this.EnableThrow = true;
    }

    public void Drop()
    {
      this.OnDrop.Invoke ();
    }

    Rigidbody2D rb2D;
    [SerializeField]
    float nextTargetAngle;
    float rotateSpeed;
  }
}
