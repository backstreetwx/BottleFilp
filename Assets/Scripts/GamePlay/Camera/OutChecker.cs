using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace GamePlay.Camera
{
  public class OutChecker : MonoBehaviour {
    public bool HasChecked;
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerEnterOver;
    [Range(0.1F, 10.0F)]
    public float WaitSeconds = 1.0F;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
      if (this.HasChecked)
        return;



      BottleController _bottle = other.GetComponent<BottleController> ();
      if (_bottle != null) 
      {
        this.HasChecked = true;
        StartCoroutine (OnTriggerEnterCoroutine ());
      }
    }

    IEnumerator OnTriggerEnterCoroutine()
    {
      OnTriggerEnter.Invoke ();
      yield return new WaitForSeconds (this.WaitSeconds);
      OnTriggerEnterOver.Invoke ();
    }
  }
}

