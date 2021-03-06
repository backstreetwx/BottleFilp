﻿using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Common;

namespace NativeServices
{
  public class PlatformAccount : Singleton<PlatformAccount> 
  {
    // FIXME: yang-zhang LEADER_BOARD_ID
    public static readonly string LEADER_BOARD_ID = "grp.xxx.score";

    public bool HasLogined = false;
    #region PUBLIC_METHOD
    public void Login() 
    {
      if (this.HasLogined) 
      {
        Debug.LogWarning ("Has logined!");
        return ;
      }

      Social.localUser.Authenticate (ProcessAuthentication);
    }

    public bool ShowLeaderboard()
    {
      if (!this.HasLogined) 
      {
        Debug.LogWarning ("Has not logined yet!");
        return false;
      }

      Social.ShowLeaderboardUI();
      return true;
    }

    public bool ReportScore (int score)
    {
      if (!this.HasLogined) 
      {
        Debug.LogWarning ("Has not logined yet!");
        return false;
      }
        
      Social.ReportScore (score, LEADER_BOARD_ID, success => 
        {
          if (success) 
          {
            Debug.Log ("ReportScore successful.");
          }
          else
          {
            Debug.LogWarning ("ReportScore failed!");
          }
        });
      return true;
    }

    public int GetMyHighScore()
    {
      if (!this.HasLogined) 
      {
        Debug.LogWarning ("Has not logined yet!");
        return 0;
      }

      if(this.leaderboard == null) 
        this.leaderboard = Social.CreateLeaderboard();
      
      int _myHighScore = 0;
      leaderboard.id = LEADER_BOARD_ID;
      leaderboard.LoadScores(result =>
        {
          if(!result)
          {
            Debug.LogWarning("LoadScores failed!");
            return;
          }
          
          Debug.Log("Received " + leaderboard.scores.Length + " scores");
          foreach (IScore score in leaderboard.scores)
          {
            if(score.leaderboardID == leaderboard.id && score.userID == Social.localUser.id)
            {
              _myHighScore = System.Convert.ToInt32(score.value);
              break;
            }
          }
        });

      return _myHighScore;
    }

    #endregion

    #region PRIVATE_METHOD
    void ProcessAuthentication (bool success)
    {
      if (success) 
      {
        this.HasLogined = true;
        Debug.Log ("Authentication successful");
        string userInfo = "Username: " + Social.localUser.userName +
                          "\nUser ID: " + Social.localUser.id +
                          "\nIsUnderage: " + Social.localUser.underage;
        Debug.Log (userInfo);
      } 
      else 
      {
        this.HasLogined = false;
        Debug.LogWarning ("Authentication failed");
      }
    }
    #endregion

    ILeaderboard leaderboard;
  }
}

