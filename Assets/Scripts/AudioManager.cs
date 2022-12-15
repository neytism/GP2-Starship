using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SoundSfx;

    public AudioSource SoundBGMusic;
    
    public SoundType[] sounds;
    
    //singleton
    private static AudioManager _instance;
    public static  AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayLoop(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);

        if (clip != null)
        {
            SoundBGMusic.clip = clip;
            SoundBGMusic.Play();
        }
        else
        {
            Debug.Log("No clip found for Sound Type");
        }
    }

    public void PlayOnce(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            SoundSfx.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("No clip found for sound Type");
        }
    }

    public void StopPlayingBGM(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);

        if (clip != null)
        {
            SoundBGMusic.clip = clip;
            SoundBGMusic.Stop();
        }
        else
        {
            Debug.Log("No clip found for Sound Type");
        }
    }
    
    public void PausePlayingBGM(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);

        if (clip != null)
        {
            SoundBGMusic.clip = clip;
            SoundBGMusic.Pause();
        }
        else
        {
            Debug.Log("No clip found for Sound Type");
        }
    }
    
    public void ResumePlayingBGM(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);

        if (clip != null)
        {
            SoundBGMusic.clip = clip;
            SoundBGMusic.UnPause();
        }
        else
        {
            Debug.Log("No clip found for Sound Type");
        }
    }
    

    public void PlayFadeIn(Sounds sound, float speed, float maxVolume)
    {
        StartCoroutine(FadeIn(sound, speed, maxVolume));
    }
    
    
    IEnumerator FadeIn(Sounds sound, float speed, float maxVolume)
    {
        AudioClip clip = getSoundClip(sound);

        if (clip != null)
        {
            SoundBGMusic.clip = clip;
            SoundBGMusic.volume = 0;
            SoundBGMusic.Play();
            float audioVolume = SoundBGMusic.volume;

            while (SoundBGMusic.volume < maxVolume)
            {
                audioVolume += speed;
                SoundBGMusic.volume = audioVolume;
                yield return new WaitForSeconds(0.1f);
            }
            
            
        }
        else
        {
            Debug.Log("No clip found for Sound Type");
        }
    }
    

    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType _soundType = Array.Find(sounds, s => s.soundType == sound);
        if (_soundType != null)
        {
            return _soundType.soundClip;
        }

        return null;
    }
    
    [Serializable] public class SoundType
    {
        public Sounds soundType;
        public AudioClip soundClip;
    }
    public enum Sounds
    {
        ButtonClick,
        PlayerDeath,
        PlayerShoot,
        EnemyDeath,
        MainMenuBGM,
        GameBGM,
        LaserSound,
        EMPsound,
        SwiftSound
    }
}
