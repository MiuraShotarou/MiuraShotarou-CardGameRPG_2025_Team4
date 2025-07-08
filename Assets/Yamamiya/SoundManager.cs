using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip stageSelectBGM;
    [SerializeField] AudioClip battleStartBGM;
    [SerializeField] AudioClip battleLoopBGM;
    [SerializeField] AudioClip dragonStartBGM;
    [SerializeField] AudioClip dragonLoopBGM;
    [SerializeField] AudioClip winBGM;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
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
        aud.PlayOneShot(battleStartBGM);
        yield return new WaitUntil(() => !aud.isPlaying) ;
        aud.clip = battleLoopBGM ;
        aud.Play();
    }

    public IEnumerator PlayBattleDragonBGM()
    {
        aud.Stop();
        aud.PlayOneShot(dragonStartBGM);
        yield return new WaitUntil(() => !aud.isPlaying);
        aud.clip = dragonLoopBGM;
        aud.Play();
    }

    public void PlayWinBGM()
    {
        aud.Stop();
        aud.clip = winBGM;
        aud.Play();
    }
}