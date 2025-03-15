using System;
using UnityEngine;

public class Audio_Source : MonoBehaviour
{
    public static Audio_Source main;
    [SerializeField] public AudioSource source;
   [SerializeField] public  AudioClip[] sfx;
   [SerializeField] public  AudioClip[] music;
   [SerializeField] public  AudioSource bgm;
    private void Awake()
    {
        
        
            main = this;
            DontDestroyOnLoad(this);
        PlayMusic(0);
    }

    public  void PlaySFX(int id)
    {
        if (source.isPlaying == false)
        {
            source.clip = sfx[id];
            source.Play();
        }
        
    }
    public  void PlayMusic(int id)
    {
        bgm.clip = music[id];
        bgm.Play();
    }
}
