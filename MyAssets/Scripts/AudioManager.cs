using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gameplayAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource ambientAudioSource;
    public AudioSource UIAudioSource;

    public AudioClip[] gameplayClips;
    public AudioClip[] musicClips;
    public AudioClip[] ambientClips;
    public AudioClip[] UIClips;
    
    public float volumeGameplay;
    public float volumeMusicNow;
    public float volumeAmbientNow;
    public float volumeUINow;
}
