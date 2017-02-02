using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common 
{
  public static class UnityExtensions
  {
    /// <summary>
    /// Adds the listener with editor.
    /// </summary>
    /// <param name="myEvent">My event.</param>
    /// <param name="action">Action.</param>
    public static void AddListenerWithEditor(this UnityEvent myEvent, UnityAction action)
    {

      #if UNITY_EDITOR
      myEvent.RemoveEmptyPersistentListeners();
      #endif

      /*
      for (int i = 0; i < myEvent.GetPersistentEventCount (); i++) {
        if(myEvent.GetPersistentTarget (i) == null)
        {
          myEvent.RemoveAllListeners ();
          break;
        }
      }*/

      if (myEvent.GetPersistentEventCount () == 0) 
      {
        #if UNITY_EDITOR
        UnityEditor.Events.UnityEventTools.AddPersistentListener (myEvent, action);
        #else
        myEvent.AddListener(action);
        #endif
        return;
      }


      for (int i = 0; i < myEvent.GetPersistentEventCount(); i++) 
      {
        string _storedMethodName = myEvent.GetPersistentMethodName (i);
        Object _storedTargetObj = myEvent.GetPersistentTarget (i);
        string _storedTypeName =  _storedTargetObj.GetType ().Name;
        if (_storedTargetObj == (Object)action.Target && 
          _storedMethodName == action.Method.Name &&
          _storedTypeName == action.Method.DeclaringType.Name) 
        {
          //Debug.LogFormat ("{0}.{1}.{2} has exsit in {3} !", _storedTargetObj.name, _storedTypeName, _storedMethodName, myEvent.ToString());
          //Debug.Log ("Skip add listener");
          return;
        }
      }

      #if UNITY_EDITOR
      UnityEditor.Events.UnityEventTools.AddPersistentListener (myEvent, action);
      #else
      myEvent.AddListener(action);
      #endif
    }

    /// <summary>
    /// Adds the listener with editor.
    /// </summary>
    /// <param name="myEvent">My event.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T0">The 1st type parameter.</typeparam>
    public static void AddListenerWithEditor<T0>(this UnityEvent<T0> myEvent, UnityAction<T0> action)
    {/*
      for (int i = 0; i < myEvent.GetPersistentEventCount (); i++) {
        if(myEvent.GetPersistentTarget (i) == null)
        {
          myEvent.RemoveAllListeners ();
          break;
        }
      }*/

      #if UNITY_EDITOR
      bool _hasNull = true;
      while(_hasNull)
      {
        _hasNull = false;

        for (int i = 0; i < myEvent.GetPersistentEventCount (); i++) {
          if(myEvent.GetPersistentTarget (i) == null)
          {
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(myEvent, i);
            _hasNull = true;
            break;
          }
        }
      }
      #endif

      if (myEvent.GetPersistentEventCount () == 0) 
      {
        #if UNITY_EDITOR
        UnityEditor.Events.UnityEventTools.AddPersistentListener (myEvent, action);
        #else
        myEvent.AddListener(action);
        #endif
        return;
      }


      for (int i = 0; i < myEvent.GetPersistentEventCount(); i++) 
      {
        string _storedMethodName = myEvent.GetPersistentMethodName (i);
        Object _storedTargetObj = myEvent.GetPersistentTarget (i);
        string _storedTypeName =  _storedTargetObj.GetType ().Name;

        if (_storedTargetObj == (Object)action.Target && 
          _storedMethodName == action.Method.Name &&
          _storedTypeName == action.Method.DeclaringType.Name) 
        {
          //Debug.LogFormat ("{0}.{1}.{2} has exsit in {3} !", _storedTargetObj.name, _storedTypeName, _storedMethodName, myEvent.ToString());
          //Debug.Log ("Skip add listener");
          return;
        }
      }

      #if UNITY_EDITOR
      UnityEditor.Events.UnityEventTools.AddPersistentListener (myEvent, action);
      #else
      myEvent.AddListener(action);
      #endif
    }

    /// <summary>
    /// Adds the listener with editor.
    /// </summary>
    /// <param name="eventTrigger">Event trigger.</param>
    /// <param name="eventType">Event type.</param>
    /// <param name="action">Action.</param>
    public static void AddListenerWithEditor(this EventTrigger eventTrigger, EventTriggerType eventType, UnityAction<BaseEventData> action)
    {
      #if UNITY_EDITOR
      eventTrigger.RemoveEmptyPersistentEvent();
      #endif

      if (eventTrigger.triggers.Count == 0) 
      {
        EventTrigger.Entry entry = new EventTrigger.Entry( );
        entry.eventID = eventType;

        #if UNITY_EDITOR
        UnityEditor.Events.UnityEventTools.AddPersistentListener (entry.callback, action);
        #else
        entry.callback.AddListener(action);
        #endif

        eventTrigger.triggers.Add (entry);
        return;
      }

      bool _foundEventID = false;
      foreach (var entry in eventTrigger.triggers) 
      {
        if (entry.eventID != eventType)
          continue;

        _foundEventID = true;

        bool _isExsit = false;
        for (int i = 0; i < entry.callback.GetPersistentEventCount (); i++) 
        {
          string _storedMethodName = entry.callback.GetPersistentMethodName (i);
          Object _storedTargetObj = entry.callback.GetPersistentTarget (i);
          string _storedTypeName =  _storedTargetObj.GetType ().Name;

          if (_storedTargetObj == (Object)action.Target && 
            _storedMethodName == action.Method.Name &&
            _storedTypeName == action.Method.DeclaringType.Name) 
          {
            _isExsit = true; 
            //Debug.LogFormat ("{0}.{1}.{2} has exsit in {3} !", _storedTargetObj.name, _storedTypeName, _storedMethodName, entry.callback.ToString());
          }
        }

        if (_isExsit) 
        {
          //Debug.Log ("Skip add listener");
          continue;
        }

        #if UNITY_EDITOR
        UnityEditor.Events.UnityEventTools.AddPersistentListener (entry.callback, action);
        #else
        entry.callback.AddListener(action);
        #endif
      }

      if (!_foundEventID) 
      {
        EventTrigger.Entry entry = new EventTrigger.Entry( );
        entry.eventID = eventType;

        #if UNITY_EDITOR
        UnityEditor.Events.UnityEventTools.AddPersistentListener (entry.callback, action);
        #else
        entry.callback.AddListener(action);
        #endif

        eventTrigger.triggers.Add (entry);
      }
    }

    #if UNITY_EDITOR
    /// <summary>
    /// Removes the empty persistent listeners.
    /// </summary>
    /// <param name="myEvent">My event.</param>
    public static void RemoveEmptyPersistentListeners(this UnityEvent myEvent)
    {
      bool _hasNull = true;
      while(_hasNull)
      {
        _hasNull = false;

        for (int i = 0; i < myEvent.GetPersistentEventCount (); i++) {
          if(myEvent.GetPersistentTarget (i) == null)
          {
            //Debug.LogFormat ( "Event-{0} is empty and will be removed.", i);
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(myEvent, i);
            _hasNull = true;
            break;
          }
        }
      }
    }

    /// <summary>
    /// Removes the empty persistent listeners.
    /// </summary>
    /// <param name="myEvent">My event.</param>
    /// <typeparam name="T0">The 1st type parameter.</typeparam>
    public static void RemoveEmptyPersistentListeners<T0>(this UnityEvent<T0> myEvent)
    {
      bool _hasNull = true;
      while(_hasNull)
      {
        _hasNull = false;

        for (int i = 0; i < myEvent.GetPersistentEventCount (); i++) {
          if(myEvent.GetPersistentTarget (i) == null)
          {
            //Debug.LogFormat ( "Event-{0} is empty and will be removed.", i);
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(myEvent, i);
            _hasNull = true;
            break;
          }
        }
      }
    }

    /// <summary>
    /// Removes all persistent listeners.
    /// </summary>
    /// <param name="myEvent">My event.</param>
    public static void RemoveAllPersistentListeners(this UnityEvent myEvent)
    {
      bool _isEmpty = false;
      while(!_isEmpty)
      {
        _isEmpty = true;
        if (myEvent.GetPersistentEventCount () > 0) 
        {
          _isEmpty = false;
          UnityEditor.Events.UnityEventTools.RemovePersistentListener(myEvent, 0);
        }
      }

      //Debug.LogFormat ( "Removed all persistent listeners from {0}.", myEvent.GetType().Name);
    }

    /// <summary>
    /// Removes all persistent listeners.
    /// </summary>
    /// <param name="myEvent">My event.</param>
    /// <typeparam name="T0">The 1st type parameter.</typeparam>
    public static void RemoveAllPersistentListeners<T0>(this UnityEvent<T0> myEvent)
    {
      bool _isEmpty = false;
      while(!_isEmpty)
      {
        _isEmpty = true;
        if (myEvent.GetPersistentEventCount () > 0) 
        {
          _isEmpty = false;
          UnityEditor.Events.UnityEventTools.RemovePersistentListener(myEvent, 0);
        }
      }

      //Debug.LogFormat ( "Removed all persistent listeners from {0} .", myEvent.GetType().Name);
    }

    /// <summary>
    /// Removes the empty persistent event.
    /// </summary>
    /// <param name="eventTrigger">Event trigger.</param>
    public static void RemoveEmptyPersistentEvent (this EventTrigger eventTrigger)
    {
      foreach (var entry in eventTrigger.triggers) 
      {
        entry.callback.RemoveEmptyPersistentListeners();
      }

      eventTrigger.triggers.RemoveAll(entry => {
        //Debug.Log (entry.eventID + " is empty and will be removed.");
        return entry.callback.GetPersistentEventCount () == 0;
      });
    }
    #endif
  }
}
