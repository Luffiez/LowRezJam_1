using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    
    public AudioSource bgm;
    public AudioSource sfx;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySfx(AudioClip clip, float volume = 1f)
    {
        sfx.PlayOneShot(clip, volume);
    }

    public void ChangeBgm(AudioClip clip)
    {
        StartCoroutine(CrossFadeBgm(clip));
    }


    IEnumerator CrossFadeBgm(AudioClip newClip)
    {
        float timer = 1;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            bgm.volume = timer;
            yield return new WaitForEndOfFrame();
        }

        bgm.clip = newClip;
        while (timer <= 1)
        {
            timer += Time.deltaTime;
            bgm.volume = timer;
            yield return new WaitForEndOfFrame();
        }
    }

    internal void ResumeBgm()
    {
        bgm.UnPause();
    }

    internal void PauseBgm()
    {
        bgm.Pause();
    }
}
