using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource themaSource = null;
    [SerializeField]
    private AudioSource sfxSource = null;
    [SerializeField]
    private AudioSource disallowanceSfx = null;

    public void startSfx()
    {
        Debug.Log("sfx");
        sfxSource.Play();
    }
    public void startDisallowance()
    {
        disallowanceSfx.Play();
    }
    public void SetThemaVolume(float volume)
    {
        themaSource.volume = volume;
    }
    public void Stop()
    {
        themaSource.volume = 0;
    }
    public void ReplayThema()
    {
        themaSource.volume = 1;
    }
}
