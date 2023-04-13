using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public enum Clip
    {
        shot   = 0,
        clear  = 1
    };

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        private AudioSource[] soundSource;
        void Start()
        {
            instance = GetComponent<SoundManager>();
            soundSource = GetComponents<AudioSource>();
        }

        public void PlayOneShot(Clip audioClip)
        {
            soundSource[(int)audioClip].Play();
        }

        public void PlayOneShot(Clip audioClip, float volumeScale)
        {
            AudioSource source = soundSource[(int)audioClip];
            source.PlayOneShot(source.clip, volumeScale);
        }
    }
}

