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
    [SerializeField] float bgmVolume;
    [SerializeField] AudioSource BGMplayer;

    [Space(30)]
    [Header("SFX")]
    [SerializeField] float sfxVolume;
    [SerializeField] Sound[] sfxClips;
    [SerializeField] AudioSource SFXplayer;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화합니다.
    /// </summary>
    private void Init()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();

        // BGM Player 생성 및 세팅
        GameObject bgm_obj = GetBgm_obj();
        bgm_obj.transform.parent = transform;
        BGMplayer = bgm_obj.AddComponent<AudioSource>();
        BGMplayer.playOnAwake = false;
        BGMplayer.loop = true;
        SetBgmVolume(gameManager.MasterVolumes * gameManager.BGMVolumes);

        // SFX Player 생성 및 세팅
        GameObject sfx_obj = new("SfxPlayer");
        sfx_obj.transform.parent = transform;
        SFXplayer = sfx_obj.AddComponent<AudioSource>();
        SFXplayer.playOnAwake = false;
        SetSFXVolume(gameManager.SFXVolumes * gameManager.BGMVolumes);
    }

    private static GameObject GetBgm_obj()
    {
        return new GameObject("BgmPlayer");
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
    void SetBgmVolume(float volume = 1.0f)
    {
        SFXplayer.volume = volume;
    }

    /// <summary>
    /// 효과음을 재생합니다.
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        // 이름 일치하는 SFX 찾아서 재생
        foreach (Sound s in bgmClips)
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
    void SetSFXVolume(float volume = 1.0f)
    {
        SFXplayer.volume = volume;
    }
}
