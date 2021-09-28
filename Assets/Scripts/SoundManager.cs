using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource themaSource = null;
    [SerializeField]
    private AudioSource sfxSource = null;

    public void startSfx()
    {
        sfxSource.Play();
    }
    public void SetThemaVolume(float volume)
    {
        themaSource.volume = volume;
    }
}
