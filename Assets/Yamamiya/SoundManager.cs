using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip stageSelectBGM;
    [SerializeField] AudioClip battleStartBGM;
    [SerializeField] AudioClip battleLoopBGM;
    [SerializeField] AudioClip winBGM;

    [SerializeField] float battleBGMVolume = 0.5f;

    private AudioSource aud;
    private float InitialVolume = 0f;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        InitialVolume = aud.volume;
    }

    void Start()
    {
        PlayStageSelectBGM();  
    }

    public void PlayStageSelectBGM()
    {
        aud.Stop();
        aud.clip = stageSelectBGM;
        aud.Play();
    }

    public IEnumerator PlayBattleBGM()
    {
        aud.Stop();
        aud.volume = battleBGMVolume;
        aud.PlayOneShot(battleStartBGM);
        yield return new WaitUntil(() => !aud.isPlaying) ;
        aud.clip = battleLoopBGM ;
        aud.Play();
    }

    public void PlayWinBGM()
    {
        aud.Stop();
        aud.clip = winBGM;
        aud.Play();
    }
}