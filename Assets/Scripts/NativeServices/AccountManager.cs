using UnityEngine;
using System.Collections;
using Common;
using DataManagement;

namespace NativeServices
{
  public class AccountManager : SingletonObject<AccountManager> 
  {

    // Use this for initialization
    IEnumerator Start () 
    {
      PlatformAccount.Instance.Login ();
      yield return new WaitUntil (() => 
        {
          return PlatformAccount.Instance.HasLogined;
        });

      int _remoteHighScore = PlatformAccount.Instance.GetMyHighScore ();
      int _localHighScore = UserData.Instance.HighScore;
      if (_remoteHighScore > _localHighScore)
        UserData.Instance.HighScore = _remoteHighScore;
      else if (_remoteHighScore < _localHighScore)
        PlatformAccount.Instance.ReportScore (_localHighScore);

      Debug.Log ("Syncronization of GameCenter & Leaderboard has finished.");
      yield break;
    }
  }
}

