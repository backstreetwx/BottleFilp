using UnityEngine;
using Common;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ConstCollections.PJPaths;
using ConstCollections.PJEnums;
using NativeServices;

namespace DataManagement {

  public class UserData : Singleton<UserData>
  {
    public static readonly int DID_NOT_PLAY = -1;
    public static readonly float VOLUME_MAX = 1.0F;
    public static readonly float VOLUME_MIN = 0.0F;
    public static readonly int BOTTLE_NUMBER_MAX = 20;
    public static readonly int BOTTLE_DEFAULE_SELECTED_INDEX = 0;
    public static readonly int BOTTLE_DEFAULE_UNLOCKED_INDEX = 0;
    public System.Action<float> BGMVolumeChangedEvents;
    public System.Action<float> SEVolumeChangedEvents;

    public void Save() 
    {
      // This method will be called by Unity automatically when application exiting
      PlayerPrefs.Save ();
    }

    public void Clear()
    {
      PlayerPrefs.DeleteAll ();
    }

    /*
    public int PlayedTimesBeforeAd 
    {
      get 
      {
        return PlayerPrefs.GetInt (KEY_PLAYED_TIMES_BEFORE_AD, DID_NOT_PLAY);
      }
      set 
      {
        PlayerPrefs.SetInt (KEY_PLAYED_TIMES_BEFORE_AD, value);
      }
    }

    public int PlayedTimesBeforeAdLimit 
    {
      get 
      {
        return PlayerPrefs.GetInt (KEY_PLAYED_TIMES_BEFORE_AD_LIMIT, DID_NOT_PLAY);
      }
      set 
      {
        PlayerPrefs.SetInt (KEY_PLAYED_TIMES_BEFORE_AD_LIMIT, value);
      }
    }
    */

    public float BGMVolume
    {
      get 
      {
        return PlayerPrefs.GetFloat (KEY_BGM_VOLUME, VOLUME_MAX);
      }
      set 
      {
        PlayerPrefs.SetFloat (KEY_BGM_VOLUME, value);
        BGMVolumeChangedEvents (value);
      }
    }

    public float SEVolume
    {
      get 
      {
        return PlayerPrefs.GetFloat (KEY_SE_VOLUME, VOLUME_MAX);
      }
      set 
      {
        PlayerPrefs.SetFloat (KEY_SE_VOLUME, value);
        SEVolumeChangedEvents (value);
      }
    }

    public bool AudioActive
    {
      get
      { 
        int _result = PlayerPrefs.GetInt (KEY_AUDIO_ACTIVE, System.Convert.ToInt32(true));
        return System.Convert.ToBoolean (_result);
      }
      set
      { 
        if (value) 
        {
          this.BGMVolume = VOLUME_MAX;
          this.SEVolume = VOLUME_MAX;
        } 
        else 
        {
          this.BGMVolume = VOLUME_MIN;
          this.SEVolume = VOLUME_MIN;
        }

        PlayerPrefs.SetInt (KEY_AUDIO_ACTIVE, System.Convert.ToInt32(value));
      }
    }

    public int BottleSelectedIndex
    {
      get
      {
        return PlayerPrefs.GetInt (KEY_BOTTLE_SELECTED_INDEX, BOTTLE_DEFAULE_SELECTED_INDEX);
      }
      set
      {
        PlayerPrefs.SetInt (KEY_BOTTLE_SELECTED_INDEX, value);
      }
    }

    // Index, States
    public Dictionary<int, BOTTLE_STATE> BottleStateList
    {
      get
      {
        Dictionary<int, BOTTLE_STATE> _list = new Dictionary<int, BOTTLE_STATE>();

        for (int i = 0; i < BOTTLE_NUMBER_MAX; i++) 
        {
          int _resultInt = (int)BOTTLE_STATE.NONE;
          if(i == BOTTLE_DEFAULE_UNLOCKED_INDEX)
          {
            _resultInt = PlayerPrefs.GetInt (KEY_BOTTLE_STATE_PREFIX + i, (int)BOTTLE_STATE.UNLOCKED);
          }
          else
          {
            _resultInt = PlayerPrefs.GetInt (KEY_BOTTLE_STATE_PREFIX + i, (int)BOTTLE_STATE.NONE);
          }

          _list.Add (i, (BOTTLE_STATE)_resultInt);
        }

        return _list;
      }

      set
      {
        foreach (var _item in value) 
        {
          PlayerPrefs.SetInt (KEY_BOTTLE_STATE_PREFIX + _item.Key, (int)_item.Value);
        }
      }
    }

    public int HighScore
    {
      get
      {
        return PlayerPrefs.GetInt (KEY_HIGH_SCORE, 0);
      }
      set
      {
        int _currentHighScore = PlayerPrefs.GetInt (KEY_HIGH_SCORE, 0);

        if (value > _currentHighScore) 
        {
          PlayerPrefs.SetInt (KEY_HIGH_SCORE, value);
          PlatformAccount.Instance.ReportScore (value);
        }
      }
    }

    public int DiamondNumber
    {
      get
      {
        return PlayerPrefs.GetInt (KEY_DIAMOND_NUMBER, 0);
      }
      set
      {
        PlayerPrefs.SetInt (KEY_DIAMOND_NUMBER, value);
      }
    }

    #region PRIVATE_MEMBER
//    static readonly string KEY_PLAYED_TIMES_BEFORE_AD = "KEY_PLAYED_TIMES_BEFORE_AD";
//    static readonly string KEY_PLAYED_TIMES_BEFORE_AD_LIMIT = "KEY_PLAYED_TIMES_BEFORE_AD_LIMIT";
    static readonly string KEY_BGM_VOLUME = "KEY_BGM_VOLUME";
    static readonly string KEY_SE_VOLUME = "KEY_SE_VOLUME";
    static readonly string KEY_AUDIO_ACTIVE = "KEY_AUDIO_ACTIVE";
    static readonly string KEY_BOTTLE_SELECTED_INDEX = "KEY_BOTTLE_SELECTED_INDEX";
    static readonly string KEY_BOTTLE_STATE_PREFIX = "KEY_BOTTLE_INDEX_";
    static readonly string KEY_HIGH_SCORE = "KEY_HIGH_SCORE";
    static readonly string KEY_DIAMOND_NUMBER = "KEY_DIAMOND_NUMBER";
    #endregion


  }
}