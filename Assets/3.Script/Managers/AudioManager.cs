using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;

    [Header("BGM")]
    [SerializeField] Sound[] bgmClips;
    public float bgmVolume;
    [SerializeField] AudioSource BGMplayer;

    [Space(30)]
    [Header("SFX")]
    [SerializeField] float sfxVolume;
    public Sound[] sfxClips;
    [SerializeField] AudioSource SFXplayer;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        PlayBGM("BGM_Test");
    }

    /// <summary>
    /// 초기화합니다.
    /// </summary>
    private void Init()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();

        // BGM Player  세팅
        BGMplayer.playOnAwake = false;
        BGMplayer.loop = true;
        SetBgmVolume(gameManager.MasterVolumes * gameManager.BGMVolumes);

        // SFX Player  세팅
        SFXplayer.playOnAwake = false;
        SetSFXVolume(gameManager.SFXVolumes * gameManager.BGMVolumes);
    }


    /// <summary>
    /// BGM을 재생합니다.
    /// </summary>
    /// <param name="name"></param>
    public void PlayBGM(string name)
    {
        // 이미 재생중인 BGM이 있으면 멈춤
        StopBGM();

        // 이름 일치하는 BGM 찾아서 재생
        foreach (Sound s in bgmClips)
        {
            if (s.name.Equals(name))
            {
                BGMplayer.clip = s.clip;
                BGMplayer.Play();
                break;
            }
        }
    }

    /// <summary>
    /// BGM을 멈춥니다.
    /// </summary>
    public void StopBGM()
    {
        if (BGMplayer.isPlaying)
        {
            BGMplayer.Stop();
        }
    }

    /// <summary>
    /// BGM Volume을 설정합니다.
    /// </summary>
    /// <param name="volume"></param>
    public void SetBgmVolume(float volume)
    {
        BGMplayer.volume = volume;
    }

    /// <summary>
    /// 효과음을 재생합니다.
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        // 이름 일치하는 SFX 찾아서 재생
        foreach (Sound s in sfxClips)
        {
            if (s.name.Equals(name))
            {
                SFXplayer.clip = s.clip;
                SFXplayer.Play();
                break;
            }
        }
    }

    /// <summary>
    /// 효과음을 설정합니다.
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        Debug.Log("audioManager" + volume);
        SFXplayer.volume = volume;
    }
}
