using UnityEngine;
using System.Collections;

namespace ConstCollections.PJEnums
{
  public enum SCENE_LIST
  {
    TITLE = 0,
    HOW_TO_PLAY,
    GAMEPLAY,
    GAMEOVER
  }

  public enum INFINITY_MOVE_DIRECTION
  {
    NONE = 0,
    LEFT,
    RIGHT
  }

  public enum OBSTACLE_TYPE
  {
    NONE = 0,
    STICK,
    SLOPE
  }

  public enum BOTTLE_STATE
  {
    NONE = -1,
    UNLOCKED = 0,
    LOCKED = 1
  }
    
}
