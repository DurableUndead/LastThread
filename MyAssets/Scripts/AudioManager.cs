using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public AudioSource gameplayAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource ambientAudioSource;
    public AudioSource UIAudioSource;

    public AudioClip[] gameplayClips;
    public AudioClip[] musicClips;
    public AudioClip[] ambientClips;
    public AudioClip[] UIClips;
    
    public float volumeGameplayNow;
    public float volumeMusicNow;
    public float volumeAmbientNow;
    public float volumeUINow;

    // public NamedAudioClip[] gameplayClips2;
    // public NamedAudioClip[] musicClips2;
    // public NamedAudioClip[] ambientClips2;
    // public NamedAudioClip[] UIClips2;

    // public AudioClip GetAudioClipByName(NamedAudioClip[] audioClips, string name)
    // {
    //     foreach (var namedAudioClip in audioClips)
    //     {
    //         if (namedAudioClip.name == name)
    //         {
    //             return namedAudioClip.clip;
    //         }
    //     }
    //     return null; // Atau Anda bisa melemparkan pengecualian jika klip tidak ditemukan
    // }

    public void PlayButtonPressedUI()
    {
        UIAudioSource.PlayOneShot(UIClips[0]);
    }

    public void PlayButtonHoverSoundUI()
    {
        UIAudioSource.PlayOneShot(UIClips[1]);
    }

    public void PlayTriggerOrNextSoundUI()
    {
        UIAudioSource.PlayOneShot(UIClips[2]);
    }
}
