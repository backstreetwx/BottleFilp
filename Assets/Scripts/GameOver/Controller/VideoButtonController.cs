using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PJAdvertisememt;
using GoogleMobileAds.Api;

namespace GameOver.Controller{

  public class VideoButtonController : MonoBehaviour {

    // Use this for initialization
    void Start () 
    {
      videoButton = GetComponent<Button> ();
      videoImage = GetComponent<Image> ();
      SetInvisibleAndNonclickable ();

      buttonBlink = GetComponent<ButtonBlinkView> ();
      
      adManager = GameObject.FindObjectOfType<AdManager> ();
      diamondController = GameObject.FindObjectOfType<DiamondController> ();
      RequestRewardVideo ();
    }

    // Update is called once per frame
    void Update () 
    {

    }

    public void ShowVideoAndGetDiamond()
    {
      Debug.Log ("Show Video");
      adManager.ShowRewardVideo (this.GetReward);
    }

    void RequestRewardVideo()
    {
      StartCoroutine(adManager.RequestRewardVideoCoroutine (this.VideoLoadSucceed , this.VideoLoadTimeout));
    }

    void VideoLoadSucceed(string onLoaded)
    {
      Debug.Log (onLoaded);
      SetVisibleAndClickable ();

      buttonBlink.InitAnimation ();
      buttonBlink.PlayAnimation ();
    }

    void VideoLoadTimeout(string onTimeout)
    {
      Debug.Log (onTimeout);
    }

    void GetReward(Reward reward)
    {
      int _rewardDiamondNum = (int)reward.Amount;
      Debug.Log ("get reward" + _rewardDiamondNum);
      diamondController.AddDiamond (_rewardDiamondNum);
      SetInvisibleAndNonclickable ();
    }

    void SetVisibleAndClickable()
    {
      Debug.Log ("Button visible");
      videoButton.interactable = true;
      Color _color = videoImage.color;
      _color.a = transparencyMax;
      videoImage.color = _color;
    }

    void SetInvisibleAndNonclickable()
    {
      Debug.Log ("Button invisible");
      videoButton.interactable = false;
      Color _color = videoImage.color;
      _color.a = transparencyMin;
      videoImage.color = _color;
    }

    private Button videoButton;
    private Image videoImage;
    private DiamondController diamondController;
    private AdManager adManager;
    private ButtonBlinkView buttonBlink;

    float transparencyMin = 0.0f;
    float transparencyMax = 1.0f;
  }
}