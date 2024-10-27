using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class AudioController : Singleton<AudioController>
    {
        [Header("Main Settings:")]
        [Range(0, 1)]
        public float musicVolume = 0.3f;

        [Range(0, 1)]
        public float sfxVolume = 1f;

        public AudioSource musicAus;
        public AudioSource sfxAus;

        [Header("Game sounds and musics: ")]
        public AudioClip attack;
        public AudioClip buy;
        public AudioClip dead;
        public AudioClip enemyDead;
        public AudioClip fail;
        public AudioClip fireBullet;
        public AudioClip footStep;
        public AudioClip getHit;
        public AudioClip jump;
        public AudioClip land;
        public AudioClip missionComplete;
        public AudioClip unlock;
        public AudioClip btnClick;
        public AudioClip[] bgms;
        public AudioClip[] menus;

        public void PlaySound(AudioClip[] clips, AudioSource aus = null)
        {
            if (!aus)
            {
                aus = sfxAus;
            }

            if (clips != null && clips.Length > 0 && aus)
            {
                var randomIdx = Random.Range(0, clips.Length);
                aus.PlayOneShot(clips[randomIdx], sfxVolume);
            }
        }

        public void PlaySound(AudioClip clip, AudioSource aus = null)
        {
            if (!aus)
            {
                aus = sfxAus;
            }

            if (clip != null && aus)
            {
                aus.PlayOneShot(clip, sfxVolume);
            }
        }

        public void PlayMusic(AudioClip[] musics, bool loop = true)
        {
            if (musicAus && musics != null && musics.Length > 0)
            {
                var randomIdx = Random.Range(0, musics.Length);

                musicAus.clip = musics[randomIdx];
                musicAus.loop = loop;
                musicAus.volume = musicVolume;
                musicAus.Play();
            }
        }

        public void PlayMusic(AudioClip music, bool canLoop)
        {
            if (musicAus && music != null)
            {
                musicAus.clip = music;
                musicAus.loop = canLoop;
                musicAus.volume = musicVolume;
                musicAus.Play();
            }
        }

        public void SetMusicVolume(float vol)
        {
            if (musicAus) musicAus.volume = vol;
        }

        public void StopPlayMusic()
        {
            if (musicAus) musicAus.Stop();
        }

        public void PlayBackgroundMusic()
        {
            PlayMusic(bgms, true);
        }
    }

}