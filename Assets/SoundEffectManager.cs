using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{

    public static AudioClip[] SoundEffects = new AudioClip[5];
    static AudioSource AudioSrc;

    void Start()
    {
        SoundEffects[0] = Resources.Load<AudioClip>("Sounds/SoundEffects/成就");
        SoundEffects[1] = Resources.Load<AudioClip>("Sounds/SoundEffects/死亡");
        SoundEffects[2] = Resources.Load<AudioClip>("Sounds/SoundEffects/游戏胜利");
        SoundEffects[3] = Resources.Load<AudioClip>("Sounds/SoundEffects/被抓");
        SoundEffects[4] = Resources.Load<AudioClip>("Sounds/SoundEffects/金币2");
        AudioSrc = GetComponent<AudioSource>();
    }

    public static void playSound(int select)
    {
        AudioSrc.PlayOneShot(SoundEffects[select]);
    }
}

