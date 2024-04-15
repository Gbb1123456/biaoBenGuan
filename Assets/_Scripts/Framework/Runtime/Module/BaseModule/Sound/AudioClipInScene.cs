using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    public class AudioClipInScene : MonoBehaviour
    {
        public List<AudioClip> allClip = new List<AudioClip>();
        public Dictionary<string, AudioClip> allClips = new Dictionary<string, AudioClip>();
        private void Awake()
        {
            foreach (AudioClip clip in allClip) 
            {
                if (!allClips.ContainsKey(clip.name))
                {
                    allClips.Add(clip.name,clip);
                }
            }         
        }
    }
}

