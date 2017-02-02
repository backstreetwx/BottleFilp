using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Common;
using System.Text;
using System.Collections;

namespace DataManagement.Common
{
  public class FileIO : Singleton<FileIO>
  {
    public bool EnableDeepDebug = false;

    public AudioClip LoadAudio(string fileFullPath)
    {
      return AudioReader.Instance.LoadAudio(fileFullPath);
    }

    public ResourceRequest LoadAudioAsync(string fileFullPath)
    {
      return AudioReader.Instance.LoadAsyncAudio(fileFullPath);
    }
  }
}
